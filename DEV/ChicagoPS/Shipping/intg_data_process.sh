#!/bin/bash
#Integration EC2 Process

echo " #### Setting Script Variables"
CLIENT="CPS"
TYPE="Shipping"
AWSBUCKET="hssintg-prod"
FOLDER="intg_prod"
TEMPLATE="intgCpsPushShipping"
REGION="us-east-1"
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)
CURRENTDATE=$(date '+%Y%m%d_%H%M%S');

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

# Move files to the archive path
echo " #### Moving input files to Archive path: //$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/"
processedFiles = "";
processedFilesHtml = "";
for csvFile in *.csv; do
    archiveExt="${csvFile##*.}";
    archiveFileName="${csvFile%.*}";
    ARCHIVE_FILE=$archiveFileName"_"$CURRENTDATE"."$archiveExt;
    echo " #### Moving file $csvFile to the archive folder as $ARCHIVE_FILE"
    aws s3 mv "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files/$csvFile" "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/$ARCHIVE_FILE"

    echo " #### Adding file to DataIntegrationFiles"
    hayes-datamapper --insert-process-file --client "$CLIENT" -id "$INSTANCEID" --filename "$csvFile" --filelink "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/$ARCHIVE_FILE";

    processedFiles="$processedFiles""\n    ""$csvFile";
    processedFilesHtml="$processedFilesHtml""<br />    ""$csvFile";
done

echo " #### Sending the file processed email"
RECIPIENTS="ToAddresses=""lsager@hayessoft.com, gcollazo@hayessoft.com"",CcAddresses=""jayala@hayessoft.com""";
TEXTCONTENT="\nThe $TYPE Integration has processed for files: $processedFiles\n\nTo access the results go to the Integration Portal and select Instance $INSTANCEID\n\nIf you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com\n\nHayes Software Systems";
HTMLCONTENT="<br />The $TYPE Integration has processed for files: $processedFilesHtml<br /><br />To access the results go to the Integration Portal and select Instance $INSTANCEID<br /><br />If you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com<br /><br />Hayes Software Systems";
MESSAGE="Subject={Data=""$CLIENT $TYPE Integration Status - $CURRENTDATE"",Charset=""ascii""},Body={Text={Data=$TEXTCONTENT,Charset=""utf8""},Html={Data=$HTMLCONTENT,Charset=""utf8""}}";

aws ses send-email --from "do_not_reply@hayessoft.com" --destination "$RECIPIENTS" --message "$MESSAGE";

# remove the process file
echo " #### Removing the run.process file"
aws s3 rm "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files/run.process"

#Split file into 10,000 element chunks
echo " #### Converting csv files to JSON..."
for csvFile in *.csv; do
    echo " #### Converting CSV file $csvFile to json format"
    csvtojson "$csvFile" > "/home/ec2-user/etc/$CLIENT/processing/json/${csvFile/csv/json}";
done

cd "/home/ec2-user/etc/$CLIENT/processing/json/";
echo " #### Splitting large JSON file to smaller chunks..."
for jsonFile in *.json; do
    echo " #### Splitting json file: $jsonFile"
    cat "$jsonFile" | jq -c -M '.[]' | split -l 10000 - "./parsed/$jsonFile";
done

#Convert each chunk into a JSON array
cd "/home/ec2-user/etc/$CLIENT/processing/json/parsed/";
echo " #### Converting small chunks to JSON arrays...";
for jsonChunk in *; do
    echo " #### converting json chunks to arrays for file: $jsonChunk"
    cat "$jsonChunk" | jq --raw-input . | jq -s . > "../arrays/$jsonChunk.json";
done

echo " #### Starting datamapper entry for instance "$INSTANCEID;
hayes-datamapper --create -id $INSTANCEID;
hayes-datamapper -gld;

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
    hayes-datamapper -chu -id $INSTANCEID;
    echo " #### Next chunk";
done

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
hayes-datamapper ---filter-bad-shipments -id $INSTANCEID;
echo " #### Unnecessary records removed pre-push to API.";

echo " #### Running custom scripts.";
hayes-datamapper -cust

echo " #### Beginning send to TIPWEBAPI."
hayes-datamapper --send-to-api -id $INSTANCEID;
echo " #### File Data Processing stage complete";

###############################################################################################################################################
#Stop currently running instance and start api push instance.
###############################################################################################################################################
echo " #### Launching the EC2 for the next step using template $TEMPLATE"
aws ec2 run-instances --count 1 --launch-template LaunchTemplateName=$TEMPLATE;

echo " #### Terminate instance $INSTANCEID"
aws ec2 terminate-instances --instance-ids $INSTANCEID;
###############################################################################################################################################
#DONE!
###############################################################################################################################################