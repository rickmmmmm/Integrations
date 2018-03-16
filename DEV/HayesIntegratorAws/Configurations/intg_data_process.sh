#!/bin/bash
#Integration EC2 Process

echo " #### Setting Script Variables"
CLIENT="INTEGRATION_CLIENT"
TYPE="INTEGRATION_TYPE"
AWSBUCKET="S3_BUCKET"
FOLDER="S3_BUCKET_FOLDER"
TEMPLATE="LAUNCH_TEMPLATE"
REGION="AWS_REGION"
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)
CURRENTDATE=$(date '+%Y%m%d_%H%M%S');

echo " #### Starting $CLIENT $TYPE API Push Process"

echo " #### Set region to $REGION"
aws configure set default.region $REGION

# Get Tags for instance
# echo " #### Get Instance Tag Keys";
# AWSBUCKET=$(aws ec2 describe-tags --filters "Name=key,Values=s3Bucket" | jq -r ".Tags[0].Value"); # $TAGS.Tags["s3Bucket"];
# # AWSBUCKET=$(perl -n -mHTML::entities -e " ; print HTML::Entities::decode_entities($AWSBUCKET) ;");
# echo " #### $AWSBUCKET"
# AWSKEY=$(aws ec2 describe-tags --filters "Name=key,Values=s3Key" | jq -r ".Tags[0].Value"); # $TAGS.Tags["s3Key"];
# # AWSKEY=$(aws ec2 describe-tags --filters "Name=key,Values=s3Key" | jq -r ".Tags[0].Value" | recode html..ascii); # $TAGS.Tags["s3Key"];
# # AWSKEY=$(perl -n -mHTML::entities -e " ; print HTML::Entities::decode_entities($AWSKEY) ;");
# AWSKEY=$(urldecode "$AWSKEY");
# echo " #### $AWSKEY"
# if [ -n "$AWSBUCKET" -a -n "$AWSKEY" ]; then
# echo " #### Getting file from: $AWSBUCKET/$AWSKEY"

#clear local folders
echo " #### Clearing previous data"
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
for csvFile in *.csv; do
    echo " #### Moving file $csvFile to the archive folder"
    aws s3 mv "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files/$csvFile" "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/$csvFile_$CURRENTDATE"

    echo " #### Adding file to DataIntegrationFiles"
    hayes-datamapper --insert-process-file -client $CLIENT -id $INSTANCEID -filename $csvFile -filelink "s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/archive/$csvFile_$CURRENTDATE";
done

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
#    #PROCESS
#        #Processing:
#            #For each array:
#                #Map data to appropriate DB flat table
#                #Execute Process(es)
#                #Toggle Post-processing
###############################################################################################################################################
cd "/home/ec2-user/etc/$CLIENT/processing/json/arrays/";

for jsonArray in *.json; do

    echo " #### Mapping Array chunks of file: $jsonArray";
    hayes-datamapper --mapflat -f "/home/ec2-user/etc/$CLIENT/processing/json/arrays/$jsonArray" -id $INSTANCEID ;
    echo " #### Mapped flat data to database...";

    echo " #### Mapped ENTITIES OR OBJECTS.";
    hayes-datamapper --ENDPOINT_PROCESS -id $INSTANCEID -ids true;
    ...

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
hayes-datamapper --filter-unncessary -id $INSTANCEID;
echo " #### Unnecessary records removed pre-push to API.";

echo " #### Staging product data.";
hayes-datamapper --products -id $INSTANCEID;

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

# else
#     echo " #### Could NOT get a Bucket and file from the Instance Tags #### "
#     echo " #### Terminate instance $INSTANCEID"
#     aws ec2 terminate-instances --instance-ids $INSTANCEID;
# fi