import React from 'react'
import { BrowserRouter, NavLink } from "react-router-dom";
import InnerRouter from "./InnerRouter";
import { Routes, Route } from 'react-router-dom'
import Info from './Info'
import Todos from './Todos'
import Albums from './Albums'
import Posts from './Posts'

import LogIn from './LogIn'

export default function Home() {
   let user = JSON.parse(window.localStorage.getItem('current_user'))

   debugger
   return (
      <div>
         <h1>{user.name}</h1>
         <InnerRouter />
         <Routes>
            <Route exact element={<Info />} path='/Info'></Route>
            <Route exact element={<Todos />} path='/Todos'></Route>
            <Route exact element={<Posts />} path='/Posts'></Route>
            <Route exact element={<Albums />} path='/Albums'></Route>
            <Route exact element={<LogIn />} path='/LogIn'></Route>
         </Routes>

      </div>
   )
}