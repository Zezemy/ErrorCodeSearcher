import { useState, useEffect } from 'react';

export function SearchErrorDatas(requestBody) {
    //console.log("requestBody");
    //console.log(requestBody);
    return fetch('https://localhost:7139/api/Error/Search', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            return res.json();
        })
        .catch((error) => {
            console.log(error)
            console.log("err:", error, error.name, error.message)
            return { responseCode: "-1", responseDescription: "system error" };
        });
}

export function AddErrorDatas(requestBody) {
    fetch('https://localhost:7139/api/Error/Add', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            return res.json();
        })
        .then((data) => {
            console.log(data);
            alert(data.responseDescription);
        })
        .catch((error) => {
            console.log(error)
            console.log("err:", error, error.name, error.message)
            return { responseCode: "-1", responseDescription: "system error" };
        });
}

export function UpdateErrorDatas(requestBody) {
    fetch('https://localhost:7139/api/Error/Update', {
        method: 'put',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            return res.json();
        })
        .then((data) => {
            console.log(data);
            alert(data.responseDescription);
        })
        .catch((error) => {
            console.log(error)
            console.log("err:", error, error.name, error.message)
            return { responseCode: "-1", responseDescription: "system error" };
        });
}

export function DeleteErrorDatas(requestBody) {
    console.log(requestBody);
    fetch(`https://localhost:7139/api/Error/Delete?id=${requestBody.id}`, {
        method: 'delete',
        headers: { 'Content-Type': 'application/json' }
    })
        .then((res) => {
            return res.json();
        })
        .then((data) => {
            console.log(data);
            alert(data.responseDescription);
        })
        .catch((error) => {
            console.log(error)
            console.log("err:", error, error.name, error.message)
            return { responseCode: "-1", responseDescription: "system error" };
        });
}


