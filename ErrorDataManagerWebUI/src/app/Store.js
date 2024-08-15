import { combineSlices, configureStore } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import { searchSlice } from '../features/userPanel/errorDataManagement/errorDataApiSearchSlice';
import { errorDataSlice } from '../features/userPanel/errorDataManagement/errorDataFormSlice';
import { userDataSlice } from '../features/adminPanel/userManagement/userDataFormSlice';
import { AppStateSlice } from './AppStateSlice';

const rootReducer = combineSlices(searchSlice, errorDataSlice, userDataSlice, AppStateSlice); //creating global state

export const makeStore = preloadedState => {
    const store = configureStore({
        reducer: rootReducer,
        middleware: getDefaultMiddleware => {
            return getDefaultMiddleware({
                serializableCheck: false,
            })
        },
        preloadedState,
    })
    setupListeners(store.dispatch);
    return store;
}
export const store = makeStore();