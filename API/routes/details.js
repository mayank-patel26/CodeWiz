const express = require("express");
const Detail = require("../models/detail");
const router = express.Router();

//updating details
router.put("/:username", checkExist, async (req, res) => {
    if(req.body.username != null){
        res.detail._id = req.body.username;
    }
    if(req.body.timetaken != null){
        res.detail.timetaken = req.body.timetaken;
    }
    if(req.body.difficulty != null){
        res.detail.difficulty = req.body.difficulty;
    }
    if(req.body.hintsTaken != null){
        res.detail.hintsTaken = req.body.hintsTaken;
    }
    if(req.body.correctAns != null){
        res.detail.correctAns = req.body.correctAns;
    }
    try{
        const updatedDetail = await res.detail.save()
        res.status(200).json(updatedDetail)
    }catch(err){
        res.status(502).json({message: err.message})
    }
})


//getting all details of all student
router.get("/", async (req, res)=>{
    try{
        const details = await Detail.find().clone()
        res.json(details)
    }catch(err){
        res.status(400).json({message: err.message})
    }
})


//middleware
async function checkExist(req, res, next){
    try{
        const detail = await Detail.findById(req.params.username).clone()
        if(detail == null){
            const detailTBA = new Detail({
                _id: req.params.username,
                timetaken: req.body.timetaken,
                difficulty: req.body.difficulty,
                hintsTaken: req.body.hintsTaken,
                correctAns: req.body.correctAns
            })
            try{
                const newDetail = await detailTBA.save();
                res.status(201).json(newDetail)
            }catch(err){
                res.status(400).json({message: err.message})
            }
        }else{
            res.detail = detail;
            next()
        }
    }catch(err){
        res.status(501).json({message: err.message})
    }
}

module.exports = router;