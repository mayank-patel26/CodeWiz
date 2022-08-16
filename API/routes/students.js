const express = require("express");
const Student = require("../models/student");
const router = express.Router();
const bcryptjs = require("bcryptjs");
const student = require("../models/student");
const { body, validationResult } = require("express-validator");
const config = require("../config");
var jwt = require('jsonwebtoken');
const jsecret = config.jwt_secret;

//Creating one student (WEBSITE)
router.post(
  "/register",
  [body("email").isEmail(), body("password").isLength({ min: 6 })],
  async (req, res) => {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return res.status(400).json({ errors: errors.array() });
    }
    try {
      let student = await Student.findById(req.body.username);
      if (student) {
        return res.status(400).json({ error: "Student already exists" });
      }
      const time = new Array(3).fill(0).map(() => new Array().fill(0));
      const incat = new Array(3).fill(0).map(() => new Array().fill(0));
      const score = [];
      defaultMentor = "";
      let lvl = {
        time: time,
        score: score,
        incat: incat,
        badges: null,
        helpReq: false,
        mentorUser: defaultMentor
      };
      const studentNew = new Student({
        _id: req.body.username,
        password: (await bcryptjs.hash(req.body.password, 10)).toString(),
        email: req.body.email,
        fullname: req.body.fullname,
      });

      for (let i = 0; i < 7; i++) {
        studentNew.level.push(lvl);
      }

      try {
        const newStudent = await studentNew.save();
        //jwt
        const data = {
          user: {
            _id: studentNew._id
          }
        }
        // const authToken = jwt.sign(data, jsecret)
        // res.json({authToken: authToken})

        // normal response
        res.status(201).json(newStudent);
      } catch (err) {
        res.status(400).json({ message: err.message });
      }
    } catch (err) {
      res.status(400).json({ message: err.message });
    }
  }
);

//Login (WEBSITE AND UNITY)
router.post("/login", async (req, res) => {
  try {
    const student = await Student.findById(req.body.username).clone();
    if (student == null) {
      return res.status(404).json({ message: "Cannot find student" });
    } else {
      bcryptjs.compare(req.body.password, student.password, (err, response) => {
        if (err) {
          res.status(501).json({ message: err.message });
        }
        if (response) {
          res.send(student);
        } else {
          res.status(401).json({ message: "Incorrect password" });
        }
      });
    }
  } catch (err) {
    res.status(500).json({ message: err.message });
  }
});

//Get particular lvl for a particular student
router.get("/:username/:lvl", async (req, res) => {
  try {
    const student1 = await Student.findById(req.params.username).clone();
    if (student1 == null) {
      return res.status(404).json({ message: "Cannot find student" });
    } else {
      res.send(student1.level[req.params.lvl - 1]);
    }
  } catch (err) {
    res.status(500).json({ message: err.message });
  }
});

//UPDATE FROM UNITY
router.post("/:username/:lvl", async (req, res) => {
  try {
    const student1 = await Student.findById(req.params.username).clone();
    if (student1 == null) {
      return res.status(404).json({ message: "Cannot find student" });
    } else {
      student1.level[req.params.lvl - 1].time = req.body.time;
      student1.level[req.params.lvl - 1].incat = req.body.incat;
      student1.level[req.params.lvl - 1].score = req.body.score;
      student1.level[req.params.lvl - 1].badges = req.body.badges;
      student1.level[req.params.lvl - 1].helpReq = req.body.helpReq;
      student1.level[req.params.lvl - 1].mentorUser = req.body.mentorUser;
      //if only new badge is sent
      // for(let i=0;i<req.body.badges.length;i++){
      //   student1.badges.push(req.body.badges[i])
      // }
      student1.badges = req.body.badges
      const id = req.params.username;
      const updatedLevel = student1.level;
      const option = { new: true };

      const result = await Student.updateOne(
        { _id: id },
        { $set: { level: updatedLevel} },
        option
      );
      res.send(student1);
    }
  } catch (err) {
    res.status(500).json({ message: err.message });
  }
});

//Getting all students (WEBSITE)
router.get("/", async (req, res) => {
  try {
    const students = await Student.find().clone();
    res.json(students);
  } catch (err) {
    res.status(400).json({ message: err.messages });
  }
});

//Getting one student (WEBSITE)
router.post("/getStudent", async (req, res) => {
  const student = await Student.findById(req.body.username).clone();
  if (student == null) {
    res.status(404).json({ message: "Cannot find student" });
  } else {
    res.status(201).send(student);
  }
});

module.exports = router;
