const mongoose = require('mongoose');
let Schema = mongoose.Schema;

const studentSchema = new Schema({
    fullname:{
        type: String, required: true,
    },
    _id:{
        type: String, required: true,
    },
    password:{
        type: String, required: true,
    },
    email:{
        type: String, required: true, unique: true
    },
    level:{
        type: Array, required: false
    },
    followings:{
        type: Array, default: []
    },
    followers:{
        type: Array, default: []
    }
},
{
    collection: "students"
}); 

module.exports = mongoose.model("Student", studentSchema);