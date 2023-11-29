const express = require('express')
const router = express.Router();
const userServie = require('../services/userServies')

//log in
router.post('/logIn', async (req, res) => {
    debugger
    try {
        console.log('------------------------------------------------------')
        console.log('log in server');
        console.log('------------------------------------------------------')
        if (!req.body)
            throw "ERROR---missing details"
        let data = await userServie.checkLogIn(req.body);
        res.send(data);
    }
    catch (err) {
        console.log(err)
    }
})

//sign in
router.post('/signIn', async (req, res) => {
    try {
        console.log('------------------------------------------------------')
        console.log('sign in server');
        console.log('------------------------------------------------------')
        if (!req.body)
            throw "ERROR---missing details"
        let data = await userServie.checkSignIn(req.body);
        console.log("data" + data)
        if (!data[0]) {
            await userServie.signIn(req.body)
            res.status(200).json(null);
        }
        else
            res.status(400).json(null);
        res.end();
    }
    catch (err) {
        console.log(err)
    }
})

router.get('/:id/getPoints', async (req, res) => {
    debugger
    try {
        console.log("=======================================================");
        console.log('in getPoints requests 1');
        console.log("=======================================================");
        console.log("id " + req.params.id);
        console.log("=======================================================");
        let data = await userServie.getPoints(req.params.id);
        console.log("=======================================================");
        console.log("data " + data);
        console.log("=======================================================");
        console.log('out getPoints requests 1');
        console.log("=======================================================");
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})




module.exports = router;
