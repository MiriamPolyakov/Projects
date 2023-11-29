import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

import { getBabysitterLink } from '../links'
import home from '../img/home.png'




export default function AddReqest() {

    let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));

    let navigate = useNavigate();
    const [ok, setOk] = useState(false);
    const [notOk, setNotOk] = useState(false)

    function handleSubmit(event) {
        debugger
        event.preventDefault();
        Add(event);
    }

    const Add = async (e) => {
        debugger

        try {

            let person = {
                id: currentUser.ID,
                hours: e.target.hours.value,
                time: e.target.time.value,
                date: e.target.date.value,
                children: e.target.children.value,
                comments: e.target.comments.value,
                exception: false,
                reason: ''
            }

            let result = await fetch(`${getBabysitterLink()}/addReqest`, {
                method: 'POST', headers: { 'Content-Type': 'application/json' },
                body: JSON.stringify(person)
            });
            debugger
            if (result.status === 200) {
                setOk(true);
                setTimeout(() => {
                    navigate('/main');
                }, 2000);
            }
            else {
                setNotOk(true);
                setTimeout(() => {
                    navigate('/main');
                }, 5000);
            }



        } catch (msg) {
            alert(msg)
        }

    }

    function checkPoints() {
        let currentPoints = window.localStorage.getItem("currentPoints")
        console.log(currentPoints)
        if (currentPoints < -10) {
            setNotOk(true);
            setTimeout(() => {
                navigate('/main');
            }, 5000);
        }
    }

    useEffect(() => {
        checkPoints()
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
        <>
            <div className="back">
            {!ok && !notOk && <input  type="image" src={home} onClick={() => navigate('/main')} className="getHome"></input>}
                <div id="ccc" className="center" >
                    <h1>Add a reqest</h1>
                    {ok && <h1>Your request was added to the system successfully 😁!!</h1>}
                    {notOk && <div> <h1>Sorry, You have exceeded the limit of hours 😢</h1><h1>if it is an exceptional case, contact your system administrator for a exception...</h1></div>}
                    {!ok && !notOk &&
                        <form onSubmit={handleSubmit}>
                            <div class="inputbox">
                                <input type="date" placeholder="date" name="date" required></input></div>
                            <div className="inputbox"><input type="time" placeholder="time" name="time" required></input></div>
                            <div className="inputbox"><input type="number" placeholder="amount of hours" name="hours" required></input></div>
                            <div className="inputbox"><input type="number" placeholder="amount of children" name="children" required></input></div>
                            <div className="inputbox"><input type="text" placeholder="comments" name="comments"></input></div>
                            <div className="inputbox"> <input type="submit"></input></div>
                        </form>}

                </div>
            
            </div >

        </>
    );
}



{/* <div class="center">
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