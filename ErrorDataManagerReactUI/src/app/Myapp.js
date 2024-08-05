import React from 'react';
import ErrorDataForm from '../components/ErrorDataForm';
import Fetch from '../components/FetchErrorDatas';
import Table from "../components/Table";
import '../App.css';

function Myapp() {
    return (
        <div className="App">
            <header className="App-header">
                <p>
                    <ErrorDataForm />
                </p>
            </header>
        </div>
    );
}

export default Myapp;
