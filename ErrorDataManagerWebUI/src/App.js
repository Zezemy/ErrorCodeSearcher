import * as React from 'react';
import { Routes, Route } from 'react-router-dom';
import ErrorDataManager from './app/ErrorDataManager';
import NoPage from './pages/NoPage';
import AdminDashboard from './pages/AdminDashboard';
import { useDispatch } from 'react-redux';
import { setUsername, setPassword } from './features/errorDataForm/appStateSlice';
import { AuthProvider, useAuth } from './auth/AuthContext';
import { ProtectedRoute } from './routing/ProtectedRoute';
import Container from '@mui/material/Container';
import Login from './pages/Login';

const App = () => {
    return (
        <Container maxWidth={false}>

            <AuthProvider>

                {/*            <Navigation />*/}

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
                            <ProtectedRoute>
                                <AdminDashboard />
                            </ProtectedRoute>
                        }
                    />

                    <Route path="*" element={<NoPage />} />
                </Routes>
            </AuthProvider>
        </Container>
    );
};

//const Navigation = () => {
//    const { token, onLogout } = useAuth();

//    return (
//        <nav>

//            {token && (
//                <button type="button" onClick={onLogout}>
//                    Sign Out
//                </button>
//            )}
//        </nav>
//    );
//};

//const Home = () => {
//    const { onLogin } = useAuth();
//    const dispatch = useDispatch();

//    return (
//        <>

//            <div>
//                <input type="text" onChange={(e) => dispatch(setUsername(e.target.value))} />
//                <input type="password" onChange={(e) => dispatch(setPassword(e.target.value))} />
//                <button onClick={onLogin}>Login</button>
//            </div>

//        </>
//    );
//};

//const Dashboard = () => {
//    const { token } = useAuth();

//    return (
//        <>
//            <h2>Dashboard (Protected)</h2>

//            <div>Authenticated as {token}</div>
//        </>
//    );
//};

export default App;
