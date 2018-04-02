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
echo " #### Setting Script Variables"
CLIENT="CPS";
TYPE="PurchaseOrder";
REGION="us-east-1";
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id);
CURRENTDATE=$(date '+%Y-%m-%d %H:%M:%S');

echo " #### Starting $CLIENT $TYPE API Push Process"

echo " #### Set region to $REGION";
aws configure set default.region $REGION;

echo " #### Updating DataMapper process";
hayes-datamapper --sending-id;

cd /home/ec2-user;
INTEGRATIONID=$(<intgid.txt);
echo " #### Retrieved Integration ID from database and saved to local file ID = $INTEGRATIONID";

hayes-datamapper --get-token;
echo " #### Web API token retrieved."

hayes-datamapper --push-vendors -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Vendors pushed to API."

# hayes-datamapper --toggle-vendors
# echo " #### Toggled Vendors pushed to API."

hayes-datamapper --push-products -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Products pushed to API."

# hayes-datamapper --toggle-products
# echo " #### Toggled products pushed to API."

hayes-datamapper --push-headers -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Header records pushed to API."

hayes-datamapper --push-details -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Detail records pushed to API."

hayes-datamapper --complete -id $INTEGRATIONID;
echo " #### $TYPE Data Push process complete!"

echo " #### Sending completion email ";
# RECIPIENTS="ToAddresses=""support@hayessoft.com"",CcAddresses=""jayala@hayessoft.com,gcollazo@hayessoft.com""";
RECIPIENTS="ToAddresses=""lsager@hayessoft.com, gcollazo@hayessoft.com"",CcAddresses=""jayala@hayessoft.com""";
TEXTCONTENT="\nThe $TYPE Integration has completed.\n\nTo access the results go to the Integration Portal and select Instance $INSTANCEID\n\nIf you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com\n\nHayes Software Systems";
HTMLCONTENT="<br />The $TYPE Integration has completed.<br /><br />To access the results go to the Integration Portal and select Instance $INSTANCEID<br /><br />If you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com<br /><br />Hayes Software Systems";
MESSAGE="Subject={Data=""$CLIENT $TYPE Integration Status - $CURRENTDATE"",Charset=""ascii""},Body={Text={Data=$TEXTCONTENT,Charset=""utf8""},Html={Data=$HTMLCONTENT,Charset=""utf8""}}";

aws ses send-email --from "do_not_reply@hayessoft.com" --destination "$RECIPIENTS" --message "$MESSAGE";

echo " #### Terminate instance $INSTANCEID";
aws ec2 terminate-instances --instance-ids $INSTANCEID
# aws ec2 stop-instances --instance-ids $INSTANCEID