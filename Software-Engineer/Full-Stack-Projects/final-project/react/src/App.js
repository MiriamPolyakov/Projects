import './App.css';
//import '../node_modules/bootstrap/dist/css/bootstrap.css';
import { BrowserRouter } from 'react-router-dom';
import Router from './components/Router';
//import LogIn from './components/LogIn';

function App() {
  return (
    <BrowserRouter>
      <div className="App">
        <Router/>
        {/* <LogIn/> */}
      </div>
    </BrowserRouter>
  );
}

export default App;