import * as React from 'react';
import * as ReactDOM from 'react-dom/client';
import { Provider } from 'react-redux';
import CssBaseline from '@mui/material/CssBaseline';
import { ThemeProvider } from '@mui/material/styles';
import App from './App';
import theme from './theme';
import { store } from './app/Store';
import {
    BrowserRouter,
    useNavigate,
    useLocation,
} from 'react-router-dom';
import { QueryParamProvider } from 'use-query-params';

const RouteAdapter = ({ children }) => {
    const navigate = useNavigate();
    const location = useLocation();

    const adaptedHistory = React.useMemo(
        () => ({
            replace(location) {
                navigate(location, { replace: true, state: location.state });
            },
            push(location) {
                navigate(location, { replace: false, state: location.state });
            },
        }),
        [navigate]
    );
    return children({ history: adaptedHistory, location });
};

const rootElement = document.getElementById('root');
const root = ReactDOM.createRoot(rootElement);

root.render(
    <BrowserRouter>
        <QueryParamProvider ReactRouterRoute={RouteAdapter}>
            <Provider store={store}>
                <ThemeProvider theme={theme}>
                    {/* CssBaseline kickstart an elegant, consistent, and simple baseline to build upon. */}
                    <CssBaseline />
                    <App />
                </ThemeProvider>
            </Provider>
        </QueryParamProvider>
    </BrowserRouter>
);
