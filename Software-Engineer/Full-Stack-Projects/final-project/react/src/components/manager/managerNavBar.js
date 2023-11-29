import React, { useEffect, useState } from "react";
import { NavLink } from 'react-router-dom'
//import '../Nav/navBar.css';


export default function NavBar(props) {
    debugger
    let currentUser = window.localStorage.getItem("currentUser");
    currentUser = JSON.parse(currentUser);
    //  const [points, setPoints] = useState(currentUser.Personal_Hours);

    // async function getPoints() {
    //     try {
    //         debugger
    //         let points = await fetch(`http://localhost:8080/api/user/${currentUser.ID}/getPoints`)
    //         points = await points.json();
    //         setPoints(points[0].Personal_Hours);
    //     }
    //     catch (err) {
    //         alert("filed to get points")
    //     }
    // }


    // useEffect(() => {
    //     getPoints()
    //     console.log("+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++")
    // }, [])


    return (

        <header id="Home">
            <nav class="flexbox-header" ><br />
                <div class="dot">
                    <div className="content"><h2></h2></div>
                    <div className="navbar">
                        <div className="navbar-logo">

                        </div>
                        <div id="navWrapper">
                            <ul class="active" className="navbar-social">
                                {/* <NavLink activeclassname="active" className="nav-link" exact="true" to="/main/babysitter/reqests">see all reqests</NavLink>{" "}<br />
                            <NavLink activeclassname="active" className="nav-link" exact="true" to="/main/babysitter/myReqests">see my reqests</NavLink><br />
                            <NavLink activeclassname="active" className="nav-link" exact="true" to="/main/babysitter/myCommitment">see my commitments</NavLink><br /> */}
                                <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/logOut">log out</NavLink>{" "}
                                <NavLink style={{ color: "#26dbe7" }} activeclassname="active" className="navigation" className="nav-link" exact="true" to="/main">pass as a user</NavLink>
                                <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/manager/signInReqests">see sign in requests</NavLink>
                                <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/manager/exceptionRequests">see exception requests</NavLink>
                                <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/manager/members">members</NavLink>
                                <NavLink activeclassname="active" className="nav-link" className="navigation" exact="true" to="/main/manager/sendEmail">email</NavLink>


                                {/* <NavLink activeclassname="active" className="nav-link" exact="true" to="/main/logOut">log out</NavLink>{" "}<br /> */}
                            </ul>
                        </div>
                    </div>
                </div>

            </nav>
        </header>
    );
}

