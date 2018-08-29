#!/bin/bash
#Integration EC2 Process
echo " #### Setting Script Variables"
ENVIRONMENT="QA"
CHUNK_SIZE=5000
CLIENT="CPS"
TYPE="PurchaseOrder"
TEMPLATE="intgCpsPush"
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)
CURRENTDATE=$(date '+%Y%m%d_%H%M%S');
if [ $ENVIRONMENT = "Production" ]; then
    ### Production
    DEBUG=false
    LAUNCH_NEXT=true
    AWSBUCKET="hssintg-prod"
    FOLDER="intg_prod"
    REGION="us-east-1"
else
    ### QA
    DEBUG=false
    LAUNCH_NEXT=true
    AWSBUCKET="hssintg"
    FOLDER="intg_test"
    REGION="us-east-1"
fi

echo " #### Starting $CLIENT $TYPE Data Process"

echo " #### Set region to $REGION"
aws configure set default.region $REGION

#clear local folders
echo " #### Clearing previous data";
rm -rf /home/ec2-user/etc/$CLIENT/processing/json/parsed/*
rm -rf /home/ec2-user/etc/$CLIENT/processing/json/arrays/*
rm -rf /home/ec2-user/etc/$CLIENT/processing/csv/*
rm -rf /home/ec2-user/etc/$CLIENT/processing/json/*.json

#Convert file to JSON
echo " #### Getting new data from AWS S3 bucket."
aws s3 sync s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files /home/ec2-user/etc/$CLIENT/processing/csv
cd /home/ec2-user/etc/$CLIENT/processing/csv/

fileCount="$(ls | wc -l)"

if [ $fileCount -gt 0 ]; then

    echo " #### Starting datamapper entry for instance "$INSTANCEID;
    hayes-datamapper --create -id $INSTANCEID;
    hayes-datamapper -gld;

    # Move files to the archive path
    echo " #### Moving input files to Archive path: //$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/"
    processedFiles=""
    processedFilesHtml=""
    ### Add logic to check for files?
    for csvFile in *.csv; do
        archiveExt="${csvFile##*.}";
        archiveFileName="${csvFile%.*}";
        ARCHIVE_FILE=$archiveFileName"_"$CURRENTDATE"."$archiveExt;
        echo " #### Moving file $csvFile to the archive folder as $ARCHIVE_FILE"
        aws s3 mv "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files/$csvFile" "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/$ARCHIVE_FILE"

        echo " #### Adding file to DataIntegrationFiles"
        hayes-datamapper --insert-process-file --client "$CLIENT" -id "$INSTANCEID" --filename "$csvFile" --filelink "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/$ARCHIVE_FILE";

        processedFiles="$processedFiles""\n - ""$csvFile";
        processedFilesHtml="$processedFilesHtml""<br />&nbsp;-&nbsp;""$csvFile";
    done

    echo " #### Sending the file processed email"
    if [ $ENVIRONMENT = "Production" ]; then
        RECIPIENTS="ToAddresses=""support@hayessoft.com""";
    else        
        RECIPIENTS="ToAddresses=""rgailey@hayessoft.com""";
    fi
TEXTCONTENT="\nThe Chicago Hayes Oracle $TYPE Integration has begun processing files: $processedFiles\n\nIf you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com\n\nHayes Software Systems";
HTMLCONTENT="<br />The Chicago Hayes Oracle $TYPE Integration has begun processing files: $processedFilesHtml<br /><br />If you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com<br /><br />Hayes Software Systems";
MESSAGE="Subject={Data=""Chicago Hayes Oracle $TYPE Integration Status: Started"",Charset=""ascii""},Body={Text={Data=$TEXTCONTENT,Charset=""utf8""},Html={Data=$HTMLCONTENT,Charset=""utf8""}}";

aws ses send-email --from "do_not_reply@hayessoft.com" --destination "$RECIPIENTS" --message "$MESSAGE";

    # remove the process file
    echo " #### Removing the run.process file"
    aws s3 rm "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files/run.process"

    #Split file into 10,000 element chunks
    echo " #### Converting csv files to JSON..."
    for csvFile in *.csv; do
        echo " #### Converting CSV file $csvFile to json format"
        hayes-datamapper --csv-to-json -f "$csvFile" -fo "/home/ec2-user/etc/$CLIENT/processing/json/${csvFile/csv/json}";
        # hayes-datamapper --csv-to-json -f "csvFile" -fo "/home/ec2-user/etc/$CLIENT/processing/json/csvFile";
    done

    cd "/home/ec2-user/etc/$CLIENT/processing/json/";
    echo " #### Splitting large JSON file to smaller chunks..."
    for jsonFile in *.json; do
        echo " #### Splitting json file: $jsonFile"
        cat "$jsonFile" | jq -c -M '.[]' | split -l $CHUNK_SIZE - "./parsed/$jsonFile";
    done

    #Convert each chunk into a JSON array
    cd "/home/ec2-user/etc/$CLIENT/processing/json/parsed/";
    echo " #### Converting small chunks to JSON arrays...";
    for jsonChunk in *; do
        echo " #### converting json chunks to arrays for file: $jsonChunk"
        cat "$jsonChunk" | jq --raw-input . | jq -s . > "../arrays/$jsonChunk.json";
    done

    ###############################################################################################################################################
    #    #Purchase Orders
    #        #Processing:
    #            #For each array:
    #                #Map data to appropriate DB flat table
    #                #Process Purchase Order Headers
    #                #Process Purchase Order Details
    #                #Toggle Post-processing
    ###############################################################################################################################################
    cd "/home/ec2-user/etc/$CLIENT/processing/json/arrays/";

    for jsonArray in *.json; do
        echo " #### Mapping Array chunks of file: $jsonArray";
        hayes-datamapper --mapflat -f "/home/ec2-user/etc/$CLIENT/processing/json/arrays/$jsonArray" -id $INSTANCEID ;
        echo " #### Mapped flat data to database...";
        # hayes-datamapper --vendors -id $INSTANCEID -ids true;
        # echo " #### Mapped new vendors.";
        # hayes-datamapper --stage-headers -id $INSTANCEID;
        hayes-datamapper --headers -id $INSTANCEID;
        echo " #### Mapped headers.";
        hayes-datamapper --details -id $INSTANCEID;
        echo " #### Mapped details.";
        hayes-datamapper -chu -id $INSTANCEID;
        echo " #### Next chunk";
    done

    echo " #### Done processing chunks";
    ###############################################################################################################################################
            #Post-Processing:
                #Remove products that already exist
                #Remove vendors that already exist
                #Remove PO headers that already exist
                #Remove PO details that have invalid headers
                #Remove PO details that already exist
                #Run any custom scripts
                #Toggle DataSentToTipweb
    ###############################################################################################################################################
    hayes-datamapper --post-processing -id $INSTANCEID;
    hayes-datamapper --filter-unncessary -id $INSTANCEID;
    echo " #### Unnecessary records removed pre-push to API.";

    echo " #### Stage New Data.";
    hayes-datamapper --vendors -id $INSTANCEID -ids true;
    echo " #### Mapped new vendors.";
    hayes-datamapper --products -id $INSTANCEID;
    echo " #### Mapped new products.";

    # hayes-datamapper --headers -id $INSTANCEID;
    # echo " #### Mapped new headers.";

    # echo " #### Running custom scripts.";
    # hayes-datamapper -cust

    echo " #### Beginning send to TIPWEBAPI."
    hayes-datamapper --send-to-api -id $INSTANCEID;
    echo " #### File Data Processing stage complete";

    ###############################################################################################################################################
    #Stop currently running instance and start api push instance.
    ###############################################################################################################################################
	
	echo ""
	echo " #### Debug: $DEBUG and LaunchNext: $LAUNCH_NEXT "
	echo ""

    if [ $LAUNCH_NEXT == true ] && [ $DEBUG == false ]; then
        # echo " #### Launching the EC2 for the next step using template $TEMPLATE"
		echo -e " #### Launching the \033[1;32m EC2 for the next step  \e[0m using template"
        aws ec2 run-instances --count 1 --launch-template LaunchTemplateName=$TEMPLATE;
    fi

	echo ""

    if [ $DEBUG == true ]; then
       # echo -e " #### \033[1;32m Stop instance \e[0m $INSTANCEID"
		echo -e " #### \033[1;32m Stop instance \e[0m "
        aws ec2 stop-instances --instance-ids $INSTANCEID;
    else
        # echo -e " #### \033[1;32m Terminate instance \e[0m $INSTANCEID"
		echo -e " #### \033[1;32m Terminate instance \e[0m "
        aws ec2 terminate-instances --instance-ids $INSTANCEID;
    fi

else
    echo -e " #### \033[1;37m No Files found. \e[0m Stop instance $INSTANCEID"
    aws ec2 stop-instances --instance-ids $INSTANCEID;
fi

    ###############################################################################################################################################
    #DONE!
    ###############################################################################################################################################