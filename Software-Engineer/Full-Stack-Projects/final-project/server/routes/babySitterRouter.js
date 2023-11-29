const express = require('express')
const router = express.Router();
const babysitterServie = require('../services/babySitterServer')

router.get('/requests', async (req, res) => {
    debugger
    try {
        console.log('in babysitter requests 1');
        let data = await babysitterServie.getAllBabysitterReqests(req.query);
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})
router.get('/myHistory', async (req, res) => {
    debugger
    try {
        console.log('in babysitter requests 1');
        let data = await babysitterServie.getMyHistory(req.query);
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})


router.get('/:id/myCommitments', async (req, res) => {
    debugger
    try {
        console.log('in myCommitments requests 1');
        console.log(req.params.id);
        let data = await babysitterServie.getAllmyBabysitterCommitments(req.params.id);
        console.log(data);

        console.log('out myCommitments requests 1');
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})


router.put('/myCommitments/cancle', async (req, res) => {

    try {
        console.log("=======================================================")
        console.log("in cancle 1")
        console.log("=======================================================")
        console.log(req.body)
        if (!req.body)
            throw "ERROR---missing details"
        let data = await babysitterServie.cancleBabysitter(req.body);
        console.log("=======================================================")
        console.log("leave cancle 1")
        console.log("=======================================================")
        res.send(data);
    }
    catch (err) {
        res.send(err)
    }
})

router.put('/tookReqest', async (req, res) => {
    debugger
    try {
        console.log("in tookReqest 1")
        if (!req.body)
            throw "ERROR---missing details"
        await babysitterServie.tookBabysitterReqest(req.body);//?
        console.log("leave tookReqest 1")
        res.send(null);
    }
    catch (err) {
        res.send(err)
    }
})


router.post('/addReqest', async (req, res) => {

    try {
        console.log("??????????????????????      ")
        console.log("=======================================================")
        console.log("in addReqest 1")
        console.log("=======================================================")
        console.log(req.body)
        if (!req.body)
            throw "ERROR---missing details"
        let ans = await babysitterServie.addBabysitterReqest(req.body);
        console.log("??????????????????????      " + ans)
        if (!ans)
            res.status(400).json(null);
        else {
            res.status(200).json(null);
            console.log("=======================================================")
            console.log("leave addReqest 1")
            console.log("=======================================================")
        }


    }
    catch (err) {
        res.send(err)
    }
})
router.get('/myRequests/:id', async (req, res) => {
    debugger
    try {
        console.log("=======================================================")
        console.log('in mmyyyyyyrequests requests 1');
        console.log("=======================================================")
        if (!req.body)
            throw "ERROR---missing details"
        let data = await babysitterServie.getMyBabysitterReqests(req.params.id);
        console.log(data)
        res.send(data)
    }
    catch (err) {
        res.send(err)
    }
})


// router.get('/getRequest/:id', async (req, res) => {
//     debugger
//     try {
//         console.log("=======================================================")
//         console.log('in getReqest requests 1');
//         console.log("=======================================================")
//         let data = await userServie.getReqest(req.params.id);
//         console.log(data)
//         res.send(data)
//     }
//     catch (err) {
//         res.send(err)
//     }
// })

router.delete('/deleteRequest', async (req, res) => {
    try {
        console.log("=======================================================")
        console.log('in deleteRequests requests 1');
        console.log("=======================================================")
        if (!req.body)
            throw "ERROR---missing details"
        await babysitterServie.deleteMyBabysitterReqest(req.body);
        console.log("=======================================================")
        console.log('outdeleteRequests requests 1');
        console.log("=======================================================")
        res.send(null)
    }
    catch (err) {
        res.send(err)
    }
})

router.put('/myRequests/:id/updataRequest', async (req, res) => {
    try {
        console.log("=======================================================")
        console.log('in updataRequest requests 1');
        console.log("=======================================================")
        if (!req.body)
            throw "ERROR---missing details"
        await babysitterServie.updataBabysitterRequest({ body: req.body, id: req.params.id });
        console.log("=======================================================")
        console.log('updataRequest requests 1');
        console.log("=======================================================")

        res.send(null)
    }
    catch (err) {
        res.send(err)
    }
})

router.post('/exception', async (req, res) => {

    try {
        console.log("=======================================================")
        console.log("in exception 1")
        console.log("=======================================================")
        console.log(req.body)
        if (!req.body)
            throw "ERROR---missing details"
        await babysitterServie.addBabysitterReqest(req.body);
        console.log("=======================================================")
        console.log("leave exception 1")
        console.log("=======================================================")

    }
    catch (err) {
        res.send(err)
    }
})
router.get('/searchReqests', async (req, res) => {
    debugger
    try {
        console.log("=======================================================")
        console.log('in mmyyyyyyrequests requests 1');
        console.log("=======================================================" )
        let data = await babysitterServie.searchReqests(req.query);
        console.log(data)
        res.send(data)
    }
    catch (err) {
        res.status(500).send(err)
    }
})

module.exports = router;

// router.get(`/:id/MyRequests`, async (req, res) => { //////חליצת יוזר
//     debugger
//     // req.email
//     try {
//         console.log('my')
//         res.send('MyBabysitterRequests')
//     }
//     catch (err) {
//         res.send(err)
//     }
// })

// router.post('/addRequest', async (req, res) => {//להוסיף בקשה
//     debugger
//     try {
//         console.log('hiii')
//         console.log(req)//?req
//     }
//     catch (err) {
//         res.send(err)
//     }
// })
