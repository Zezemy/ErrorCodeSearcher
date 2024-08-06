import React, { useEffect, useState } from "react";
import '../../App.css';
import DataTable from 'react-data-table-component';
import { SearchErrorDatas, AddErrorDatas, UpdateErrorDatas, DeleteErrorDatas } from './fetchErrorDatas';
import { useDispatch, useSelector } from 'react-redux';
import { searchAsync, getSelectedRows } from './errorDataApiSearchSlice';
import { setCategory, setDeviceClassName, setErrorCode, selectState } from './errorDataFormSlice';
import Table from "./Table";
import { store } from '../../app/Store';

function ErrorDataForm() {
    const dispatch = useDispatch();
    const selectedRows = useSelector(getSelectedRows);
    let errorDataFormState = useSelector(selectState);

    /*    const [category, setCategory] = useState("");*/
    /*   const [deviceClassName, setDeviceClassName] = useState("");*/
    //const [code, setCode] = useState("");
    const [description, setDescription] = useState("");
    const [tag, setTag] = useState("");

    const handleCategoryChange = (event) => {
        dispatch(setCategory(event.target.value));
        errorDataFormState = store.getState().errorData;
        console.log(errorDataFormState);

    }
    const handleDeviceClassNameChange = (event) => {
        dispatch(setDeviceClassName(event.target.value));
        errorDataFormState = store.getState().errorData;
        console.log(errorDataFormState);
    }

    const handleSubmit = (event) => {
        event.preventDefault();
        if (event.nativeEvent.submitter.name == "search") {
            let payload = {
                "errorDataList": [{
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": description,
                    "tag": tag
                }]
            };
            dispatch(searchAsync(payload));
        }

        else if (event.nativeEvent.submitter.name == "add") {
            AddErrorDatas({
                "errorDataArray": [{
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": description,
                    "tag": tag
                }]
            });
        }

        else if (event.nativeEvent.submitter.name == "update") {
            UpdateErrorDatas({
                "errorDataArray": [{
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": description,
                    "tag": tag
                }]
            });
        }

        else if (event.nativeEvent.submitter.name == "delete") {
            DeleteErrorDatas({
                "errorDataArray": [{
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": description,
                    "tag": tag
                }]
            });
        }
    }

    return (
        <div id="form" className="form">
            <h2>ATM Error Data Manager</h2>
            <header className="form-header">
            </header>
            <form onSubmit={handleSubmit}>
                <label>Category </label>
                <select id="category_options" value={errorDataFormState.category} onChange={handleCategoryChange}>
                    <option value=""></option>
                    <option value="XFS">XFS</option>
                    <option value="Simax">Simax</option>
                </select>
                <label>Tag
                    <input type="text"
                        value={tag}
                        onChange={(e) => setTag(e.target.value)} />
                </label>
                <br></br>
                <br></br>
                <label>Device Class </label>
                <select value={errorDataFormState.deviceClassName} onChange={handleDeviceClassNameChange}>
                    <option value=""></option>
                    <option value="XFSGeneral">XFSGeneral</option>
                    <option value="PTR">PTR</option>
                    <option value="IDC">IDC</option>
                    <option value="CDM">CDM</option>
                    <option value="PIN">PIN</option>
                    <option value="CHK">CHK</option>
                    <option value="DEP">DEP</option>
                    <option value="TTU">TTU</option>
                    <option value="SIU">SIU</option>
                    <option value="VDM">VDM</option>
                    <option value="CAM">CAM</option>
                    <option value="ALM">ALM</option>
                    <option value="CEU">CEU</option>
                    <option value="CIM">CIM</option>
                    <option value="CRD">CRD</option>
                    <option value="BCR">BCR</option>
                    <option value="IPM">IPM</option>
                    <option value="BIO">BIO</option>
                </select>
                <br /><br />
                <label>Error Code
                    <input type="text"
                        value={errorDataFormState.code}
                        onChange={(e) => dispatch(setErrorCode(e.target.value))} />
                </label>
                <br /><br />
                <label>Description </label>
                <textarea name="content" rows={5} cols={45} value={description} onChange={(e) => setDescription(e.target.value)} />
                <br /><br />
                <input type="submit" name="search" value="Search" />
                <input type="submit" name="add" value="Add" />
                <input type="submit" name="bulkInsert" value="Bulk Insert" />
                <input type="submit" name="update" value="Update" />
                <input type="submit" name="delete" value="Delete" />
                <input type="submit" name="reset" value="Reset" />
            </form>
            <br />
            <div class="container">
                <Table />
            </div>
        </div>
    );
}

export default ErrorDataForm;