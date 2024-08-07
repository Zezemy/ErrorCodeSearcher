import { combineSlices, configureStore } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import { searchSlice } from '../features/errorDataForm/errorDataApiSearchSlice';
import { errorDataSlice } from '../features/errorDataForm/errorDataFormSlice';

const rootReducer = combineSlices(searchSlice, errorDataSlice);

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