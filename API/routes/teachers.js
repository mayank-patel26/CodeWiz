const express = require("express");
const Teacher = require("../models/teacher");
const router = express.Router();
const bcryptjs = require('bcryptjs');

//register
router.post("/register", async(req, res)=>{
    const teacher = new Teacher({
        _id: req.body.username,
        password: (await bcryptjs.hash(req.body.password, 10)).toString(),
        email: req.body.email,
        fullname: req.body.fullname,
        subject: req.body.subject
      });
  
      try{
          const newTeacher = await teacher.save();
          res.status(201).json(newTeacher);
      } catch (err){
          res.status(400).json({ message: err.message});
      }
})

router.get("/login", getTeacher, async (req, res) => {
    bcryptjs.compare(req.body.password, teacher.password, (err, valid) =>{
      if(!valid){
        res.status(401).json({message: "Incorrect password"})
      }
      res.status(200).json({username: teacher._id})
    //   res.status(200).json({username: teacher.username})
      if(err){
        res.status(501).json({message: err.message})
      }
    })
  });


//middleware for login
async function getTeacher(req, res, next){
    try{
      teacher = await Teacher.findById(req.body.username).clone();
      if(teacher == null){
        return res.status(404).json({message: "Cannot find teacher"});
      }
    } catch(err) {
      return res.status(500).json({message: err.message});
    }

    res.teacher = teacher;
    next();
  }

module.exports = router;