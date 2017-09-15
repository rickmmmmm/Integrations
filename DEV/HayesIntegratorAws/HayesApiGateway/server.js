//server.js

var Promise = require('bluebird');

var express = require('express'),
app = express(),
port = process.env.PORT || 3000,
bodyParser = require('body-parser');

app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use((req,res,next)=> {
  res.header("Access-Control-Allow-Origin", "*");
  res.header("Access-Control-Allow-Headers", "Origin, X-Requested-With, Content-Type, Accept");
  next();
})

var routes = require('./api/routes/intgErrorRoutes.js');
routes(app);

app.use((req, res) => {
    res.status(404).send({url: req.originalUrl + ' not found'});
  });

app.listen(port);

console.log('todo list RESTful API server started on: ' + port);