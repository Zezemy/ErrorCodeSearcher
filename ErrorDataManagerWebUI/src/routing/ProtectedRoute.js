import { useAuth } from '../auth/AuthContext';
import { useLocation, Navigate } from 'react-router-dom';

export const ProtectedRoute = ({ children }) => {
    const { token } = useAuth();
    const location = useLocation();

    if (!token) {
        return <Navigate to="/home" replace state={{ from: location }} />;
    }

    return children;
};