import React, { Component } from "react";
import DataTable, { TableColumn } from "react-data-table-component";

// import * as signalR from "@microsoft/signalr";

interface InputProps {
    name: string;
}

interface WordItem {
    id: string;
    word: string;
    definitions: string[];
    definition: string;
    example: string;
    translation: string;
}

interface ComponentState {
    words: WordItem[];
    hubConnection: any;
}

type DataRow = {
    word: string;
    translation: string;
}

const wordsTableColumns: TableColumn<DataRow>[] = [
    {
        name: 'Text',
        selector: row => row.word
    },
    {
        name: 'Translation',
        selector: row => row.translation
    },
];

export class WordsList extends Component<InputProps, ComponentState>
{
    constructor(props: InputProps) {
        super(props);
        this.state = {
            words: [],
            hubConnection: null
        }
    }

    componentDidMount = () => {
        this.getWords();
        /*
        const hubConnection = new signalR.HubConnectionBuilder()
			.withUrl("http://localhost:5248/api")
			.build();

        this.setState({ hubConnection: hubConnection }, () => {
            this.state.hubConnection.on("newMessageReceived", (data: any) => {
                const words = this.state.words.concat([{ id: data.Id, word: data.Word, definitions: data.Definitions }]);
                // console.log(words)
                this.setState({ words: words });
            });
    
            this.state.hubConnection
                .start()
                .catch((err: string) => document.write(err));
        });
        */
    }

    getWords() {
		let xhr = new XMLHttpRequest();
		xhr.addEventListener('load', () => {
			if (xhr.status == 200) {
                let responseResult = JSON.parse(xhr.responseText);
                this.setState(
                    {
                        words: responseResult
                    }
                );
				console.log(responseResult);
			}
		});
		xhr.open('GET', 'http://localhost:5248/api/words/');
		xhr.send();
    }
    
    render() {
        let items = this.state.words.map((word: WordItem) => 
            <div key={ word.word }>
                <div className="card text-bg-primary mb-3">
                    <div className="card-header">{ word.word }</div>
                    <div className="card-body">
                        <h5 className="card-title">{ word.definition }</h5>
                        <p className="card-text">{ word.example }</p>
                        <p className="card-text">
                            <small className="text-muted">{ word.translation }</small>
                        </p>
                    </div>
                </div>
            </div>
            /*
            <div key={ word.word }>
                <h3>{ word.word }</h3>
                <ul className="list-group words-list">
                    { word.definitions.map((definition: string) =>
                    <li className="list-group-item list-group-item-primary" key={ definition }>
                        { definition }
                    </li>
                    ) }
                </ul>
            </div>
            */
        );

		return (
            <div>
                <div>
                    Hello, {this.props.name}
                </div>
                <div>
                    Length: { this.state.words.length }
                    <div>
                        <DataTable columns={wordsTableColumns} data={this.state.words} selectableRows />
                    </div>
                </div>
            </div>
        );
	}
}