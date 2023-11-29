import { Routes, Route } from 'react-router-dom'
import LogIn from './LogIn';
import Home from './Home'


export default function Router() {
    return (
        <div className="App">
            <Routes>
                <Route exact element={<LogIn />} path='/LogIn'></Route>
                <Route exact element={<LogIn />} path='/'></Route>
                <Route exact element={< Home/>} path='/Home/*'></Route>
            </Routes>
        </div>
    );
}

