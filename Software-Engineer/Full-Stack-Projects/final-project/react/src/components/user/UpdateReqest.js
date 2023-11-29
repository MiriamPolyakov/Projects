import { useEffect, useState } from 'react';
import {getBabysitterLink} from '../links'


export default function UpdataRequest(props) {
    let details = props.item;
    console.log("item" + details)
    console.log("++++++++++++++++++++++++++++++++++++++++")
    console.log("id" + props.item.id)
    debugger
    // let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));
    // const [data,setData]=useState({})

    // const [requests, setRequests] = useState([''])

    async function update(e) {
        let request = {
            hours: e.target.hours.value,
            time: e.target.time.value,
            date: e.target.date.value,
            children: e.target.children.value,
            comments: e.target.comments.value
        }
        console.log(e)
        debugger
        let result = await fetch(`${getBabysitterLink()}/myRequests/${props.item.ID_reqest}/updataRequest`, {
            method: 'PUT',
            headers: { 'Content-Type': 'application/json' },
            body: JSON.stringify(request), mode: 'cors'
        });
        // res = await res.json()
        debugger
        // setRequests(res)////////??????????????????????
    }

    return (

        <div >
            <div className="todo">
                <form onSubmit={(e) => update(e)}>
                    <input type="date" placeholder="date" name="date" defaultValue={details.b_date} required></input>
                    <input type="time" placeholder="time" name="time" defaultValue={details.b_time} required></input>
                    <input type="number" placeholder="amount of hours" defaultValue={details.hours} name="hours" required></input>
                    <input type="number" placeholder="amount of children" defaultValue={details.childrenamount} name="children" required></input>
                    <input type="text" placeholder="comments" name="comments" defaultValue={details.comments}></input>
                    <input type="submit" ></input>
                </form>
            </div>
        </div >
    );
}
