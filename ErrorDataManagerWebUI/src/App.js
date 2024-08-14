import * as React from 'react';
import { Routes, Route } from 'react-router-dom';
import ErrorDataManager from './features/errorDataForm/ErrorDataManager';
import NotFound from '../src/features/NotFound';
import AdminDashboard from '../src/features/adminPanel/AdminDashboard';
import { ProtectedRoute } from './routing/ProtectedRoute';
import { AdminRoute } from './routing/AdminRoute';
import Container from '@mui/material/Container';
import Login from '../src/features/login/Login';
import UserDataManager from './features/adminPanel/userManagement/UserDataManager';

const App = () => {
    return (
        <Container maxWidth={false}>

            <Routes>
                <Route index element={<Login />} />
                <Route path="home" element={<Login />} />
                <Route
                    path="dashboard"
                    element={
                        <ProtectedRoute>
                            <ErrorDataManager />
                        </ProtectedRoute>
                    }
                />
                <Route
                    path="admin"
                    element={
                        <AdminRoute>
                            <AdminDashboard />
                        </AdminRoute>
                    }
                >
                    <Route
                        path="userDataManager"
                        element={
                            <UserDataManager />
                        }
                    />
                </Route>

                <Route path="*" element={<NotFound />} />
            </Routes>
        </Container>
    );
};

export default App;
