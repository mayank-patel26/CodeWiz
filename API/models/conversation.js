const mongoose = require('mongoose');
let Schema = mongoose.Schema;

const conversationSchema = new Schema({
    members:{
        type: Array,
    }
},
{
    collection: "conversation"
}); 

module.exports = mongoose.model("Conversation", conversationSchema);