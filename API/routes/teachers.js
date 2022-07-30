const express = require("express");
const Teacher = require("../models/teacher");
const router = express.Router();
const bcryptjs = require("bcryptjs");
const { body, validationResult } = require("express-validator");

//register
router.post(
  "/register",
  [body("email").isEmail(), body("password").isLength({ min: 8 })],
  async (req, res) => {
    const errors = validationResult(req);
    if (!errors.isEmpty()) {
      return res.status(400).json({ errors: errors.array() });
    }
    try {
      let teacher = await Teacher.findById(req.body.username);
      if (teacher) {
        res.status(400).json({ error: "Teacher already exists" });
      } else {
        const teacherNew = new Teacher({
          _id: req.body.username,
          password: (await bcryptjs.hash(req.body.password, 10)).toString(),
          email: req.body.email,
          fullname: req.body.fullname,
          subject: req.body.subject,
        });

        try {
          const newTeacher = await teacherNew.save();
          res.status(201).send(newTeacher);
        } catch (err) {
          res.status(400).json({ message: err.message });
        }
      }
    } catch (err) {
      res.status(400).json({ message: err.message });
    }
  }
);

router.post("/login", checkTeacher, async (req, res) => {
  try {
    bcryptjs.compare(
      req.body.password,
      teacherCheck.password,
      function (err, response) {
        if (err) {
          res.status(400).json({ message: err.message });
        }
        if (response) {
          res.send(teacherCheck);
        } else {
          res.status(400).json({ message: "incorrect password" });
        }
      }
    );
  } catch (err) {
    res.status(400).json({ message: err.message });
  }
});

async function checkTeacher(req, res, next) {
  try {
    teacherCheck = await Teacher.findById(req.body.username);
    if (!teacherCheck) {
      res.status(400).json({ error: "Teacher does not exist" });
    } else {
      res.teacherCheck = teacherCheck;
      next();
    }
  } catch (err) {
    res.status(400).json({ message: err.message });
  }
}

module.exports = router;
