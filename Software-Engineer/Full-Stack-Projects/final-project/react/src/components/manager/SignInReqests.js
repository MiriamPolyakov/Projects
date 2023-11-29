import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import {getManagerLink} from '../links'




export default function SignInReqests() {
    let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));

    let navigate = useNavigate();


    async function add(reqest) {
        let data = await fetch(`${getManagerLink()}/approveSignInReqest`, {
            method: 'POST', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(reqest), mode: 'cors'
        });
        data = await data.json()
        setRequests(data);
     
    }

    async function reject(item) {
        let data = await fetch(`${getManagerLink()}/rejectSignInReqest`, {
            method: 'DELETE', headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(item), mode: 'cors'
        });
        data = await data.json()
        setRequests(data);
     
    }
    const [requests, setRequests] = useState(['']);


    const getData = async () => {
        try {
            //get 10
            let response = await fetch(`${getManagerLink()}/signInReqests`, { method: 'GET' })
            debugger
            response = await response.json()
            debugger
            if (!response) throw "ERROR"
            setRequests(response);
            console.log(response)
        } catch (msg) {
            alert(msg)
        }

    }
    useEffect(() => {
        getData();
    }, [])


    return (

        <div >
            <div className="todo">
                {requests.map(
                    item =>
                        <div key={item.ID_reqest} style={{ "border": "6px solid #5d02b3", margin: "10px", width: "300px", fontSize: "10px" }}>
                            {/* <h1>{item.lastName}</h1> */}
                            {console.log(item)}
                            <h1><u>family name:</u> {item.LastName}</h1>
                            <h1><u>mail:</u> {item.Mail}</h1>
                            <h1><u>address: </u>{item.Address}</h1>
                            <button type="button" className="get btn" onClick={() => add(item)} >add</button>
                            <button type="button" className="get btn" onClick={() => reject(item)} >reject</button>
                        </div>)}
                <input type="button" onClick={() => navigate('/main/managerNavBar')} className="getHome"></input>
            </div>
        </div >
    );
}
