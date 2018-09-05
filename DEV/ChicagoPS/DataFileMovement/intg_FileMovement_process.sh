#!/bin/bash
#Integration EC2 Process
echo " #### Setting Script Variables"
ENVIRONMENT="QA"
CLIENT="CPS"
TYPE="FileMovement"


INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)

if [ $ENVIRONMENT = "Production" ]; then
    ### Production
    DEBUG=false
    LAUNCH_NEXT=true
    AWSBUCKET="hssintg-prod"
    FOLDER="intg_prod"
    REGION="us-east-1"
	SFTPFolder="hssintg-sftp/chicagoint_sftp_user/uploads"
else
    ### QA
    DEBUG=false
    LAUNCH_NEXT=true
    AWSBUCKET="hssintg"
    FOLDER="intg_test"
    REGION="us-east-1"
	SFTPAWSBUCKET="hssintg"
	SFTPFolder="sftpFolder/CPSFiles"
fi

echo " #### Starting $CLIENT $TYPE Process"

echo " #### Set region to $REGION"
aws configure set default.region $REGION

#clear local folders
echo " #### Clearing previous data";
rm -rf /home/ec2-user/etc/$CLIENT/processing/csv/*

echo " #### Getting new data from AWS S3 bucket."
aws s3 sync s3://$SFTPAWSBUCKET/$SFTPFolder /home/ec2-user/etc/$CLIENT/processing/csv

cd /home/ec2-user/etc/$CLIENT/processing/csv/

fileCount="$(ls | wc -l)"

if [ $fileCount -gt 0 ]; then

    # Moving files to the production path
    echo " #### Processing input files to Production path: //$AWSBUCKET/$FOLDER/$CLIENT/ PurchaseOrder/Shipping folders"
    processedFiles=""
	echo " "
 
    for csvFile in *.*; do
	
		echo " ##### 1. Checking file $csvFile for folder placement"
		ProductionFolder="Default"
		if [[ $csvFile = *"_PO_LINES_D_V_"* ]]; then
			ProductionFolder="PurchaseOrder"
			else
			if [[ $csvFile = *"_PO_LINES_S_V_"* ]]; then
				ProductionFolder="Shipping"
				else
				if [[ $csvFile = *"_PO_LINES_I_V_"* ]]; then
					ProductionFolder="DeleteMe"
					else
						ProductionFolder="DeleteMe"
				fi
			fi
		fi
		
		if [ $ProductionFolder = "DeleteMe" ]; then
			echo -e " ##### 2. \033[1;31mRemoving\e[0m file $csvFile"
			aws s3 rm "s3://$SFTPAWSBUCKET/$SFTPFolder/$csvFile" 
		else
			echo -e " ##### 2. \033[1;32mMoving\e[0m file $csvFile to the folder $ProductionFolder"
			aws s3 mv "s3://$SFTPAWSBUCKET/$SFTPFolder/$csvFile" "s3://$AWSBUCKET/$FOLDER/$CLIENT/$ProductionFolder/files/$csvFile"
		fi
		echo " "
    done

	echo -e " ##### \033[1;34mCoping run.process\e[0m file to PurchaseOrder/files folder"
	aws s3 cp "s3://$AWSBUCKET/$FOLDER/$CLIENT/PurchaseOrder/run.process" "s3://$SFTPAWSBUCKET/$SFTPFolder/run.process"
	aws s3 mv "s3://$SFTPAWSBUCKET/$SFTPFolder/run.process" "s3://$AWSBUCKET/$FOLDER/$CLIENT/PurchaseOrder/files/run.process"
fi
if [ $DEBUG == true ]; then
	echo -e " #### \033[1;32m Stop instance \e[0m "
	aws ec2 stop-instances --instance-ids $INSTANCEID;
else
	echo -e " #### \033[1;32m Terminate instance \e[0m "
	aws ec2 terminate-instances --instance-ids $INSTANCEID;
fi
	