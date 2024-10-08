//import { useAuth } from '../auth/AuthContext'
import { useLocation, Navigate } from 'react-router-dom';
import { store } from '../app/Store';

export const ProtectedRoute = ({ children }) => {
    const location = useLocation();
    let authUser = JSON.parse(localStorage.getItem('authUser'));

    if (!authUser?.token) {
        return <Navigate to="/" replace state={{ from: location }} />;
    }

    return children;
};