
import React, { Component } from 'react';


export default class Keyboard extends Component {
    numbers_arr = ["‼", 1, 2, 3, 4, 5, 6, 7, 8, 9, 0, "-", "+", "="]
    hebrew_arr = ["ק", "ר", "א", "ט", "ו", "ן", "ם", "פ", "ש", "ד", "ג", "כ", "ע", "י", "ח", "ל", "ך", "ף", "ז", "ס", "ב", "ה", "נ", "מ", "צ", "ת", "ץ", "."]
    english_arr = ["q", "w", "e", "r", "t", "y", "u", "i", "o", "p", "a", "s", "d", 'f', "g", "h", "j", "k", "l", "z", "x", "c", "v", "b", "n", "m", "[", "]"]
    language = ["English", "עברית", "UPPERCASE", "lower-case", "🤣 💯 ❤ ✌"]
    images = ['😊', '🤣', '❤', '😍', '💋', '🙌', '🤷‍♀️', '😜', '👌', '😁', '👀', '💯', '✌', '💃', '👲', '👵', '🎗', '🥎', '🍿', '🥩', '🍔', '🍖', '💔', '💟', '❗', '❕', '💥', '💌']
    constructor(props) {
        super(props);
        this.state = {
            arr: this.english_arr,
            new_arr: []
        }
        this.set_lan = this.set_lan.bind(this)
    }

    set_lan(item) {
        switch (item) {
            case "English":
                this.setState({ arr: this.english_arr })
                break;
            case "עברית":
                this.setState({ arr: this.hebrew_arr })
                break;
            case "UPPERCASE":
                { this.state.new_arr = this.english_arr.map((item) => item.toUpperCase()) }
                this.setState({ arr: this.state.new_arr })
                break;
            case "lower-case":
                { this.state.new_arr = this.english_arr.map((item) => item.toLowerCase()) }
                this.setState({ arr: this.state.new_arr })
                break;
            case "🤣 💯 ❤ ✌":
                { this.state.new_arr = this.images.map((item) => item.toLowerCase()) }
                this.setState({ arr: this.state.new_arr })
                break;
        }
    }
    render() {
        return (
            <div >
                <div className="keyboard">
                    {this.numbers_arr.map((item) => <button key={item} className="key" key={item} onClick={() => this.props.pushWord(item)} style={{ width: 50, height: 50, fontSize: 20 }}>{item}</button>)}
                    {this.state.arr.map((item) => <button key={item} className="key" key={item} onClick={() => this.props.pushWord(item)} style={{ width: 50, height: 50, fontSize: 20 }}>{item}</button>)}
                    {this.language.map((item) => <button key={item} className="keys" key={item} onClick={() => this.set_lan(item)} >{item}</button>)}
                </div>
            </div>
        )
    }
}