import { createAppSlice } from '../../app/createAppSlice';

const initialState = {
    errorData: {
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
}

export const errorDataSlice = createAppSlice({
    name: "errorData",
    initialState,

    reducers: create => ({
        setId: (state, action) => {
            console.log(action.payload);
            state.errorData.id = action.payload
        },
        setCategory: (state, action) => {
            state.errorData.category = action.payload
        },
        setDeviceClassName: (state, action) => {
            state.errorData.deviceClassName = action.payload
        },
        setErrorCode: (state, action) => {
            state.errorData.code = action.payload
        },
        setDescription: (state, action) => {
            state.errorData.description = action.payload
        },
        setTag: (state, action) => {
            state.errorData.tag = action.payload
        }
    }),

    selectors: {
        selectState: state => state.errorData
    },
})
export const { setId, setCategory, setDeviceClassName, setErrorCode, setDescription, setTag } = errorDataSlice.actions;
export const { selectState } = errorDataSlice.selectors;
