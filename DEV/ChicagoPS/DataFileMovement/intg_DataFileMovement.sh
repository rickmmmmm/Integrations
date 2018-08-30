#!/bin/bash

echo ">>> Download the Data File Movement Process Init Script"
sudo aws s3 cp s3://hssintg/intg_test/CPS/DataFileMovement/init/intg_DataFileMovement.sh /home/ec2-user;

echo ">>> Start the Data File Movement Process Init Script"
sudo sh /home/ec2-user/intg_init.sh