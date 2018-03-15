###############################################################################################################################################
		#DataSentToTipweb:
			#Get ID of integration we wish to process and save to file
			#Get API token and save to local file
			#Send any new vendors to API in chunks of 800
			#Send any new products to API in chunks of 800
			#Send any new valid headers to API in chunks of 800
			#Mark headers as Submitted
			#Send any new valid details to API in chunks of 800
			#Mark details as Submitted
		#Cleanup:
			#Toggle DataProcessedSuccessfully
###############################################################################################################################################
echo "Start"
CLIENT="Client"
TYPE="Type"
REGION="AWS_Region"

hayes-datamapper --sending-id;
cd /home/ec2-user;
INTEGRATIONID=$(<intgid.txt);
echo "Retrieved Integration ID from database and saved to local file ID = "$INTEGRATIONID;
hayes-datamapper --get-token;
echo "Web API token retrieved."
...
hayes-datamapper --endpoint_process -id $INTEGRATIONID -lv 800 -i 0;
echo "Endpoint_Process Executed."
...
hayes-datamapper --complete -id $INTEGRATIONID;
echo "Done!"
aws configure set default.region $REGION;
aws ec2 terminate-instances --instance-ids $(curl http://169.254.169.254/latest/meta-data/instance-id)