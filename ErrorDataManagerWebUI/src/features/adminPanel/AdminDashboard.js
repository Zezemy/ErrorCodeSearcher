import * as React from 'react';
import { createTheme, ThemeProvider, alpha } from '@mui/material/styles';
import Box from '@mui/material/Box';
import Stack from '@mui/material/Stack';
import Navbar from './components/Navbar';
import Header from './components/Header';
import SideMenu from './components/SideMenu';
import { Outlet } from 'react-router-dom';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import MenuIcon from '@mui/icons-material/Menu';
import Logout from '../../components/Logout';
function AdminDashboard() {
    const [mode, setMode] = React.useState('light');
    const defaultTheme = createTheme({ palette: { mode } });

    const toggleColorMode = () => {
        setMode((prev) => (prev === 'dark' ? 'light' : 'dark'));
    };

    return (
        <div >
            <ThemeProvider theme={defaultTheme}>
                {/*    <CssBaseline />*/}
                <Box sx={{ display: 'flex' }}>
                    <SideMenu />
                    <Navbar mode={mode} toggleColorMode={toggleColorMode} />

                    <Box
                        component="main"
                        sx={(theme) => ({
                            position: { sm: 'relative', md: '' },
                            top: { sm: '48px', md: '0' },
                            height: { sm: 'calc(100vh - 48px)', md: '100vh' },
                            flexGrow: 1,
                            pt: 2,
                            backgroundColor: alpha(theme.palette.background.default, 1),
                            overflow: 'auto',
                        })}
                    >
                        <Stack
                            spacing={2}
                            sx={{
                                alignItems: 'center',
                                mx: 3,
                                pb: 2,
                            }}
                        >
                            <Header mode={mode} toggleColorMode={toggleColorMode} />

                        </Stack>
                        <Outlet />
                    </Box>

                </Box>
            </ThemeProvider>
        </div >
    );
}

export default AdminDashboard;