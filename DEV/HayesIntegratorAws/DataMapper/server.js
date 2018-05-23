'use strict';

const Hapi = require('hapi');
const server = new Hapi.Server();

server.connection({
  host: '0.0.0.0',
  port: process.env.PORT || 8080
});

server.register([
  require('inert'),
  {
    register: require('hapi-router'),
    options: {
      routes: 'routes.js'
    }
  }
], (err) => {
  if (err) {
    throw err;
  }

  server.ext('onPostHandler', (request, reply) => {
    const response = request.response;
    if (response.isBoom && response.output.statusCode === 404) {
      return reply.file('docs/global.html');
    }
    return reply.continue();
  });

  server.start((err) => {
    if (err) {
      throw err;
    }
    console.log(`[${new Date()}] Server started at ${server.info.uri}`);
  });
});
