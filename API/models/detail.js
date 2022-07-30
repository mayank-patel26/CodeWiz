const { Timestamp } = require('mongodb');
const mongoose = require('mongoose');
let Schema = mongoose.Schema;

const detailSchema = new Schema({
    _id:{
        type: String, required: true
    },
    timetaken:{
        type: Number, required: true
    },
    difficulty:{
        type: String, required: true
    },
    hintsTaken:{
        type: Number, required: true
    },
    correctAns:{
        type: Number, required: true
    }
},
{
    collection: "details"
})

module.exports = mongoose.model("Detail", detailSchema);