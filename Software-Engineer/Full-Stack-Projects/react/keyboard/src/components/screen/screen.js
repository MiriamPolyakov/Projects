import React, { Component } from 'react';
import './screen.css'
export default class Screen extends Component {
    render() {
        return (
            <article>
                <div className="stand">
                    <div className="monitor">
                        {this.props.word.map((letter, i) => <span key={i} style={{ color: letter.color, fontSize: letter.size, fontFamily: letter.fontFamily }}>{letter.value}</span>)}
                    </div>
                </div>
            </article>
        )
    }
}

