import React from 'react'
import { Routes, Route } from 'react-router-dom'
import { BrowserRouter, NavLink } from "react-router-dom";
import Home from './Home'
import '../App.css'

export default function InnerRouter() {
    debugger
    return (
        <nav>
            {/* <NavLink exact to='/Home' activeClassName='active'>Home</NavLink> */}
            <NavLink className="nav_link" exact to='/Home/Info' activeClassName='active'>Info</NavLink>
            <NavLink className="nav_link" exact to='/Home/Albums' activeClassName='active'>Albums</NavLink>
            <NavLink className="nav_link" exact to='/Home/Posts' activeClassName='active'>Posts</NavLink>
            <NavLink className="nav_link" exact to='/Home/Todos' activeClassName='active'>Todos</NavLink>
            <NavLink className="nav_link" exact to='/LogIn' activeClassName='active'>LogOut</NavLink>
        </nav>
    );
}

