const express = require("express");
const app = express();
const cors = require("cors");
const bodyParser = require("body-parser");
const logger = require("morgan");
const mongoose = require("mongoose");

const port = 3031;
const config = require("./config");

const usersRouter = require("./routes/users");

app.use(logger("dev"));

const dbUrl = config.dbUrl;

var options = {
  keepAlive: true,
  connectTimeoutMS: 30000,
  useNewUrlParser: true,
  useUnifiedTopology: true,
};

mongoose.connect(dbUrl, options, (err) => {
  if (err) console.log(err);
  else console.log("Connected to Database");
});

app.use(cors());
app.use(bodyParser.urlencoded({ extended: true }));
app.use(bodyParser.json());
app.use("/users", usersRouter);

app.listen(port, function () {
  console.log("Runnning on " + port);
});

module.exports = app;