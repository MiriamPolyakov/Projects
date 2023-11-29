import React, { useEffect, useState } from "react";
import { NavLink } from 'react-router-dom'
import './nav.css';
import logo from '../img/logo3.png';


export default function NavBar(props) {
    debugger
    let currentUser = window.localStorage.getItem("currentUser");
    currentUser = JSON.parse(currentUser);
    const [points, setPoints] = useState(currentUser.Personal_Hours);

    async function getPoints() {
        try {
            debugger
            console.log()
            let points = await fetch(`http://localhost:8080/api/user/${currentUser.ID}/getPoints`)
            points = await points.json();
            setPoints(points[0].Personal_Hours);
            window.localStorage.setItem("currentPoints", points[0].Personal_Hours)
        }
        catch (err) {
            alert("filed to get points")
        }
    }


    useEffect(() => {
        getPoints()
        console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    }, [])


    return (
        <>
            <header id="Home">
                <nav class="flexbox-header" ><br />
                    <div class="dot">
                        <div className="content"><h2></h2></div>
                        <div className="navbar">
                            <div className="navbar-logo">

                            </div>
                            <div id="navWrapper">
                                <ul class="active" className="navbar-social">

                                    <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/logOut">log out</NavLink>{" "}
                                    {currentUser.manager ? <NavLink style={{ color: "##ffae00" }} className="navigation" activeclassname="active" className="nav-link" exact="true" to="/main/managerNavBar">pass as a manager</NavLink> : ''}
                                    <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/babysitter/myCommitment">see my commitments</NavLink>
                                    <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/babysitter/exceptionRequest">exception request</NavLink>
                                    <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/babysitter/myReqests">see my reqests</NavLink>
                                    <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/babysitter/addReqest">add reqest</NavLink>
                                    <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/babysitter/reqests">see all reqests</NavLink>
                                    <NavLink activeclassname="active" className="nav-link" class="navigation" exact="true" to="/main/babysitter/history">my history</NavLink>
                                    <h2 id="my_name" >My points: {points}</h2>
                                </ul>
                            </div>
                        </div>

                    </div>
                </nav>
            </header>
            <img src={logo} id="logo"></img>

        </>
    );
}







//   return (
//     <header id="Home">
//       <nav class="flexbox-header">
//         {isSpecificUser ||<a onClick={()=>{_props.setIsLogIn(true)}} className="navLink" class="navigation" >LogIn  </a>}{"  "}
//         {isSpecificUser ||<a onClick={()=>{_props.setIsSignIn(true)}} className="navLink" class="navigation" >SignIn  </a>}{"  "}
//         <NavLink className="navLink" class="navigation" exact='true' to="/" onClick={logOut}>Log Out</NavLink>{" "}
//         {isSpecificUser &&<a  className="navLink" class="navigation" >My Oreders Status  </a>}{"  "}
//         <div class="dot"></div>
//       </nav>
//     </header>
//   );
// }


// {isSpecificUser || <NavLink className="navLink" class="navigation" exact='true' to="/home/logIn" >LogIn  </NavLink>}{"  "}
// {isSpecificUser || <NavLink className="navLink" class="navigation" exact='true' to="/home/signIn">SignIn  </NavLink>}{"  "}
// {<a onClick={_props.updateShopingCart} className="navLink" class="navigation" >My Shopping Cart  </a>}{"  "}












