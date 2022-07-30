const mongoose = require('mongoose');
let Schema = mongoose.Schema;

const teacherSchema = new Schema({
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
        type: String, required: true
    },
    subject:{
        type: String, required: true
    }
},
{
    collection: "teachers"
});

let Teacher = mongoose.model("Teacher", teacherSchema);

module.exports = Teacher;