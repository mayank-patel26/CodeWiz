const express = require("express")
const mongo = require("mongodb").MongoClient
const app = express()
/*add get post methods here*/
app.listen(3000, () => console.log("Server ready"))
const url = "mongodb://localhost:27017"

app.use(express.json())
let db

mongo.connect(
  url,
  {
    useNewUrlParser: true,
    useUnifiedTopology: true,
  },
  (err, client) => {
    if (err) {
      console.error(err)
      return
    }
	console.log("Connected!")
    //db = client.db("DBName")
  }
)