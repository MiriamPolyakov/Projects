const db = require('./DB')
const email = require('../email')
const config = require('../config')

async function getAllBabysitterExceptionRequests() {
    //!
    try {
        console.log("--------------------------------------")
        console.log("in getAllExceptionRequests 2")
        console.log("--------------------------------------")
        let data = await db.query("select bs.ID_reqest,m.Personal_Hours,bs.exceptionReason, bs.b_date , bs.b_time, bs.hours, bs.childrenamount,bs.comments, m.lastname, m.mail, m.address  from babyExchange.babysitterReqests bs join members m on m.id=bs.id_memberask where bs.exception is true and B_Date>=CURDATE()");////and exception = false
        console.log("--------------------------------------")
        console.log("out getAllExceptionRequests 2")
        console.log("--------------------------------------")
        return data;
    } catch (err) {
        console.log(err)
    }
}

async function approveBabysitterException(id) {
    //!
    try {
        console.log("--------------------------------------")
        console.log("in approveException 2")
        console.log("--------------------------------------")
        await db.query(`update babyExchange.babysitterreqests set exception = 0 where id_reqest = ${id}`);////and exception = false
        console.log("--------------------------------------")
        console.log("out approveException 2")
        console.log("--------------------------------------")
        return getAllExceptionRequests()
    } catch (err) {
        console.log(err)
    }
}
async function getAllSignInReqests() {
    //!
    try {
        console.log("--------------------------------------")
        console.log("in approveException 2")
        console.log("--------------------------------------")
        let data = await db.query(`select * from babyExchange.signIn`);////and exception = false
        console.log("--------------------------------------")
        console.log("out approveException 2")
        console.log("--------------------------------------" + data)
        return data;
    } catch (err) {
        console.log(err)
    }
}
async function approveSignInReqest(user) {
    //!

    try {
        console.log("--------------------------------------")
        console.log("in approveException 2")
        console.log("--------------------------------------")
        let data = await db.query(`insert into babyExchange.members value(default,'${user.LastName}','${user.Mail}',${user.M_password},'${user.Address}',default,default)`);////and exception = false
        await db.query(`delete from babyExchange.signIn where ID_signIn =${user.ID_signIn}`)
        console.log("--------------------------------------")
        console.log("out approveException 2")
        console.log("--------------------------------------" + data);
        const item = {
            managerEmail: config.emailAcount.email,//////////////////////////////////////????????????????????????????????
            managerEmailPassword: config.emailAcount.emailPassword,//
            receiver: user.Mail,
            html: `<h1 >Hello ${user.LastName}!!!</h1><h2>we are glad to conform you that your reqest has been accepted and you have joined our system!</h2><h2>have a good day😊</h2>`,
            subject: 'answer from babyExchange system'
        }
        email.sendEmail(item);
        return getAllSignInReqests();
    } catch (err) {
        console.log(err)
    }
}
async function rejectSignInReqest(details) {
    //!

    try {
        await db.query(`delete from babyExchange.signIn where ID_signIn =${details.ID_signIn}`)
        console.log("--------------------------------------")
        console.log("out approveException 2")
        console.log("--------------------------------------")
        const item = {
            managerEmail: config.emailAcount.email,//////////////////////////////////////????????????????????????????????
            managerEmailPassword: config.emailAcount.emailPassword,///////////////////////////////////????????????????????????????????
            receiver: details.Mail,
            html: `<h1>Hello ${details.LastName},</h1><h2>we are sorry to let you know that your reqest to join our system has been reject ...</h2><h2>have a good day</h2>`,
            subject: 'answer from babyExchange system'
        }
        console.log(item)
        email.sendEmail(item);
        return getAllSignInReqests();
    } catch (err) {
        console.log(err)
    }
}
async function sendMail(details) {
    //!

    try {
        console.log("???????????????????????????????  " + details)
        const item = {
            managerEmail: config.emailAcount.email,//////////////////////////////////////????????????????????????????????
            managerEmailPassword: config.emailAcount.emailPassword,//////////////////////////////////////????????????????????????????????
            receiver: details.email,
            html: `<h1>${details.text}</h1>`,
            subject: details.subject
        }
        email.sendEmail(item);
        console.log("!!!!!!!!!!!!!!!!!")
    } catch (err) {
        console.log(err)
    }
}
async function getMembers(item) {
    //!
    try {
        console.log("--------------------------------------")
        console.log("in getMembers 2")
        console.log("--------------------------------------")
        let data = await db.query(`select * from members where id!=${item.id} limit 0,${item.end}`);
        console.log("--------------------------------------")
        console.log("out getMembers 2")
        console.log("--------------------------------------")
        return data;
    } catch (err) {
        console.log(err)
    }
}

async function removeMember(item) {
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    try {
        console.log("--------------------------------------")
        console.log("in removeMember 2   "+item.ID)
        console.log("--------------------------------------")
        console.log(`delete from babyExchange.babysitterreqests  where ID_memberAsk=${item.ID}`)
        console.log(`delete from babyExchange.members where id=${item.ID}`)
        await db.query(`delete from babyExchange.babysitterreqests  where ID_memberAsk=${item.ID}`)
        await db.query(`delete from babyExchange.members where mail='${item.Mail}'`)
        console.log("--------------------------------------")
        console.log("out removeMember 2")
        console.log("--------------------------------------")
    } catch (err) {
        console.log(err)
    }
}
async function searchName(item) {
    //!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
    try {
       console.log(item.name+"+++++++++++++++++"+item.id)
       let data= await db.query(`select * from babyExchange.members where lastname like '${item.name}%' and id!=${item.id}`)
        console.log("--------------------------------------")
        console.log("out removeMember 2")
        console.log("--------------------------------------")
        return data
    } catch (err) {
        console.log(err)
    }
}

module.exports = {
    getAllBabysitterExceptionRequests,
    approveBabysitterException,
    getAllSignInReqests,
    approveSignInReqest,
    rejectSignInReqest,
    sendMail,
    getMembers,
    removeMember,
    searchName
}