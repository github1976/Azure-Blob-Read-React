import React from 'react';
import './App.css';
import BlobData from './BlobData';

function App() {
    return (
        <div className="App">
            <header className="App-header">
                <h1>Blob Storage Data Viewer</h1>
            </header>
            <main>
                <BlobData />
            </main>
        </div>
    );
}

export default App;
