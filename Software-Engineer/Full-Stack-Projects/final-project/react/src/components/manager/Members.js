
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { useRef } from "react";
import { getManagerLink } from '../links'




export default function Members() {
    let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));
    const listInnerRef = useRef();
    let navigate = useNavigate();

    const [end, setEnd] = useState(5);
    const [requests, setRequests] = useState(['']);

    const removeMember = async (item) => {
        try {
            //get 10
            debugger
            await fetch(`${getManagerLink()}/removeMember`, {
                method: 'PUT', headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(item), mode: 'cors'
            });
            debugger//?
            let data = [...requests]
            debugger
            data = data.filter(i => i.ID != item.ID)
            if (!data) throw "ERROR"
            setRequests(data);
            debugger
            console.log(data)
        } catch (msg) {
            alert(msg)
        }
    }

    const getData = async () => {
        try {
            //get 10
            debugger
            let response = await fetch(`${getManagerLink()}/members?id=${currentUser.ID}&end=${end}`, { method: 'GET' })
            debugger
            let data = await response.json()
            debugger
            if (!data) throw "ERROR"
            setRequests(data);
            debugger
            console.log(data)
        } catch (msg) {
            alert(msg)
        }
    }

    useEffect(() => {
        getData();
    }, [end])

    const searchName = async (item) => {
        try {
            //get 10
            debugger
            let response = await fetch(`${getManagerLink()}/members/searchName?name=${item.target.value}&id=${currentUser.ID}`, { method: 'GET' })
            debugger
            let data = await response.json()
            debugger
            if (!data) throw "ERROR"
            setRequests(data);
            debugger
            console.log(data)
        } catch (msg) {
            alert(msg)
        }
    }

    function onScroll() {
        if (listInnerRef.current) {
            const { scrollTop, scrollHeight, clientHeight } = listInnerRef.current;
            if (scrollTop + clientHeight === scrollHeight) {
                console.log("reached bottom");
                let tmp = end + 5
                setEnd(tmp);
            }
        }
    }
  
    return (

        <div >
            <form>
                <h2>enter family name to search:</h2>
                <input type="text" name="name" placeholder="name" onChange={searchName} ></input>
            </form>

            <div className="todo" style={{ height: "2000px", overflowY: "auto" }} onScroll={onScroll} ref={listInnerRef}>
                {requests.map(
                    item =>
                        <div key={item.ID} style={{ "border": "6px solid #5d02b3", margin: "10px", width: "300px", fontSize: "10px" }}>
                            {console.log(item)}
                            <h1><u>family name:</u> {item.LastName}</h1>
                            <h1><u>mail:</u> {item.Mail}</h1>
                            <h1><u>address: </u>{item.Address}</h1>
                            <h1><u>personal Hours: </u>{item.Personal_Hours}</h1>
                            <button onClick={() => removeMember(item)}>remove member</button>
                        </div>)}
                <input type="button" onClick={() => navigate('/main/managerNavBar')} className="getHome"></input>
            </div>
        </div >
    );
}