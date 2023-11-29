import React, { Component } from "react";
import Keyboard from './keyboard'
import Items from './items'
import Screen from './screen/screen'

export default class Main extends Component {
    constructor(props) {
        super(props)
        this.state = {
            undo: [],
            output: [],
        }
    }
    pushWord = (char) => {
        const newChar = { color: this.props.style.color, size: this.props.style.size, value: char, fontFamily: this.props.style.font }
        const text1 = [... this.state.output];
        text1.push(newChar);
        this.setState({ output: text1 })
    }

    actionButtons = (title) => {
        switch (title) {
            case "space":
                this.pushWord(" ");
                break;
            case "delete":
                let text = [...this.state.output]
                const text2 = [...this.state.undo]
                text2.push(text.pop())
                this.setState({ undo: text2 })
                this.setState({ output: text });
                break;
            case "UNDO":
                if (this.state.undo.length != 0) {
                    const text_output = [... this.state.output];
                    const newChar = this.state.undo.pop();
                    text_output.push(newChar);
                    this.setState({ output: text_output })
                }
                break;
            case "enter":
                this.pushWord(<br></br>)
                break;
        }
    }

    setSpecial = (change) => {
        let new_arr=[]
        switch (change) {
            case "CLEAR ALL":
                this.setState({ output: [] })
                break;
            case "LOWER ALL":
               new_arr = [...this.state.output];
                {new_arr.forEach((item) =>item.value.type!="br"?  item.value = item.value.toLowerCase():'')};
                this.setState({ output: new_arr })
                break;
            case "UPPER ALL":
                new_arr = [...this.state.output];
                {new_arr.forEach((item) =>item.value.type!="br"? item.value = item.value.toUpperCase():'')};
                this.setState({ output: new_arr })
                break;
        }
    }

    render() {
        return (
            <div>
                <Screen word={this.state.output} ></Screen>
                <Keyboard pushWord={this.pushWord} />
                <Items actionButtons={this.actionButtons} SetStyle={this.props.SetStyle} special={this.setSpecial} />
            </div>

        )
    }
}





