const db = require('./DB')


async function checkLogIn(user) {
    let result = await db.query(`select * from babyExchange.members where Mail='${user.email}'`); ///
    console.log(result)
    return result;
}

async function checkSignIn(user) {
    let result = await db.query(`select * from babyExchange.members  where Mail='${user.email}'`);
    console.log(result)
    return result;
}

async function signIn(user) {
    try {
        await db.query(`insert into babyExchange.signIn value (default,'${user.name}','${user.email}',${user.password},'${user.address}')`);
        console.log("in sign in function")
    } catch (err) {
        console.log(err)
    }
}

async function getPoints(id) {
    let result = await db.query(`select Personal_Hours from babyExchange.members where id=${id}`);
    console.log(result)
    return result;
}

module.exports = {
    checkLogIn,
    checkSignIn,
    signIn,
    getPoints
}

