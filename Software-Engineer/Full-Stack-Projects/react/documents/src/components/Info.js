import React from 'react'
import InnerRouter from "./InnerRouter";
import Home from './Home';
import '../App.css'

export default function Info() {
    let user = JSON.parse(window.localStorage.getItem('current_user'))
    return (
        <div className="App">
            <h1>
                <h2>id:{user.id}</h2>
                <h2> username:{user.username}</h2>
                <h2> email:{user.email}</h2>
               <h2> -------------</h2>
                <h2> address:</h2>
                <h2>street:{user.address.street}</h2>
                <h2> suite: {user.address.suite}</h2>
                <h2> city: {user.address.city}</h2>
                <h2>zipcode: {user.address.zipcode}</h2>
                <h2>geo:</h2>
                <h2> lat:   {user.address.geo.lat}</h2>
                <h2>lng: {user.address.geo.lng}</h2>
                <h2> -----------------</h2>
                <h2>phone: {user.phone}</h2>
                <h2>website: {user.website}</h2>
                <h2> company: ---</h2>
                <h2>name:{user.company.name}</h2>
                <h2> catchPhrase:{user.company.catchPhrase}</h2>
                <h2> bs:{user.company.bs}</h2>
            </h1>


        </div>
    );
}

