require('dotenv').config();

let config = {
    dbUrl: process.env.DATABASE_URL,
};

module.exports = config;