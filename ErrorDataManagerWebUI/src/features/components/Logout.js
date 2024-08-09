import React from 'react';
import { useAuth } from '../../auth/AuthContext';
import Button from '@mui/material/Button';
import IconButton from '@mui/material/IconButton';
import LogoutIcon from '@mui/icons-material/Logout';

const Logout = () => {
   const { onLogout } = useAuth();

    return (
        <div>
            <IconButton
                aria-label="logout"
                onClick={onLogout}
                color="inherit"
            >
                <LogoutIcon />
            </IconButton>
        </div>
    );
};

export default Logout;