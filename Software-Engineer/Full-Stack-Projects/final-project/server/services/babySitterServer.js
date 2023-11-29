const db = require('./DB')
const email = require('../email')
const config=require('../config')

async function getAllBabysitterReqests(item) {
    //!
    let data = await db.query(`select bs.ID_reqest, bs.id_memberask, bs.b_date , bs.b_time, bs.hours, bs.childrenamount,bs.comments, m.lastname, m.mail, m.address 
    from babyExchange.babysitterReqests bs join members m on m.id=bs.id_memberask where bs.ID_memberRecieve is null and bs.exception is false and bs.ID_memberAsk !=${item.idAsk} and B_Date>=CURDATE() limit 0,${item.end}`);////and exception = false
    return data;
}

async function getAllmyBabysitterCommitments(id) {
    let data = await db.query(`select bs.ID_reqest, bs.id_memberask, bs.b_date , bs.b_time, bs.hours,
    bs.childrenamount,bs.comments, m.lastname, m.mail, m.address  from babyExchange.babysitterReqests bs join babyExchange.members m on m.id=bs.ID_memberAsk
     where  ID_memberRecieve=${id} and bs.b_date>=curdate()`);
    return data;
}

async function getMyBabysitterReqests(id) {
    let data = await db.query(`select bs.id_reqest,bs.id_memberask, bs.ID_memberRecieve, bs.b_date , bs.b_time, bs.hours, bs.childrenamount,bs.comments, m.lastname, m.mail 
    from babyExchange.babysitterReqests bs left join members m on m.id=bs.ID_memberRecieve where ID_memberAsk=${id} and bs.exception is false and B_Date>=CURDATE()`);
    return data;//{data:data,response:response}
}

async function cancleBabysitter(req) {
    console.log("in tookReqest 2 ")
    await db.query(`UPDATE babyExchange.babysitterreqests SET ID_memberRecieve = null WHERE ID_reqest = ${req.item.ID_reqest}`);
    console.log("req !!!!!!!!!!!!!!!!!!!!!!!!!!!!   " + req.item)
    let points = req.item.hours;
    if (req.item.childrenamount > 3 && req.item.childrenamount < 6) {
        points = req.item.hours * 1.5;
    }
    else if (req.item.childrenamount > 6) {
        points = req.item.hours * 2;
    }
    await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours =m.Personal_Hours-${points} where m.ID=${req.cID}`);
    await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours =m.Personal_Hours+${points} where m.id=${req.item.id_memberask}`);
    console.log("req.item.mail  " + req.item.mail)
    const item = {
        managerEmail: config.emailAcount.email,
        managerEmailPassword: config.emailAcount.emailPassword,
        receiver: req.item.mail,
        html: `<h1>Hello ${req.item.lastname},</h1><h2>we are sorry to let you know that your reqest to a babyExchange in ${req.item.b_date} at ${req.item.b_time} is back to be on hold and waiting for a reciever again!!</h2><h2>have a good day😊</h2>`,
        subject: 'important!!! cancle from babyExchange system'
    }
    console.log(item)
    email.sendEmail(item);
    console.log("leave tookReqest 2 ");
    // let data = await getAllmyCommitments(req.cID);
}
async function AllBabysitterReqests(item) {
    //!
    let data = await db.query(`select bs.ID_reqest, bs.id_memberask, bs.b_date , bs.b_time, bs.hours, bs.childrenamount,bs.comments, m.lastname, m.mail, m.address 
    from babyExchange.babysitterReqests bs join members m on m.id=bs.id_memberask where bs.ID_memberRecieve is null and bs.exception is false and bs.ID_memberAsk !=${item} and B_Date>=CURDATE()`);////and exception = false
    return data;
}
async function tookBabysitterReqest(user) {
    console.log("in tookReqest 2 ")

    console.log(user)
    await db.query(`UPDATE babyExchange.babysitterreqests SET ID_memberRecieve = ${user.cID} WHERE ID_reqest = ${user.id_reqest}`);

    let details = await db.query(`select id_memberask,ID_memberRecieve, childrenAmount,hours from babyExchange.babysitterReqests where ID_reqest=${user.id_reqest}`);
    console.log("--------------------------------------")
    console.log("details " + details[0]);
    console.log("--------------------------------------")
    let points = details[0].hours;
    console.log("--------------------------------------")
    console.log("points ??????????????????????????????????????????" + details[0].hours);
    console.log("--------------------------------------")
    if (details[0].childrenAmount > 3 && details[0].childrenAmount < 6) {
        points = details[0].hours * 1.5;
    }
    else if (details[0].childrenAmount > 6) {
        points = details[0].hours * 2;
    }
    console.log("--------------------------------------")
    console.log("points !!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!" + details[0].hours);
    console.log("--------------------------------------")
    await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours = m.Personal_Hours+${points} where m.ID=${user.cID}`);//////
    await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours = m.Personal_Hours-${points} where m.ID=${details[0].id_memberask}`);////m.Personal_Hours-
    console.log("leave tookReqest 2 ");
    let data = await AllReqests(user.cID);
    return data;
}
async function addBabysitterReqest(user) {
    console.log("in addReqest 2 ")//????
    let points = await db.query(`select Personal_Hours, ID,exception  from babyExchange.members join babyExchange.babysitterReqests where ID=${user.id}`);
    console.log(user);
    console.log("--------------------------------------")
    console.log("Personal_Hours[] " + points[0].Personal_Hours);
    console.log("--------------------------------------")
    if (points[0].Personal_Hours < -10 && !user.exception) {//!
        console.log("--------------------------------------")
        console.log("in -10")
        return 0;
    }

    await db.query(`insert into babyExchange.babysitterreqests value (default,${points[0].ID},default,'${user.date}','${user.time}',${user.hours},${user.children},'${user.comments}',${user.exception},'${user.reason}')`);//!
    console.log("--------------------------------------")
    console.log("leave addReqest 2 ");
    return 1;
}


