import logo from './logo.svg';
import './App.css';
import React, { Component } from 'react';
import Main from './components/main'
export default class App extends Component {

  constructor(props) {
    super(props);
    this.state = {
      arr: this.english_arr,
      color: 'black',
      lan: "ENGLISH",
      size: 15,
      font: "Serif"
    }
  }
  SetStyle = (type, item) => {
    switch (type) {
      case "color":
        this.setState({ color: item })
        break;
      case "size":
        this.setState({ size: item })
        break;
      case "font":
        this.setState({ font: item })
    }
  }
  render() {
    return (
      <div className="App">
        <div class="container">
          <div class="row">
            <div class="col">col</div>
            <div class="col">col</div>
            <div class="col">col</div>
            <div class="col">col</div>
          </div>
          <div class="row">
            <div class="col-8">col-8</div>
            <div class="col-4">col-4</div>
          </div>
        </div>

      </div>
    );
  }
}

