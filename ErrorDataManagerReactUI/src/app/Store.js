import { combineSlices, configureStore } from '@reduxjs/toolkit';
import { setupListeners } from '@reduxjs/toolkit/query';
import { searchSlice } from '../features/errorDataForm/errorDataApiSearchSlice';
import { errorDataSlice } from '../features/errorDataForm/errorDataFormSlice';

//'combineSlices' automatically combines the reducers using
//their 'reducerPath's, therefore we no longer need to call 'combineReducers'.
const rootReducer = combineSlices(searchSlice, errorDataSlice);

//the store setup is wrapped in 'makeStore' to allow reuse when setting up tests that need the same store config
export const makeStore = preloadedState => {
    const store = configureStore({
        reducer: rootReducer,
        //adding the api middleware enables caching, invalidation, polling, and other useful features of 'rtk-query'.
        middleware: getDefaultMiddleware => {
            return getDefaultMiddleware({
                serializableCheck: false,
            })
        },
        preloadedState,
    })
    //configure listeners using the provided defaults
    setupListeners(store.dispatch)
    return store
}
export const store = makeStore();