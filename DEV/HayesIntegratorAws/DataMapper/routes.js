'use strict';

module.exports = [{
  method: 'GET',
  path: '/{param*}',
  config: {
    auth: false
  },
  handler: {
    directory: {
      path: ['docs'],
      listing: false,
      redirectToSlash: true,
      index: true
    }
  }
}];
