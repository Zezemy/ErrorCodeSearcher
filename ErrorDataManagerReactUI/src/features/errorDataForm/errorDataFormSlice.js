import { createAppSlice } from '../../app/createAppSlice';

const initialState = {
    "createdBy": "",
    "createDate": "",
    "updatedBy": "",
    "updateDate": "",
    "id": 0,
    "code": "",
    "description": "",
    "category": "",
    "deviceClassName": "",
    "tag": ""
}

export const errorDataSlice = createAppSlice({
    name: "errorData",
    initialState,

    reducers: create => ({
        setCategory: (state, action) => {
            state.category = action.payload
        },
        setDeviceClassName: (state, action) => {
            state.deviceClassName = action.payload
        },
        setErrorCode: (state, action) => {
            state.code = action.payload
        }
    }),

    selectors: {
        selectState: state => state
    },
})

export const { setCategory, setDeviceClassName, setErrorCode } = errorDataSlice.actions;
export const { selectState } = errorDataSlice.selectors;
