let aws = require('aws-sdk');
let EC2 = new aws.EC2();

exports.handler = (event, context, callback) => {
    launchEC2Template(process.env.templateId, () => { context.succeed("done"); });
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