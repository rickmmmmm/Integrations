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
echo " #### Starting PO Push Transaction"
CLIENT="CPS"
TYPE="PurchaseOrder"
REGION="us-east-1"

echo " #### Updating DataMapper process"
hayes-datamapper --sending-id;
cd /home/ec2-user;
INTEGRATIONID=$(<intgid.txt);
echo " #### Retrieved Integration ID from database and saved to local file ID = $INTEGRATIONID";
hayes-datamapper --get-token;
echo " #### Web API token retrieved."
hayes-datamapper --push-vendors -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Vendors pushed to API."
hayes-datamapper --push-products -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Products pushed to API."
hayes-datamapper --push-headers -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Header records pushed to API."
hayes-datamapper --push-details -id $INTEGRATIONID -lv 800 -i 0; ## THROWS ERRORS
echo " #### New Detail records pushed to API."
hayes-datamapper --complete -id $INTEGRATIONID;
echo " #### Purchase Order Data Push process complete!"
echo " #### Set region to $REGION"
# aws configure set default.region us-east-1;
aws configure set default.region $REGION;
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)
echo " #### Sending completion email "
$MESSAGE="{
  ""Subject"": {
    ""Data"": ""$CLIENT Purchase Order Integration Complete"",
    ""Charset"": ""utf8""
  },
  ""Body"": {
    ""Text"": {
      ""Data"": "" File: processed with InstanceID: $INSTANCEID "",
      ""Charset"": ""utf8""
    },
    ""Html"": {
      ""Data"": """",
      ""Charset"": """"
    }
  }
}"
aws ses send-email --from "do_not_reply@hayessoft.com" --destination "ToAddresses=" --message=$MESSAGE
echo " #### Terminate instance $INSTANCEID"
aws ec2 terminate-instances --instance-ids $INSTANCEID