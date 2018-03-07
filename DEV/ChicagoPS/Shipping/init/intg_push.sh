#!/bin/bash

CLIENT="CPS"
TYPE="Shipping"
AWSBUCK="hssintg"
INTGFILE="intg_api_transact.sh"

echo "Creating directory for Hayes DataMapper Application."
mkdir -p /home/ec2-user/etc
mkdir -p /home/ec2-user/etc/apps/node/datamapper

echo "Creating Directory for "$CLIENT

mkdir -p /home/ec2-user/etc/$CLIENT/scripts

echo "Downloading application data from AWS S3..."
aws s3 sync s3://$AWSBUCK/apps/node/DataMapper /home/ec2-user/etc/apps/node/datamapper
cd /home/ec2-user/etc/apps/node/datamapper
dos2unix index.js
dos2unix /lib/*
npm install
aws s3 cp s3://$AWSBUCK/intg_prod/$CLIENT/$TYPE/configuration.js /home/ec2-user/etc/apps/node/datamapper/lib/configuration.js
npm install -g

echo "Downloading BASH scripts..."
aws s3 cp s3://$AWSBUCK/intg_prod/$CLIENT/$TYPE/$INTGFILE /home/ec2-user/etc/$CLIENT/scripts
echo "Running BASH scripts..."
cd /home/ec2-user/etc/$CLIENT/scripts
sudo sh $INTGFILE;
echo "Done!"