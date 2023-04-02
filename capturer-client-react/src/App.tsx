import React, { Component } from 'react';
import 'bootstrap';
import { WordsList } from './WordsList';
import logo from './logo.svg';
import './App.scss';

class App extends Component {
	render() {
		return (
			<div className="App">
				<div id="divMessages">
    			</div>
				<header className="App-header">
					<div className="container-sm">
						<img src={logo} className="App-logo" alt="logo" />
						<WordsList name="Pedro Gurin" />
					</div>
				</header>
			</div>
		);
	}
}

export default App;
