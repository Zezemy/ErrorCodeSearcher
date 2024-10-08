import * as React from 'react';
import PropTypes from 'prop-types';
import Stack from '@mui/material/Stack';
import NavbarBreadcrumbs from './NavbarBreadcrumbs';
import ToggleColorMode from './ToggleColorMode';
import Logout from '../../../components/Logout';

function Header({ mode, toggleColorMode }) {
    return (
        <Stack
            direction="row"
            sx={{
                display: { xs: 'none', md: 'flex' },
                width: '100%',
                alignItems: { xs: 'flex-start', md: 'center' },
                justifyContent: 'space-between',
                maxWidth: { sm: '100%', md: '1700px' },
            }}
            spacing={2}
        >
            <NavbarBreadcrumbs />
            <Stack direction="row" sx={{ gap: 1 }}>
                <ToggleColorMode
                    mode={mode}
                    toggleColorMode={toggleColorMode}
                    data-screenshot="toggle-mode"
                />
                <Logout />
            </Stack>
        </Stack>
    );
}

Header.propTypes = {
    mode: PropTypes.oneOf(['dark', 'light']).isRequired,
    toggleColorMode: PropTypes.func.isRequired,
};

export default Header;
