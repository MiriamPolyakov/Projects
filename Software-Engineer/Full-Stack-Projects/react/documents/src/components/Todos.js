import React, { useEffect, useState } from 'react'
import InnerRouter from "./InnerRouter";

export default function Todos() {
    const [data1, setData] = useState([]);
    debugger
const Id=JSON.parse(window.localStorage.getItem('current_user')).id;
    async function getTodos() {
        try {
            let data = await fetch('https://jsonplaceholder.typicode.com/todos?userId='+Id)
            let result = await data.json();
            setData(result)
        }
        catch (error) {
            console.log(error)
        }
    }
    useEffect(() => {
        getTodos()
    }, [])
    debugger

    function ExecutionOf(e) {
        debugger
        switch (e.target.value) {
            case "ביצוע":
                A()
                break;
            case 'סדרתי':
                B()
                break;
            case 'אלפביתי':
                C()
                break;
            case 'אקראי':
                D()
                break;
            default:
                break;
        }
    }
    function A() {
        let a = [];
        let b = [];
        let c = [...data1]
        for (let i = 0; i < c.length; i++) {
            let x = c[i].completed
            if (x)
                a.push(c[i]);
            else
                b.push(c[i]);
        }
        for (let i = 0; i < b.length; i++)
            a.push(b[i]);
        setData(...[a])
    }
    function B() {
        let a = [], b = [...data1]
        let index;
        for (let i = 0; i < data1.length; i++) {
            index = b[i].id;
            a[index] = b[i];
        }
        setData(...[a])
    }
    function C() {
        let a = [...data1]
        for (let i = 0; i < a.length; i++) {
            for (let j = 0; j < a.length-i-1; j++) {
                if (a[j].title > a[j+1].title) {
                    debugger
                    let temp = a[j+1]
                    a[j+1] = a[j]
                    a[j] = temp
                }
            }
        }
        setData(...[a])
    }
    function D() {
        let a = [], b = [...data1]
        let rand,rand1;
        for (let i = 0; i < data1.length; i++) {
            rand = Math.random() * b.length;
            rand1 = Math.floor(rand);
            a[i] = b[rand1];
            b.splice(rand1, 1)
        }
        setData(...[a]);
        debugger
    }


    return (
        <>
            <h1>Choose an order</h1>
            <select onChange={(e) => ExecutionOf(e)}>
                <option>סדרתי</option>
                <option >ביצוע</option>
                <option>אלפביתי</option>
                <option>אקראי</option>
            </select>
            {data1.map((item) => <div className="Todos">

                <h2>userId:{item.userId}</h2>
                <h2>id:{item.id}</h2>
                <h2>title:{item.title}</h2>
                <h2>completed:</h2>
                <input type="checkbox" checked={item.completed}></input>
                <h2>--------------</h2>

            </div>)}

        </>
    );
}
