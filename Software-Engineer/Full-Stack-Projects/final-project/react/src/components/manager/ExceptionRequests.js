
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";

import {getManagerLink} from '../links'


export default function ExceptionRequests() {
    let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));

    let navigate = useNavigate();

    // async function getBabysitter(ids) {
    //     // navigate('/main/babysitter/reqests')



    //     let data = await fetch(`${getBabysitterLink()}/tookReqest`, {
    //         method: 'PUT', headers: { 'Content-Type': 'application/json' },
    //         body: JSON.stringify(ids), mode: 'cors'
    //     });
    //     data = await data.json()
    //     setRequests(data);
    //     //?????????

    //     //להוסיף שעות לי 
    //     //id להוריד לה שעות...לפי 
    // }
    const [requests, setRequests] = useState(['']);


    const getData = async () => {
        try {
            //get 10
            let response = await fetch(`${getManagerLink()}/exceptionRequests`, { method: 'GET' })
            debugger
            let data = await response.json()
            debugger
            if (!data) throw "ERROR"
            setRequests(data);
            console.log(data)
        } catch (msg) {
            alert(msg)
        }

    }
    async function approve(id) {
        try {
            debugger
            let data = await fetch(`${getManagerLink()}/approveExceptionRequest`, {
                method: 'PUT', headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify({id:id}), mode: 'cors'
            });
             data = await data.json()
            debugger
            if (!data) throw "ERROR"
            setRequests(data);
        } catch (msg) {
            alert(msg)
        }
    }

    useEffect(() => {
        getData();
    }, [])


    //   function handleSelect(e) {
    //     let d = [...toDoArr];
    //     const { value } = e.target;
    //     if (value === "random") {
    //       random_(d);
    //     }
    //     else {
    //       sort(d, value);
    //     }
    //     setToDos(d);
    //   }


    return (

        <div >
            {/* <select name="selectSort" id="sort-select" onChange={(e) => handleSelect(e)}>
        <option value="" selected disabled>--Please choose a type of sort--</option>
        <option value="title">Alphabetical</option>
        <option value="completed">completed</option>
        <option value="random">random</option>
        <option value="id">sequential</option>
      </select> */}
            <div className="todo">
                {requests.map(
                    item =>
                        <div key={item.ID_reqest} style={{ "border": "6px solid #5d02b3", margin: "10px", width: "300px", fontSize: "10px" }}>
                            {/* <h1>{item.lastName}</h1> */}
                            {console.log(item)}
                            <h1><u>family name:</u> {item.lastname}</h1>
                            <h1><u>mail:</u> {item.mail}</h1>
                            <h1><u>address: </u>{item.address}</h1>
                            <h1><u>points: </u>{item.Personal_Hours}</h1>
                            <h1>-----------------</h1>
                            <h1><u>date: </u>{item.b_date}</h1>
                            <h1><u>time:</u> {item.b_time}</h1>
                            <h1><u>amount of hours:</u> {item.hours}</h1>
                            <h1><u>amount of children:</u> {item.childrenamount}</h1>
                            {item.comments ? <h1><u>comments:</u> {item.comments}</h1> : ''}
                            <h1>-----------------</h1>
                            <h1><u>exception reason: </u>{item.exceptionReason}</h1>
                            <button type="button" className="get btn" onClick={() => approve(item.ID_reqest)} >approve this reqest</button>
                        </div>)}
                <input type="button" onClick={() => navigate('/main/managerNavBar')} className="getHome"></input>
            </div>
        </div >
    );
}