import { useState, useEffect } from 'react';

export function SearchErrorDatas(requestBody) {
    fetch('https://localhost:7139/api/Error/Search', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            var ret = res.json();
            console.log(ret);
            return ret;
        })
        .then((data) => {
            console.log(data);
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
        });
}

export function DeleteErrorDatas(requestBody) {
    fetch('https://localhost:7139/api/Error/Delete', {
        method: 'delete',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            return res.json();
        })
        .then((data) => {
            console.log(data);
        });
}

//fetch('https://localhost:7139/api/Error/Search', {
//    method: 'post',
//    headers: { 'Content-Type': 'application/json' },
//    body: JSON.stringify({
//        "errorDataArray": [{
//            "category": category,
//            "deviceClassName": deviceClassName,
//            "code": code,
//            "description": description,
//            "tag": tag
//        }]
//    })
//});

//fetch('https://localhost:7139/api/Error/Update', {
//    method: 'put',
//    headers: { 'Content-Type': 'application/json' },
//    body: JSON.stringify({
//        "errorDataArray": [{
//            "category": category,
//            "deviceClassName": deviceClassName,
//            "code": code,
//            "description": description,
//            "tag": tag
//        }]
//    })
//});

//fetch('https://localhost:7139/api/Error/Delete', {
//    method: 'delete',
//    headers: { 'Content-Type': 'application/json' },
//    body: JSON.stringify({
//        "errorDataArray": [{
//            "category": category,
//            "deviceClassName": deviceClassName,
//            "code": code,
//            "description": description,
//            "tag": tag
//        }]
//    })
//});


