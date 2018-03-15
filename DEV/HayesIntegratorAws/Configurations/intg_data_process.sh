#!/bin/bash
#Integration EC2 Process

#Download file
CLIENT="CPS"
TYPE="PurchaseOrder"
REGION="us-east-1"
AWSBUCKET="hssintg"
FOLDER="intg_prod"
TEMPLATE="intgCpsPush"
#clear local folders
rm -rf /home/ec2-user/etc/$CLIENT/processing/json/parsed/*
rm -rf /home/ec2-user/etc/$CLIENT/processing/json/arrays/*
rm -rf /home/ec2-user/etc/$CLIENT/processing/csv/*
rm -rf /home/ec2-user/etc/$CLIENT/processing/json/*.json
echo "Clearing previous data"

#Convert file to JSON
echo "Getting new data from AWS S3 bucket."
aws s3 sync s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files /home/ec2-user/etc/$CLIENT/processing/csv
#Split file into 10,000 element chunks
cd /home/ec2-user/etc/$CLIENT/processing/csv/
echo "Converting csv file to JSON..."
for i in *.csv; do csvtojson $i > /home/ec2-user/etc/$CLIENT/processing/json/${i/csv/json}; done

cd /home/ec2-user/etc/$CLIENT/processing/json/
echo "Splitting large JSON file to smaller chunks..."
for j in *.json; do cat $j | jq -c -M '.[]' | split -l 10000 - ./parsed/$j; done

#Convert each chunk into a JSON array

cd /home/ec2-user/etc/$CLIENT/processing/json/parsed/;
echo "Converting small chunks to JSON arrays...";
for i in *; do cat $i | jq --raw-input . | jq -s . > ../arrays/$i.json; done

cd /home/ec2-user/etc/$CLIENT/processing/json/arrays;
###############################################################################################################################################
#Create integration in DB
#Set variable to instance-id
###############################################################################################################################################
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)
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
for j in *.json; do
echo 'Processing File: '$j;
hayes-datamapper --mapflat -f '/home/ec2-user/etc/'$CLIENT'/processing/json/arrays/'$j -id $INSTANCEID ;
echo "Mapped flat data to database...";
...
hayes-datamapper --endpoint_process -id $INSTANCEID -ids true;
echo "Endpoint_Process Executed.";
...
hayes-datamapper -chu -id $INSTANCEID;
echo "Next chunk";
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
echo "Unnecessary records removed pre-push to API.";

echo "Staging product data.";
hayes-datamapper --products -id $INSTANCEID;
echo "Running custom scripts.";
hayes-datamapper -cust

echo "Beginning send to TIPWEBAPI."
hayes-datamapper --send-to-api -id $INSTANCEID;
echo "File Data Processing stage complete";
###############################################################################################################################################
#Stop currently running instance and start api push instance.
###############################################################################################################################################
aws s3 rm s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/files/ --recursive
aws configure set default.region $REGION;
aws ec2 run-instances --count 1 --launch-template LaunchTemplateName=$TEMPLATE;
aws ec2 terminate-instances --instance-ids $INSTANCEID;
###############################################################################################################################################
#DONE!
###############################################################################################################################################