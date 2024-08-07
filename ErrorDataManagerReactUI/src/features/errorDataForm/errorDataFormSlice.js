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
        setId: (state, action) => {
            state.id = action.payload
        },
        setCategory: (state, action) => {
            state.category = action.payload
        },
        setDeviceClassName: (state, action) => {
            state.deviceClassName = action.payload
        },
        setErrorCode: (state, action) => {
            state.code = action.payload
        },
        setDescription: (state, action) => {
            state.description = action.payload
        },
        setTag: (state, action) => {
            state.tag = action.payload
        }
    }),

    selectors: {
        selectState: state => state
    },
})
export const { setId, setCategory, setDeviceClassName, setErrorCode, setDescription, setTag } = errorDataSlice.actions;
export const { selectState } = errorDataSlice.selectors;
