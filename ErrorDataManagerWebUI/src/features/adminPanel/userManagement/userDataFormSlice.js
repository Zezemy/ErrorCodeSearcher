import { CreateAppSlice } from '../../../app/CreateAppSlice';

const initialState = {
    userData: {
        "id": 0,
        "userName": "",
        "password": "",
        "userType": 0
    }
}

export const userDataSlice = CreateAppSlice({
    name: "userData",
    initialState,

    reducers: create => ({
        setId: (state, action) => {
            console.log(action.payload);
            state.userData.id = action.payload
        },
        setUserName: (state, action) => {
            state.userData.userName = action.payload
        },
        setPassword: (state, action) => {
            state.userData.password = action.payload
        },
        setUserType: (state, action) => {
            state.userData.userType = action.payload
        }
    }),

    selectors: {
        selectUserData: state => state.userData
    },
})
export const { setId, setUserName, setPassword, setUserType } = userDataSlice.actions;
export const { selectUserData } = userDataSlice.selectors;
