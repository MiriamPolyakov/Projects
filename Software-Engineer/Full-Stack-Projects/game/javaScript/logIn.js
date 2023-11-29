function addToGamers(logInFirstName) {// מגדיר מערך לכל המשתמשים
    arr = localStorage.getItem('gamers')
    let gamer = [{
        "ursername": logInFirstName//השחקן הנוכחי
    }]

    if (!arr) {
        localStorage.setItem('gamers', JSON.stringify(gamer));//מעדכן משתנה נוכחי
    }

    else {
        arr = JSON.parse(arr);//אם הוא השחקן הראשון שנכנס למערך
        arr.push(gamer[0]);
        localStorage.setItem('gamers', JSON.stringify(arr));
        document.getElementById('login').setAttribute('action', 'instructions.html')
        document.getElementById('signin').setAttribute('action', 'instructions.html')
        document.getElementById("loginlink").disabled = true;
    }
}

function viewSignIn() {
    document.getElementById("signInForm").style.display = 'initial';
    document.getElementById("loginForm").style.display = 'none';
}

function viewLogIn() {
    document.getElementById("loginForm").style.display = 'initial';
    document.getElementById("signInForm").style.display = 'none';
}

function logIn() {//
    let logInFirstName = document.getElementById('LogInFirstName').value;
    let logInPassword = document.getElementById('LogInPassword').value;

    let arrString = localStorage.getItem('signed');
    let arr = JSON.parse(arrString);

    if (!arr) {// אם המערך ריק
        alert("you have to sign up");
    }
    else {
        let find = arr.find(user => {//בודק התאמה
            return (user.userName === logInFirstName && user.password === logInPassword)
        })
        if (!find) {
            alert("you have to sign up");
        }
        else {
            addToGamers(logInFirstName);
        }
    }
}

function signIn() {
    let logInFirstName = document.getElementById('FirstName').value;
    let logInPassword = document.getElementById('Password').value;

    let arrayLogIn = [{//מערך של השחקן החדש
        "userName": logInFirstName,
        "password": logInPassword
    }]
    let arr = localStorage.getItem('signed');

    if (arr) {
        arr = JSON.parse(arr);
        find = arr.find(user => {//בודק אם עדיין אינו
            return (user.userName === logInFirstName && user.password === logInPassword)
        })
        if (find) {
            alert("not availible")
        }
        else {
            arr.push(arrayLogIn[0]);
            localStorage.setItem('signed', JSON.stringify(arr));
            addToGamers(logInFirstName);
        }
    }

    if (!arr) {
        localStorage.setItem('signed', JSON.stringify(arrayLogIn));
        addToGamers(logInFirstName);
    }
}

function logOut() {
    localStorage.removeItem('gamers');
    document.getElementById("loginlink").disabled = false;
}


function disablelogin() {
    document.getElementById("loginlink").disabled = true;
}

function gamersIn() {
    if (localStorage.getItem('gamers'))
        return true;
    return false;
}

function changeLinks() {
    if (gamersIn()) {
        let imges = document.getElementsByClassName("linkers");
        for (let i = 0; i < imges.length; i++) {
            imges[i].setAttribute("href", "./connect4.html");
        }
    }
}
