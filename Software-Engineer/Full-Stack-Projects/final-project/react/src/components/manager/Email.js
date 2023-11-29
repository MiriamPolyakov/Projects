import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";

import {getManagerLink} from '../links'



export default function Email() {
    let navigate = useNavigate();

    async function handleSubmit(event) {
        let item = {
            email: event.target.email.value,
            text: event.target.text.value,
            subject: event.target.subject.value
        }
        let data=await fetch(`${getManagerLink()}/sendMail`, {
            method: 'POST', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(item), mode: 'cors'
        });
        navigate('/main/managerNavBar')
    }



    return (

        <div >
            <div className="todo" style={{ "border": "6px solid #5d02b3", margin: "10px", width: "300px", height: "200px", fontSize: "10px" }}>
                <h2>enter details:</h2>
                <form onSubmit={(event) => handleSubmit(event)}>
                    <div className="user-box">
                        <input name="email" placeholder="email@outlook.co.il" type="email" required />
                        <label>email</label>
                    </div>
                    <div className="user-box">
                        <input name="text" placeholder="text" type="text" required />
                        <label>text</label>
                    </div>
                    <div className="user-box">
                        <input name="subject" placeholder="subject" type="text" />
                        <label >subject</label>
                        <input type="submit" className="sub" placeholder="sent the email" /><span /> <span /> <span /> <span />
                    </div>
                </form>
            </div>
        </div >
    );
}
