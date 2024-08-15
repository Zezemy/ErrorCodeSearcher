export function SearchUserDatas(requestBody) {
    return fetch('https://localhost:7139/api/User/SearchUser', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            return res.json();
        })
}

export function AddUserDatas(requestBody) {
    fetch('https://localhost:7139/api/User/AddUser', {
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
        });
}

export function UpdateUserDatas(requestBody) {
    fetch('https://localhost:7139/api/User/UpdateUser', {
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
        });
}

export function DeleteUserDatas(requestBody) {
    console.log(requestBody);
    fetch(`https://localhost:7139/api/User/DeleteUser?id=${requestBody.id}`, {
        method: 'delete',
        headers: { 'Content-Type': 'application/json' }
    })
        .then((res) => {
            return res.json();
        })
        .then((data) => {
            console.log(data);
            alert(data.responseDescription);
        });
}