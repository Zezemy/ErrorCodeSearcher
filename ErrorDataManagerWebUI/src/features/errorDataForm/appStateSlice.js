import { createAppSlice } from '../../app/createAppSlice';

const initialState = {
    rowSelectionModel: []
}

export const appStateSlice = createAppSlice({
    name: "appState",
    initialState,

    reducers: create => ({
        setRowSelection: (state, action) => {
            console.log("setRowSelection called action payload");
            console.log(action.payload);
            state.rowSelectionModel = action.payload
        },
    }),

    selectors: {
        selectRowSelectionModel: state => state.rowSelectionModel
    },
})
export const { setRowSelection } = appStateSlice.actions;
export const { selectRowSelectionModel } = appStateSlice.selectors;
