//import { useAuth } from '../auth/AuthContext'
import { useLocation, Navigate } from 'react-router-dom';
import { store } from '../app/Store';

export const AdminRoute = ({ children }) => {
    let authUser = JSON.parse(localStorage.getItem('authUser'));
    const location = useLocation();
    if (!authUser?.token && authUser?.userType != 1) {
        return <Navigate to="/" replace state={{ from: location }} />;
    }

    return children;
};