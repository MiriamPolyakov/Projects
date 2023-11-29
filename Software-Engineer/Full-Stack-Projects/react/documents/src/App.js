import React from 'react'
import { BrowserRouter, Route } from 'react-router-dom'
import Home from './components/Home';
import LogIn from "./components/LogIn";
import Router from './components/Router'

export default function App() {
  return (
    <BrowserRouter>
      <Router>
        <div className="App">
        </div>
      </Router>
    </BrowserRouter>
  );
}
