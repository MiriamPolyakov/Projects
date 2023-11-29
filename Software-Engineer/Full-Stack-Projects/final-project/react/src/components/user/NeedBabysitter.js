// import { Routes, Route } from 'react-router-dom'
// import React from 'react';


// export default function NeedBabysitter() {
//   let currentUser = JSON.parse(window.localStorage.getItem("currentUser"));

//   function handleSubmit(event) {
//     event.preventDefault();
//     isValid(event);
//   }

//   async function isValid(event) {
//     try {
//       console.log(event)
//       let ask = {
//         // name:currentUser.name,
//         //   email:currentUser.email,
//         exception: false,
//         date: event.target[0].value,
//         time: event.target[1].value,
//         hours: event.target[2].value,
//         children: event.target[3].value,
//         Comments: event.target[4].value
//         //!
//       }
//       debugger
//       //post???????צריך רק לעדכן את הנתונים לטבלת הבקשות
//       await fetch('http://localhost:8080/api/babySitter/addRequest', {
//         method: 'POST', headers: { 'Content-Type': 'application/json' },
//         body: JSON.stringify(ask), mode: 'cors'
//       });
//     } catch (err) {
//       console.log(err);
//     }
//   }

//   return (
//     <div className="login-box">
//       <h2>complete details:</h2>
//       <form onSubmit={(event) => handleSubmit(event)}>
//         <div className="user-box">
//           <input type="date" name="date" required></input>
//           <input type="time" placeholder="time" name="time"></input>
//           <input type="number" placeholder="amount of hours" name="hours"></input>
//           <input type="number" placeholder="amount of children" name="children"></input>
//           <input type="text" placeholder="comments" name="comments"></input>
//           <input type="submit" id="sub" /><span /> <span /> <span /> <span />
//         </div>
//       </form>
//     </div>
//   )
// }