#!/bin/bash

CLIENT="CPS"
TYPE="Shipping"
AWSBUCKET="hssintg-prod"
FOLDER="intg_prod"
INTGFILE="intg_api_transact.sh"

echo ">>> Creating directory for Hayes DataMapper Application."
mkdir -p /home/ec2-user/etc
chmod -R 777 /home/ec2-user/etc
mkdir -p /home/ec2-user/etc/apps/node/datamapper
chmod -R 777 /home/ec2-user/etc/apps/node/datamapper

echo ">>> Creating Directory for "$CLIENT

mkdir -p /home/ec2-user/etc/$CLIENT/scripts
chmod -R 777 /home/ec2-user/etc/$CLIENT/scripts

echo ">>> Downloading application data from AWS S3..."
sudo aws s3 sync s3://$AWSBUCKET/apps/node/DataMapper /home/ec2-user/etc/apps/node/datamapper
echo ">>> datamapper downloaded"
cd /home/ec2-user/etc/apps/node/datamapper

echo ">>> starting dos2unix process"
dos2unix index.js
dos2unix /lib/*

echo ">>> Run npm install for datamapper"
npm install

echo ">>> download configuration.js "
sudo aws s3 cp s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/configuration.js /home/ec2-user/etc/apps/node/datamapper/lib/configuration.js
npm install -g

echo ">>> Downloading BASH scripts..."
sudo aws s3 cp s3://$AWSBUCKET/$FOLDER/$CLIENT/$TYPE/$INTGFILE /home/ec2-user/etc/$CLIENT/scripts
echo ">>> downloaded BASH script "$INTGFILE

echo ">>> Running BASH scripts..."
cd /home/ec2-user/etc/$CLIENT/scripts
sudo sh $INTGFILE;
echo ">>> Completed Shipping Push Process!"