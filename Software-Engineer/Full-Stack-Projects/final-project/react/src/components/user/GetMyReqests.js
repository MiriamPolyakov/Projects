
import { useEffect, useState } from 'react';
import UpdataRequest from './UpdateReqest';
import { useNavigate } from "react-router-dom";
import { getBabysitterLink } from '../links'


export default function GetMyRequests() {
  let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));
  let navigate = useNavigate();
  const [requests, setRequests] = useState([''])
  const [details, setDetails] = useState([''])
  const [item, setItem] = useState(null)
  const [count, setCount] = useState(0)
  // ID_reqest: 70
  // address: "bar ilan 14"
  // b_date: "20-08-2000"
  // b_time: "22:30"
  // childrenamount: 2
  // comments: ""
  // hours: 1
  // id_memberask: 2
  // lastname: "binn"
  // mail: "b@gmail.com"

  async function deleteRequest(it) {
    debugger
    let res = await fetch(`${getBabysitterLink()}/deleteRequest`, {
      method: 'DELETE', headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(it), mode: 'cors'
    });
    debugger
    let arr = [...requests]
    arr = arr.filter(item => item.id_reqest != it.id_reqest)
    setRequests(arr)
  }
  function updateRequest(i) {
    debugger
    console.log(i)
    console.log("@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@")
    setItem(i);
    debugger
    // let res = await fetch(`http://localhost:8080/api/babySitter/${item.ID_reqest}/updataRequest`, { method: 'UPDATE', })
    // res = await res.json()
    // debugger
    // setRequests(res)
  }

  const getData = async () => {

    try {

      let response = await fetch(`${getBabysitterLink()}/myRequests/${currentUser.ID}`, { method: 'GET' })
      debugger
      response = await response.json();
      // let res1 = response.data
      // let res2 = response.res
      debugger
      if (!response) throw "ERROR in getting list"
      setRequests(response);
      // setDetails(res2);

    } catch (msg) {
      alert(msg)
    }
  }
  useEffect(() => {
    getData();
  }, [])

  // useEffect(() => {

  // }, item)

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
      {item && <UpdataRequest item={item} />}
      <div className="todo" >
        {!item && requests.map(
          item =>
            <div className="item" key={item.id} style={{ "border": "6px solid #5d02b3", "height": "600px", margin: "10px" }}>
              {console.log(item)}
              <h1><u>date: </u>{item.b_date}</h1>
              <h1><u>time:</u> {item.b_time}</h1>
              <h1><u>amount of hours:</u> {item.hours}</h1>
              <h1><u>amount of children:</u> {item.childrenamount}</h1>
              {item.comments ? <h1><u>comments:</u> {item.comments}</h1> : <br />}
              {console.log(item.ID_memberRecieve)}
              <h1><u>status:</u> {item.ID_memberRecieve ? '👍' : '👎'}</h1>
              {item.ID_memberRecieve ? <h1><u>name:</u> {item.lastname}</h1> : <br />}
              {item.ID_memberRecieve ? <h1><u>mail:</u> {item.mail}</h1> : <br />}


              <button onClick={() => deleteRequest(item)} className="sub get">delete</button>
              {!item.ID_memberRecieve ? <button onClick={() => updateRequest(item)} className="sub get">update</button> : ''}

            </div>)}
        <input type="button" onClick={() => navigate('/main')} className="getHome"></input>
      </div>
    </div >
  );
}