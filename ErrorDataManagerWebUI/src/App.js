import * as React from 'react';
import { Routes, Route } from 'react-router-dom';
import ErrorDataManager from './features/userPanel/errorDataManagement/ErrorDataManager';
import NotFound from '../src/features/NotFound';
import AdminDashboard from '../src/features/adminPanel/AdminDashboard';
import { ProtectedRoute } from './routing/ProtectedRoute';
import { AdminRoute } from './routing/AdminRoute';
import Container from '@mui/material/Container';
import Login from '../src/features/login/Login';
import UserDataManager from './features/adminPanel/userManagement/UserDataManager';
import UserDashboard from './features/userPanel/errorDataManagement/UserDashboard';
import LandingPage from './features/LandingPage';

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
                            <UserDashboard />
                        </ProtectedRoute>
                    }
                >
                    <Route index element={<LandingPage />} />
                    <Route
                        path="errorDataManager"
                        element={
                            <ErrorDataManager />
                        }
                    />
                </Route>
                <Route
                    path="admin"
                    element={
                        <AdminRoute>
                            <AdminDashboard />
                        </AdminRoute>
                    }
                >
                    <Route index element={<LandingPage />} />
                    <Route
                        path="userDataManager"
                        element={
                            <UserDataManager />
                        }
                    />
                    <Route
                        path="errorDataManager"
                        element={
                            <ErrorDataManager />
                        }
                    />
                </Route>

                <Route path="*" element={<NotFound />} />
            </Routes>
        </Container>
    );
};

export default App;
