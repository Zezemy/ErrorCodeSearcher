import { CreateAppSlice } from '../../../app/CreateAppSlice';
import { SearchUserDatas } from './userDataApi';

const initialState = {
    value: [],
    status: "idle",
    columns: [
        {
            name: 'Id',
            selector: row => row.id,
        },
        {
            name: 'UserName',
            selector: row => row.code,
        },
        {
            name: 'Password',
            selector: row => row.description,
        },
        {
            name: 'UserType',
            selector: row => row.category,
        },
        {
            name: 'CreatedBy',
            selector: row => row.createdBy,
        },
        {
            name: 'CreateDate',
            selector: row => row.createDate,
        },
        {
            name: 'UpdatedBy',
            selector: row => row.updatedBy,
        },
        {
            name: 'UpdateDate',
            selector: row => row.updateDate,
        }
    ],
    selectedRows: [],
}

export const searchSlice = CreateAppSlice({
    name: "search",
    initialState,
    reducers: create => ({
        setSelectedRows: (state, action) => {
            state.selectedRows = action.payload
        },

        searchAsync: create.asyncThunk(
            async payload => {
                const response = await SearchUserDatas(payload)
                console.log("response:")
                console.log(response)
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

    selectors: {
        selectData: state => state.value,
        selectStatus: state => state.status,
        selectColumns: state => state.columns,
        getSelectedRows: state => state.selectedRows,
    },
})

export const { searchAsync, setSelectedRows } = searchSlice.actions;
export const { selectData, selectStatus, selectColumns, getSelectedRows } = searchSlice.selectors;