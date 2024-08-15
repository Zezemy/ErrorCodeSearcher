import React, { createContext, useContext, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { store } from '../app/Store';

function LoginUser(requestBody) {
    return fetch('https://localhost:7139/api/Authentication/LoginUser', {
        method: 'post',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify(requestBody)
    })
        .then((res) => {
            return res.json();
        })
        .catch((error) => {
            console.log(error)
            console.log("ERR:", error, error.name, error.message)
            return { responseCode: "-1", responseDescription: "System Error" };
        });
}

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const navigate = useNavigate();
    const location = useLocation();


    const handleLogin = async () => {
        let user = store.getState().appState.user;
        let request = {
            "userName": user.userName,
            "password": user.password
        }
        const response = await LoginUser(request);
        console.log('response');
        console.log(response);
        localStorage.setItem('authUser', JSON.stringify(response.user));
        if (response.responseCode != "0") {
            alert(response.responseDescription);
            return;
        }
        console.log("navigate on login");
        if (response.user.userType === 1) {
            console.log("/admin");
            navigate('/admin');
        }
        else {
            console.log("/dashboard");
            navigate('/dashboard');
        }
    };

    const handleLogout = () => {
        console.log("handlelogout");
        localStorage.setItem('authUser', JSON.stringify({}));
        navigate('/');
    };

    const value = {
        onLogin: handleLogin,
        onLogout: handleLogout,
    };

    return (
        <AuthContext.Provider value={value}>
            {children}
        </AuthContext.Provider>
    );
};

export const useAuth = () => useContext(AuthContext);