async function deleteMyBabysitterReqest(items) {
    console.log("in deleteMyReqest 2 ")//????
    console.log(items);
    console.log("--------------------------------------")
    console.log("--------------------------------------")
    let points = items.hours;
    console.log("--------------------------------------")
    if (items.ID_memberRecieve) {
        if (items.childrenamount > 3 && items.childrenamount < 6) {
            points = items.hours * 1.5;
        }
        else if (items.childrenamount > 6) {
            points = items.hours * 2;
        }
        await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours =m.Personal_Hours+${0.8 * points} where m.ID=${items.id_memberask}`);
        await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours =m.Personal_Hours-${0.8 * points} where m.ID=${items.ID_memberRecieve}`);

        const item = {
            managerEmail: config.emailAcount.email,//////////////////////////////////////????????????????????????????????
            managerEmailPassword: config.emailAcount.emailPassword,///////////////////////////////////////????????????????????????????????
            receiver: items.mail,
            html: `<h1>Hello ${items.lastname},</h1><h2>we are sorry to let you know that your comitment to do the babyExchange in ${items.b_date} at ${items.b_time} has been cancled and you are free of doing it!!</h2><h2>have a good day😊</h2>`,
            subject: 'important!!! cancle from babyExchange system'
        }
        email.sendEmail(item);
    }

    await db.query(`delete from babyExchange.babysitterreqests where ID_reqest = ${items.id_reqest}`);
    console.log("leave deleteMyReqest 2 ");
    // return await getMyReqests(items.id_memberask);
}

async function updateBabysitterRequest(req) {
    const item = req.body;
    console.log("in updataRequest 2 ")
    console.log(req)
    console.log("--------------------------------------")
    let res = await db.query(`UPDATE babyExchange.babysitterreqests SET b_date='${item.date}',b_time='${item.time}',hours=${item.hours},childrenamount=${item.children},comments='${item.comments}' WHERE ID_reqest = ${req.id}`);/////set להכל!!!

    console.log("leave updataRequest 2 ");
    let points = req.item.hours;
    if (req.item.childrenamount > 3 && req.item.childrenamount < 6) {
        points = req.item.hours * 1.5;
    }
    else if (req.item.childrenamount > 6) {
        points = req.item.hours * 2;
    }
    await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours =m.Personal_Hours-${points} where bs.ID_memberRecieve=${req.cID}`);
    await db.query(`UPDATE babyExchange.babysitterreqests bs join babyExchange.members m on bs.ID_memberAsk=m.ID SET m.Personal_Hours =m.Personal_Hours+${points} where bs.id_memberask=${req.item.id_memberask}`);
    // לשלוח מייל או הודעה שהשתנה
    let data = await getAllmyBabysitterCommitments(req.cID);
    return data;
}

async function getBabysitterReqest(id) {
    let data = await db.query(`select  bs.b_date , bs.b_time, bs.hours, bs.childrenamount,bs.comments from babyExchange.babysitterReqests bs where ID_reqest=${id} and B_Date>=CURDATE()`);
    console.log("data:" + data)
    return data;
}
async function addBabysitterException(data) {
    await db.query(`insert into babyExchange.babysitterreqests value (default,${data.ID},default,'${data.date}','${data.time}',${data.hours},${data.children},'${data.comments}')`);
    console.log("--------------------------------------")
    console.log("leave addException 2 ");
}
async function searchReqests(item) {
    console.log(item)

    let data = await db.query(`select bs.ID_reqest, bs.id_memberask, bs.b_date , bs.b_time, bs.hours, bs.childrenamount,bs.comments, m.lastname, m.mail, m.address 
    from babyExchange.babysitterReqests bs join members m on m.id=bs.id_memberask where bs.ID_memberRecieve is null and bs.exception is false and bs.ID_memberAsk !=${item.id} and b_date='${item.date}'
     and B_Date>=CURDATE()`);
    console.log("data:" + data)
    return data;
}
async function getMyHistory(item) {
    //!
    try {
        console.log("--------------------------------------")
        console.log("in getMyHistory 2")
        console.log("--------------------------------------")
        let data = await db.query(`select * from babyExchange.babysitterreqests where id_memberask=${item.id} and CURDATE()>b_date limit 0,${item.end}`);
        console.log("--------------------------------------")
        console.log("out getMyHistory 2")
        console.log("--------------------------------------")
        return data;
    } catch (err) {
        console.log(err)
    }
}

module.exports = {
    getAllBabysitterReqests,
    tookBabysitterReqest,
    getAllmyBabysitterCommitments,
    cancleBabysitter,
    addBabysitterReqest,
    getMyBabysitterReqests,
    deleteMyBabysitterReqest,
    updateBabysitterRequest,
    getBabysitterReqest,
    addBabysitterException,
    searchReqests,
    getMyHistory
}