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
ENVIRONMENT="QA"
CLIENT="CPS"
TYPE="Shipping"
INSTANCEID=$(curl http://169.254.169.254/latest/meta-data/instance-id)
CURRENTDATE=$(date '+%Y-%m-%d %H:%M:%S');
if [ $ENVIRONMENT = "Production" ]; then
    ### Production
    DEBUG=false
    REGION="us-east-1"
else
    ### QA
    DEBUG=true
    REGION="us-east-1"
fi

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

hayes-datamapper --push-shipments -id $INTEGRATIONID -lv 800 -i 0;
echo " #### New Shipments pushed to API."

hayes-datamapper --complete -id $INTEGRATIONID;
echo " #### $TYPE Data Push process complete!"

echo " #### Sending completion email ";
if [ $ENVIRONMENT = "Production" ]; then
    RECIPIENTS="ToAddresses=""support@hayessoft.com""";
else
    RECIPIENTS="ToAddresses=""Integration_results@hayessoft.com""";
fi
TEXTCONTENT="\nThe Chicago Hayes Oracle $TYPE Integration has completed.\n\nIf you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com\n\nHayes Software Systems";
HTMLCONTENT="<br />The Chicago Hayes Oracle $TYPE Integration has completed.<br /><br />If you have any questions please contact support at 1-800-495-5993 or support@hayessoft.com<br /><br />Hayes Software Systems";
MESSAGE="Subject={Data=""Chicago Hayes Oracle $TYPE Integration Status: Completed"",Charset=""ascii""},Body={Text={Data=$TEXTCONTENT,Charset=""utf8""},Html={Data=$HTMLCONTENT,Charset=""utf8""}}";

aws ses send-email --from "do_not_reply@hayessoft.com" --destination "$RECIPIENTS" --message "$MESSAGE";

echo ""

if [ $DEBUG == true ]; then
    # echo -e " #### \033[1;32m Stop instance \e[0m $INSTANCEID"
	echo -e " #### \033[1;32m Stop instance \e[0m "
    aws ec2 stop-instances --instance-ids $INSTANCEID;
else
    # echo -e " #### \033[1;32m Terminate instance \e[0m $INSTANCEID"
	echo -e " #### \033[1;32m Terminate instance \e[0m "
    aws ec2 terminate-instances --instance-ids $INSTANCEID;
fi

###########################################################################################################################################
#DONE!
###############################################################################################################################################