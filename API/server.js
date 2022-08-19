const express = require("express");
const app = express();
const cors = require("cors");
const bodyParser = require("body-parser");
const logger = require("morgan");
const mongoose = require("mongoose");

const port = process.env.PORT || 3031;
const config = require("./config");

const studentsRouter = require("./routes/students");
const teachersRouter = require("./routes/teachers");
const conversationRouter = require("./routes/conversations");
const messageRouter = require("./routes/messages");

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
app.use(express.urlencoded({ extended: true }));
app.use(express.json());


app.use("/students", studentsRouter);
app.use("/teachers", teachersRouter);
app.use("/conversations", conversationRouter);
app.use("/messages", messageRouter);

app.listen(port, function () {
  console.log("Runnning on " + port);
});

module.exports = app;