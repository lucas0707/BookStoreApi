import React, { Component } from 'react';
import logo from './logo.svg';
import './App.css';
import 'typeface-roboto';

import BookList from './Components/BookList';

class App extends Component {
  render() {
    return (
      <div className="App">
        <header className="App-header">
          <img src={logo} className="App-logo" alt="logo" />
        </header>
        <BookList/>
      </div>
    );
  }
}

export default App;
