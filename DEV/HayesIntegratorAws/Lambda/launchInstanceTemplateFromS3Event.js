let aws = require('aws-sdk');
let EC2 = new aws.EC2();

exports.handler = (events, context, callback) => {
    let process = ;
    let message = "";

    console.log(process + ' lambda started');
    // console.log(events.Records);
    if (events.Records.length > 0) {
        let event = events.Records[0];

        // console.log(event);
        // console.log(event.s3.bucket);
        // console.log(event.s3.object);

        // let bucketName = event.s3.bucket.name;
        // let fileKey = event.s3.object.key; // let fileKey = decodeURIComponent(event.s3.object.key);

        if (event.eventName == 'ObjectCreated:Put') {
            let fileKey = event.s3.object.key;
            console.log(fileKey);
            if (fileKey.includes('run.process')) {
                console.log(process + ' template launching');
                message = "done";
                launchEC2Template(process.env.templateId, () => { context.succeed(message); callback(null, message); });
                // launchEC2Template(process.env.templateId, bucketName, fileKey, () => { context.succeed(message); callback(null, message);});
            } else {
                message = "Non process event fired";
                console.log(message);
                callback(null, message);
            }
        } else {
            message = "Non Put event fired";
            console.log(message);
            callback(null, message);
        }
    } else {
        message = "Events Array is empty";
        console.log(message);
        callback(null, message);
    }
};

function launchEC2Template(templateId, cb) {
    //function launchEC2Template(templateId, s3Bucket, s3Key, cb) {

    let params = {
        MaxCount: 1,
        MinCount: 1,
        LaunchTemplate: {
            LaunchTemplateId: templateId
        },
        // TagSpecifications: [
        //     {
        //         ResourceType: "instance",
        //         Tags: [
        //             {
        //                 Key: "s3Bucket",
        //                 Value: s3Bucket
        //             },
        //             {
        //                 Key: "s3Key",
        //                 Value: s3Key
        //             }
        //         ]
        //     }
        // ]
    };

    EC2.runInstances(params,
        (err, data) => {
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