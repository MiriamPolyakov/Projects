import React from 'react'
import { useNavigate } from 'react-router';


export default function LogIn() {
    window.localStorage.removeItem('current_user')
    let navigate=useNavigate();

    async function checkUser(dataUser) {
        
        try {
            dataUser.preventDefault();
            let data = await fetch('https://jsonplaceholder.typicode.com/users');
            let response = await data.json();
            console.log(response);
            debugger
            let user = {
                username: dataUser.target.username.value,
                password: dataUser.target.password.value
            }

            let userData = response.filter((item) => {
                if (item.username === user.username && user.password === item.address.geo.lat.slice(-4))
                    return item;
            });
            debugger
            if (!userData[0]) {
                throw "you are not exist"
            }
            else {
                window.localStorage.setItem("current_user",JSON.stringify(userData[0]))
                navigate("/home")
            }
            console.log(user.username);

        }
        catch (error) {
            alert(error)
        }
    }

    return (
        <form onSubmit={(dataUser) => checkUser(dataUser)}>
            <input name="username" type="text" placeholder="Enter your name" required />
            <input name="password" type="password" placeholder="Enter your password" required />
            <input type="submit" value="LogIn" />
        </form>
    );
}
