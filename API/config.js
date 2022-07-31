require('dotenv').config();

let config = {
    dbUrl: process.env.DATABASE_URL,
    jwt_secret: process.env.JWT_SECRET
};

module.exports = config;