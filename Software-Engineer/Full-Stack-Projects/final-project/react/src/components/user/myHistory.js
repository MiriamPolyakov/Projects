
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { useRef } from "react";
import { getBabysitterLink } from '../links'




export default function MyHistory() {
    let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));
    const listInnerRef = useRef();
    let navigate = useNavigate();

    const [end, setEnd] = useState(5);
    const [requests, setRequests] = useState(['']);

    const getData = async () => {
        try {
            //get 10
            debugger
            let response = await fetch(`${getBabysitterLink()}/myHistory?id=${currentUser.ID}&end=${end}`, { method: 'GET' })
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
            <div className="todo" style={{ height: "2000px", overflowY: "auto" }} onScroll={onScroll} ref={listInnerRef}>
                {requests.map(
                    item =>
                        <div key={item.ID_reqest} style={{ "border": "6px solid #5d02b3", margin: "10px", width: "300px", fontSize: "10px" }}>
                            {console.log(item)}
                            <h1><u>date: </u>{item.B_Date}</h1>
                            <h1><u>time:</u> {item.B_Time}</h1>
                            <h1><u>amount of hours:</u> {item.hours}</h1>
                            <h1><u>amount of children:</u> {item.childrenAmount}</h1>
                            {item.comments ? <h1><u>comments:</u> {item.comments}</h1> : <br />}
                            {console.log(item.ID_memberRecieve)}
                            <h1><u>status:</u> {item.ID_memberRecieve ? '👍' : '👎'}</h1>
                        </div>)}
                <input type="button" onClick={() => navigate('/main')} className="getHome"></input>
            </div>
        </div >
    );
}