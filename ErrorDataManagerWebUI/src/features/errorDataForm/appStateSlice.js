import { CreateAppSlice } from '../../app/CreateAppSlice';

const initialState = {
    rowSelectionModel: [],
    user: {
        userName: '',
        password:''
    }
}

export const appStateSlice = CreateAppSlice({
    name: "appState",
    initialState,

    reducers: create => ({
        setRowSelection: (state, action) => {
            console.log("setRowSelection called action payload");
            console.log(action.payload);
            state.rowSelectionModel = action.payload
        },
        setUsername: (state, action) => {
            state.user.userName = action.payload
        },
        setPassword: (state, action) => {
            state.user.password = action.payload
        },
    }),

    selectors: {
        selectRowSelectionModel: state => state.rowSelectionModel,
        selectUser: state => state.user
    },
})
export const { setRowSelection, setUsername, setPassword } = appStateSlice.actions;
export const { selectRowSelectionModel, selectUser } = appStateSlice.selectors;
