import React, { createContext, useContext, useState } from 'react';
import { useNavigate, useLocation } from 'react-router-dom';
import { store } from '../app/Store';

const fakeAuth = (isAdmin) =>
    new Promise((resolve) => {
        setTimeout(() => resolve({
            authUser: {
                userName: "zeynep",
                userType: isAdmin ? 1 : 2,
                token: "123123"
            }
        }), 250);
    });

const AuthContext = createContext();

export const AuthProvider = ({ children }) => {
    const navigate = useNavigate();
    const location = useLocation();


    const handleLogin = async () => {
        let user = store.getState().appState.user;
        const response = await fakeAuth(user.userName==='admin');

        localStorage.setItem('authUser', JSON.stringify(response.authUser));
        console.log("navigate on login");
        if (response.authUser.userType === 1) {
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