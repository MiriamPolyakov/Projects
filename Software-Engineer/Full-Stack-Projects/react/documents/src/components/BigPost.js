import React, { useEffect, useState } from 'react'
import InnerRouter from "./InnerRouter";
import Home from './Home';
import '../App.css'
import { NavLink, Route, Routes } from 'react-router-dom';
import Posts from './Posts'


export default function BigPost(props) {
    debugger
    const [commentsData, setCommentsData] = useState()
    const data1 = props.data
    let booly = false;
    // const Id = JSON.parse(window.localStorage.getItem('current_user')).id;
    async function comments(id) {
        booly = !booly;
        if (booly) {
            try {
                let data = await fetch('https://jsonplaceholder.typicode.com/comments?postId=' + id);
                let response = await data.json();
                setCommentsData(response);

            }
            catch (error) {
                alert(error)
            }
        }
    }
    return (
        <div>
            <h2>userId:{data1.userId}</h2>
            <h2>id:{data1.id}</h2>
            <h2>title:{data1.title}</h2>
            <h2>body:{data1.body}</h2>
            <h3>To view comments:</h3>
            <input type="checkbox" onChange={() => comments(data1.id)}></input>
            {booly && commentsData.map((item) => {
                <div>
                    <h3>name:{item.name}</h3>
                    <h3>email:{item.email}</h3>
                    <h3>body:{item.body}</h3>
                </div>
            })}
        </div>
    );

}








