import React, { useEffect, useState } from 'react'
import InnerRouter from "./InnerRouter";
import Home from './Home';
import '../App.css'
import BigPost from './BigPost'
import { NavLink, Route, Routes } from 'react-router-dom';

export default function Posts() {
    const [data1, setData] = useState([]);
    const Id = JSON.parse(window.localStorage.getItem('current_user')).id;
    async function Posty() {
        try {
            let data = await fetch('https://jsonplaceholder.typicode.com/posts?userId=' + Id);
            let response = await data.json();
            setData(response);
        }
        catch (error) {
            alert(error)
        }
    }
    useEffect(()=>{
        Posty()
    }, [])

   
    return (
        <div>
            {data1.map((item) => <div className="Todos">
                <NavLink exact to={`/user${item.userId}/post${item.id}`}  activeClassName='active'>{item.title}</NavLink>
                <Routes>
                    <Route data={item} path={`/post/${item.id}`} exact element={<BigPost />}></Route>
                </Routes>
                <h2>--------------</h2>
            </div>)}
        </div>
    );
    
}