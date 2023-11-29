const express = require('express')
const router = express.Router();
const managerServie = require('../services/managerServer')
const Joi = require('joi');
router.get('/exceptionRequests', async (req, res) => {
    debugger
    console.log("--------------------------------------")
    console.log("exceptionRequests 1 ")
    console.log("--------------------------------------")
    try {
        console.log('in exceptionRequests requests 1');
        let data = await managerServie.getAllBabysitterExceptionRequests();
        console.log("--------------------------------------")
        console.log("out exceptionRequests 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})
router.get('/members', async (req, res) => {
    debugger
    console.log("--------------------------------------")
    console.log("members 1 ")
    console.log("--------------------------------------")
    try {
        console.log('in members requests 1');
        let data = await managerServie.getMembers(req.query);
        console.log("--------------------------------------")
        console.log("out members 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})

router.put('/approveExceptionRequest', async (req, res) => {

    try {
        console.log("--------------------------------------")
        console.log('in approveExceptionRequest requests 1');
        console.log("--------------------------------------")
        if (!req.body)
            throw "ERROR---missing details"
        let data = await managerServie.approveBabysitterException(req.body.id);
        console.log("--------------------------------------")
        console.log("out approveExceptionRequest 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})

router.get('/signInReqests', async (req, res) => {
    try {
        console.log("--------------------------------------")
        console.log('in signInReqests requests 1');
        console.log("--------------------------------------")
        let data = await managerServie.getAllSignInReqests();
        console.log("--------------------------------------")
        console.log("out signInReqests 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})
router.post('/approveSignInReqest', async (req, res) => {
    try {
        console.log("--------------------------------------")
        console.log('in approveSignInReqest requests 1');
        console.log("--------------------------------------")
        if (!req.body)
            throw "ERROR---missing details"
        let data = await managerServie.approveSignInReqest(req.body);
        console.log("--------------------------------------")
        console.log("out approveSignInReqest 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }

})
router.delete('/rejectSignInReqest', async (req, res) => {
    try {
        console.log("--------------------------------------")
        console.log('in rejectSignInReqest requests 1');
        console.log("--------------------------------------")
        if (!req.body)
            throw "ERROR---missing details"
        let data = await managerServie.rejectSignInReqest(req.body);
        console.log("--------------------------------------")
        console.log("out rejectSignInReqest 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }

})
router.post('/sendMail', async (req, res) => {

    try {
        console.log("=======================================================")
        console.log("in sendMail 1")
        console.log("=======================================================")
        console.log(req.body)
        if (!req.body)
            throw "ERROR---missing details"
        let data = await managerServie.sendMail(req.body);
        console.log("=======================================================")
        console.log("leave sendMail 1")
        console.log("=======================================================")
        res.send(null)
    }
    catch (err) {
        res.send(err)
    }
})
router.put('/removeMember', async (req, res) => {

    try {
        console.log("--------------------------------------")
        console.log('in removeMember requests 1');
        console.log("--------------------------------------")
        if (!req.body)
            throw "ERROR---missing details"
       await managerServie.removeMember(req.body);
        console.log("--------------------------------------")
        console.log("out removeMember 1 ")
        console.log("--------------------------------------")
        res.send(null)
    }
    catch (err) {
        res.send(err)
    }
})
router.get('/members/searchName', async (req, res) => {
    try {
        console.log("--------------------------------------")
        console.log('in signInReqests requests 1');
        console.log("--------------------------------------")
        let data = await managerServie.searchName(req.query);
        console.log("--------------------------------------")
        console.log("out signInReqests 1 ")
        console.log("--------------------------------------")
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})

module.exports = router;