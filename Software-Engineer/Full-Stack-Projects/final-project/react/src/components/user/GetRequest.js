
import { useEffect, useState } from 'react';
import { useNavigate } from "react-router-dom";
import { useRef } from "react";
import { getBabysitterLink } from '../links'




export default function GetRequest() {
  let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));
  const listInnerRef = useRef();
  let navigate = useNavigate();

  const [end, setEnd] = useState(5);
  const [requests, setRequests] = useState(['']);

  async function tookBabysitter(ids) {

    let data = await fetch(`${getBabysitterLink()}/tookReqest`, {
      method: 'PUT', headers: { 'Content-Type': 'application/json' },
      body: JSON.stringify(ids), mode: 'cors'
    });
    debugger
    // data = await data.json()
    let arr = [...requests]
    arr = arr.filter(item => item.ID_reqest != ids.id_reqest)
    setRequests(arr);
  }

  const getData = async () => {
    try {
      //get 10
      debugger
      let response = await fetch(`${getBabysitterLink()}/requests?idAsk=${currentUser.ID}&end=${end}`, { method: 'GET' })
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
  //   async function getData() {
  //     try {
  //         let ask = await fetch(`https://jsonplaceholder.typicode.com/photos?albumId=${idparam}&_start=${start}&_limit=10`)
  //         ask = await ask.json();
  //         let tmp = [...photos, ...ask];
  //         setPhotos(tmp);
  //     } catch (err) {
  //         console.log(err);
  //     }
  // }

  useEffect(() => {
    getData();
  }, [end])

  function onScroll() {
    if (listInnerRef.current) {
      const { scrollTop, scrollHeight, clientHeight } = listInnerRef.current;
      if (scrollTop + clientHeight === scrollHeight) {
        console.log("reached bottom");
        let tmp = end
        setEnd(tmp + 5);
      }
    }
  }



  async function search(e) {
    try {
      debugger
      let date = e.currentTarget.value;
      let response = await fetch(`${getBabysitterLink()}/searchReqests?date=${date}&id=${currentUser.ID}`, { method: 'GET' })
      debugger
      let data = await response.json()
      debugger
      if (!data) throw "ERROR"
      setRequests(data)
      console.log(data)
    } catch (msg) {
      alert(msg)
    }
    debugger
  }

  return (

    <div >
      {/* <select name="selectSort" id="sort-select" onChange={(e) => handleSelect(e)}>
        <option value="" selected disabled>--Please choose a type of sort--</option>
        <option value="title">Alphabetical</option>
        <option value="completed">completed</option>
        <option value="random">random</option>
        <option value="id">sequential</option>
      </select> */}

      <h2>enter date to search:</h2>
      <input type="date" name="date" onChange={search}></input>
      {/* <h3>enter dtails to search:</h3>
      <input type="date" name="date" onChange={date}></input> */}

      <div className="todo" style={{ height: "2000px", overflowY: "auto" }} onScroll={onScroll} ref={listInnerRef}>
        {requests.map(
          item =>
            <div key={item.ID_reqest} className="item" >
              {/* <h1>{item.lastName}</h1> */}
              {console.log(item)}
              <h1><u>family name:</u> {item.lastname}</h1>
              <h1><u>mail:</u> {item.mail}</h1>
              <h1><u>address: </u>{item.address}</h1>
              <h1>-----------------</h1>
              <h1><u>date: </u>{item.b_date}</h1>
              <h1><u>time:</u> {item.b_time}</h1>
              <h1><u>amount of hours:</u> {item.hours}</h1>
              <h1><u>amount of children:</u> {item.childrenamount}</h1>
              {item.comments ? <h1><u>comments:</u> {item.comments}</h1> : <br />}
              <button type="button" className="get btn" onClick={() => tookBabysitter({ id_reqest: item.ID_reqest, cID: currentUser.ID })} >get this babysiter</button>
            </div>)}
        <input type="button" onClick={() => navigate('/main')} className="getHome"></input>
      </div>
    </div >
  );
}