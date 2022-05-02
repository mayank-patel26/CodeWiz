const express = require("express");
const User = require("../models/user");
const router = express.Router();

//Creating one
router.post("/", async (req, res) =>{
    const user = new User({
      username: req.body.username,
      password: req.body.password,
    });

    try{
        const newUser = await user.save();
        res.status(201).json(newUser);
    } catch (err){
        res.status(400).json({ message: err.message});
    }
});

//Getting all
router.get("/", async (req, res) => {
    try{
        const users = await User.find();
        res.json(users);
    } catch(err) {
        res.status(400).json({message: err.messages});
    }
});

//Getting one
router.get("/:id", getUser, (req, res) => {
  res.json(res.user);
  });

  //Updating one
  router.put("/:id", getUser, async (req, res) => {
      if(req.body.username != null){
        res.user.username = req.body.username;
      }
      if(req.body.password != null){
        res.user.password = req.body.password;
      }
      try{
        const updatedUser = await res.user.save();
        res.json(updatedUser);
      } catch(err) {
        res.status(400).json({message: err.message});
      }
  });
  
  //Deleting one
  router.delete("/:id", getUser, async (req, res) => {
    try{
      await res.user.remove();
      res.json({message: "Deleted User"});
    } catch(err){
      res.status(500).json({message: err.message});
    }
  });

  async function getUser(req, res, next){
    try{
      user = await User.findById(req.params.id);
      if(user == null){
        return res.status(404).json({message: "cannot find user"});
      }
    } catch(err) {
      return res.status(500).json({message: err.message});
    }

    res.user = user;
    next();
  }
  
  module.exports = router;