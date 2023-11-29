import React, { Component } from 'react';
import ReactDOM from 'react-dom';

export default class Items extends Component {
    size = [ 17, 40, 50];
    color = ["crimson", "aquamarine", "yellow", "black", "chartreuse"]
    font = ["Serif", "Sans-serif", "Monospace", "Cursive", "Fantasy"]
    special = ["CLEAR ALL", "LOWER ALL", "UPPER ALL"]
    action=["space","delete","UNDO","enter"]
    new_arr = [];
    constructor(props) {
        super(props);
    }

    render() {
        return (
            <div className="see">
                <br></br>
                {this.action.map((item)=>  <button  key={item} className="keys" onClick={() => this.props.actionButtons(item)}>{item} </button>)}
                {this.size.map((item) => <button  key={item} className="key" key={item} onClick={() => this.props.SetStyle("size",item)}>{item}</button>)}
                {this.font.map((item) => <button  key={item} className="keys" key={item} onClick={() => this.props.SetStyle("font",item)}>{item}</button>)}
                {this.special.map((item) => <button  key={item} className="keys" key={item} onClick={() => this.props.special(item)}>{item}</button>)}
                <input className="color_btn" type="color" onChange={(e) => this.props.SetStyle("color",e.target.value)} />
            </div>
        )
    }
}

