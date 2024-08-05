import React, { useEffect, useState } from "react";
import '../App.css';
/*import Table from "../components/Table";*/
import DataTable from 'react-data-table-component';
import { SearchErrorDatas, AddErrorDatas, UpdateErrorDatas, DeleteErrorDatas } from '../components/FetchErrorDatas';
import { createRoot } from 'react-dom/client';

function ErrorDataForm() {
    const [category, setCategory] = useState("");
    const [deviceClassName, setDeviceClassName] = useState("");
    const [code, setCode] = useState("");
    const [description, setDescription] = useState("");
    const [tag, setTag] = useState("");
    const [data, setData] = useState([]);

    const handleCategoryChange = (event) => {
        setCategory(event.target.value)
    }
    const handleDeviceClassNameChange = (event) => {
        setDeviceClassName(event.target.value)
    }
    const columns = [
        {
            colName: 'id',
            selector: row => row.id,
        },
        {
            colName: 'code',
            selector: row => row.code,
        },
        {
            colName: 'description',
            selector: row => row.description,
        },
        {
            colName: 'category',
            selector: row => row.category,
        },
        {
            colName: 'deviceClass',
            selector: row => row.deviceClass,
        },
        {
            colName: 'tag',
            selector: row => row.tag,
        },
        {
            colName: 'createdBy',
            selector: row => row.createdBy,
        },
        {
            colName: 'createDate',
            selector: row => row.createDate,
        },
        {
            colName: 'updatedBy',
            selector: row => row.updatedBy,
        },
        {
            colName: 'updateDate',
            selector: row => row.updateDate,
        },
        {
            colName: '',
            selector: row => row.delete,
        },
    ];

    const handleSubmit = (event) => {
        event.preventDefault();
        if (event.nativeEvent.submitter.name == "search") {
            var response = SearchErrorDatas({
                "errorDataList": [{
                    "category": category,
                    "deviceClassName": deviceClassName,
                    "code": code,
                    "description": description,
                    "tag": tag
                }]
            });
            console.log("response value");
            console.log(response);
            setData(response);
            console.log("data value");
            console.log(data);
            const commentDomNode = document.getElementById('form');
            const commentRoot = createRoot(commentDomNode);

            commentRoot.render(<MyComponent />);

        }
        else if (event.nativeEvent.submitter.name == "add") {
            AddErrorDatas({
                "errorDataArray": [{
                    "category": category,
                    "deviceClassName": deviceClassName,
                    "code": code,
                    "description": description,
                    "tag": tag
                }]
            });
        }
        else if (event.nativeEvent.submitter.name == "update") {
            UpdateErrorDatas({
                "errorDataArray": [{
                    "category": category,
                    "deviceClassName": deviceClassName,
                    "code": code,
                    "description": description,
                    "tag": tag
                }]
            });
        }
        else if (event.nativeEvent.submitter.name == "delete") {
            DeleteErrorDatas({
                "errorDataArray": [{
                    "category": category,
                    "deviceClassName": deviceClassName,
                    "code": code,
                    "description": description,
                    "tag": tag
                }]
            });
        }
    }

    function MyComponent() {
        return (
            <DataTable
                columns={columns}
                data={data}
            />
        );
    };

    return (
        <div id = "form" className="form">
            <h2>ATM Error Data Manager</h2>
            <header className="form-header">
            </header>
            <form onSubmit={handleSubmit}>
                <label>Category </label>
                <select value={category} onChange={handleCategoryChange}>
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
                <select value={deviceClassName} onChange={handleDeviceClassNameChange}>
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
                        value={code}
                        onChange={(e) => setCode(e.target.value)} />
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
                <MyComponent />
            </div>
        </div>
    );
}

export default ErrorDataForm;