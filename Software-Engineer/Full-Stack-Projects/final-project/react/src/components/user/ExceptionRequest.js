import React, { useState } from 'react'
import { useNavigate } from "react-router-dom";
import AddReqest from '../user/AddReqest';
import {getBabysitterLink} from '../links'
import home from '../img/home.png'



export default function ExceptionRequest() {
    let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));
    let navigate = useNavigate();

    async function sendException(event) {
        let details = {
            id: currentUser.ID,
            date: event.target.date.value,
            time: event.target.time.value,
            hours: event.target.hours.value,
            children: event.target.children.value,
            comments: event.target.comments.value,
            exception: true,
            reason: event.target.reason.value
        }
        try {
            let response = await fetch(`${getBabysitterLink()}/exception`, {
                method: 'POST', headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(details), mode: 'cors'
            });
        } catch (err) {
            console.log(err)
        }
    }

    function handelSubmit(e) {
        sendException(e);
        setSend(true)
        setTimeout(() => {
            navigate('/main');
        }, 4000)

    }




    const [send, setSend] = useState(false);

    return (
        <div className="back">
        <input  type="image" src={home} onClick={() => navigate('/main')} className="getHome"></input>
        <div class="center">
            <h1>Enter detail:</h1>
            {send && <div className="login-box">
                <h1>Your request has been sent to the system !!</h1>
            </div>}
            {!send && <form onSubmit={handelSubmit}>
            <div className="inputbox"><input type="date" placeholder="date" name="date" required></input>  </div>
            <div className="inputbox">  <input type="time" placeholder="time" name="time" required></input>  </div>
            <div className="inputbox">   <input type="number" placeholder="amount of hours" name="hours" required></input>  </div>
            <div className="inputbox">   <input type="number" placeholder="amount of children" name="children" required></input>  </div>
            <div className="inputbox">  <input type="text" placeholder="comments" name="comments"></input>  </div>
                <br />
                <div className="inputbox">    <textarea  id="textA" placeholder='Reason for request' name="reason" rows="4" cols="50" required> 
                </textarea> </div>
                <br />
                <div className="inputbox">    <input type="submit" value="Submit" />  </div>
            </form>}
           
        </div>

        </div>

    );
}


{/* <div >
  <h1>Our Newsletter</h1>
  <form>
    <div class="inputbox">
      <input type="text" required="required">
      <span>Email</span>
    </div>
    <div class="inputbox">
      <input type="text" required="required">
      <span>Password</span>
    </div>
    <div class="inputbox">
      <input type="button" value="submit">
    </div>
  </form>
</div> */}
