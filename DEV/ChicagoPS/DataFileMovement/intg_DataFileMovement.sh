#!/bin/bash
echo ">>> Setting Script Variables"
DEBUG=true
ENVIRONMENT="Production"
CLIENT="CPS"
TYPE="DataFileMovement"
INTGFILE="intg_FileMovement_process.sh"
echo ">>> Setting ENVIRONMENT Varables for $ENVIRONMENT"
if [ $ENVIRONMENT = "Production" ]; then
    ### Production
    AWSBUCKET="hssintg-prod"
    FOLDER="intg_prod"
    REGION="us-east-1"
else
    ### QA
    AWSBUCKET="hssintg"
    FOLDER="intg_test"
    REGION="us-east-1"
fi

echo ">>> Creating Local User Directories"
mkdir -p /home/ec2-user/etc
chmod -R 777 /home/ec2-user/etc
echo ">>> Creating Local User Directories for "$CLIENT" Processes"
mkdir -p /home/ec2-user/etc/$CLIENT/scripts
chmod -R 777 /home/ec2-user/etc/$CLIENT/scripts
mkdir -p /home/ec2-user/etc/$CLIENT/processing/csv
chmod -R 777 /home/ec2-user/etc/$CLIENT/processing/csv
echo ">>> starting dos2unix process"
sudo dos2unix index.js
sudo dos2unix /lib/*
echo ">>> Downloading BASH scripts..."
sudo aws s3 cp s3://$AWSBUCKET/$FOLDER/$CLIENT/PurchaseOrder/$INTGFILE /home/ec2-user/etc/$CLIENT/scripts
echo ">>> downloaded BASH script "$INTGFILE
echo ">>> Running BASH scripts..."
cd /home/ec2-user/etc/$CLIENT/scripts
sudo sh $INTGFILE;
echo ">>> Completed $TYPE Process!"