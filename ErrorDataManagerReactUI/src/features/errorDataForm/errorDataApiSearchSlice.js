import { createAppSlice } from '../../app/createAppSlice';
import { SearchErrorDatas } from './fetchErrorDatas';

const initialState = {
    value: [],
    status: "idle",
    columns: [
        {
            name: 'id',
            selector: row => row.id,
        },
        {
            name: 'code',
            selector: row => row.code,
        },
        {
            name: 'description',
            selector: row => row.description,
        },
        {
            name: 'category',
            selector: row => row.category,
        },
        {
            name: 'deviceClassName',
            selector: row => row.deviceClassName,
        },
        {
            name: 'tag',
            selector: row => row.tag,
        },
        {
            name: 'createdBy',
            selector: row => row.createdBy,
        },
        {
            name: 'createDate',
            selector: row => row.createDate,
        },
        {
            name: 'updatedBy',
            selector: row => row.updatedBy,
        },
        {
            name: 'updateDate',
            selector: row => row.updateDate,
        }
    ],
    selectedRows: []
}

//if you are not using async thunks you can use the standalone 'createSlice'.
export const searchSlice = createAppSlice({
    name: "search",
    //'createSlice' will infer the state type from the 'initialState' argument
    initialState,
    //the 'reducers' field let us define reducers and generate associated actions
    reducers: create => ({
        setSelectedRows: (state, action) => {
            state.selectedRows = action.payload
        },
        // Use the PayloadAction type to declare the contents of `action.payload`
        searchAsync: create.asyncThunk(
            async payload => {
                const response = await SearchErrorDatas(payload)
                console.log("response:")
                console.log(response)
                //the value we return becomes the 'fulfilled' action payload
                return response.data
            },
            {
                pending: state => {
                    state.status = "loading"
                },
                fulfilled: (state, action) => {
                    state.status = "idle"
                    console.log("response:")
                    console.log(action.payload)
                    state.value = action.payload
                },
            },
        ),
    }),

    //the selectors receive the slice state as their first argument.
    selectors: {
        selectData: state => state.value,
        selectStatus: state => state.status,
        selectColumns: state => state.columns,
        getSelectedRows: state => state.selectedRows
    },
})

//action creators are generated for each case reducer function.
export const { searchAsync, setSelectedRows } = searchSlice.actions;
//selectors returned by 'slice.selectors' take the root state as their first argument.
export const { selectData, selectStatus, selectColumns, getSelectedRows } = searchSlice.selectors;