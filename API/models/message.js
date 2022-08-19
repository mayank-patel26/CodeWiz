const mongoose = require('mongoose');
let Schema = mongoose.Schema;

const messageSchema = new Schema({
    conversationId:{
        type: String,
    },
    sender: {
        type: String
    },
    text: {
        type: String
    }
},
{
    collection: "message"
}); 

module.exports = mongoose.model("Message", messageSchema);