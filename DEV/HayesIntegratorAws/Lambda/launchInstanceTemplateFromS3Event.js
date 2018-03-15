let aws = require('aws-sdk');
let EC2 = new aws.EC2();

exports.handler = (events, context, callback) => {
    console.log('ProcessS3File lambda started');
    console.log(events.Records);
    let message = "";
    if(events.Records.length > 0) {
        let event = events.Records[0];
        console.log(event);
        if(event.eventName == 'ObjectCreated:Put') {
            console.log('ProcessS3File template launching');
            message = "done";
            launchEC2Template(process.env.templateId, () => { context.succeed(message); });
        } else {
            message = "Non Put event fired";
            console.log(message);
        }
    } else {
        message = "Events Array is empty";
        console.log(message);
    }
};

function launchEC2Template(templateId, cb) {
    let params = { 
        MaxCount: 1,
        MinCount: 1,
        LaunchTemplate: {
            LaunchTemplateId: templateId
        }
    };

    EC2.runInstances(params,
        (err,data) => {
            if (err) {
                console.log(err, err.stack);
                cb();
            }
            else {
                console.log(data);
                cb();
            }
        }
    );

};