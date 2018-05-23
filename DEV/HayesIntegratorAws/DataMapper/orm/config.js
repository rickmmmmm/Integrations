const tedious = require('tedious');

const {
	config: {
		host,
		database,
		username,
		dbType
	},
	secrets: {
		password
	}
} = require('../lib/configuration');

const def = {
	database,
	host,
	username,
	password,
	dialect: dbType
};

module.exports = {
	development: def,
	production: def
};
