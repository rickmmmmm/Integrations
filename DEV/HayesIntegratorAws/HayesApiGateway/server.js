//server.js
'use strict'

var cors = require('cors');
var Promise = require('bluebird');
var _repo = require('./lib/repository.js');

var allowedRoutes = ['/api/authenticate','/api/test'];

var express = require('express'),
app = express(),
port = process.env.PORT || 3000,
bodyParser = require('body-parser');

app.use(cors());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use((req,res,next)=> {
  if (allowedRoutes.indexOf(req.originalUrl) !== -1) {
    next();
  }

  else if (req.headers['hayes-auth-cert'] && req.headers['hayes-auth-client']) {
    checkCert(req.headers).then(
      resolve => {
        next();
      },
      reject => {
        res.status(401).send();
      }
    );
  }
  else {
    res.status(401).send();
  }
});

var routes = require('./api/routes/intgErrorRoutes.js');
routes(app);

app.use((req, res) => {
    res.status(404).send({ url: req.originalUrl + ' not found' });
  });

app.listen(port);

console.log('todo list RESTful API server started on: ' + port);

function checkCert(request) {
  return new Promise(
    (res, rej) => {

      let opts = { certVal: request['hayes-auth-cert'], clientVal: request['hayes-auth-client']};

      _repo.checkCertByClientAndCertificateVal(opts).then(
        resolve => {
          res();
        },
        reject => {
          rej();
        }
      );
    }
  );
}