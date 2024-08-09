import React, { createContext, useContext, useState } from 'react';
import {
    useNavigate,
    useLocation,
} from 'react-router-dom';
import { store } from '../app/Store';

const fakeAuth = () =>
    new Promise((resolve) => {
        setTimeout(() => resolve('123123'), 250);
    });

const AuthContext = createContext(null);

export const AuthProvider = ({ children }) => {
    const navigate = useNavigate();
    const location = useLocation();

    const [token, setToken] = useState(null);

    const handleLogin = async () => {
        const token = await fakeAuth();
        let user = store.getState().appState.user;

        setToken(token);
        console.log(token);

        let origin = location.state?.from?.pathname || '/dashboard';

        console.log(user);
        if (user.userName === 'admin') {
            console.log("userName is admin");
            origin = '/admin';
        }
        else {
            origin = '/dashboard';
        }
        navigate(origin);
    };

    const handleLogout = () => {
        console.log("handlelogout");
        setToken(null);
    };

    const value = {
        token,
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