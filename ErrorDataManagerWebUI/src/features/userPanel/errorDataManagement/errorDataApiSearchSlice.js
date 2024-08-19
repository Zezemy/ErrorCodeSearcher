import { CreateAppSlice } from '../../../app/CreateAppSlice';
import { SearchErrorDatas } from './errorDataApi';

const initialState = {
    value: [],
    status: "idle",
}

export const searchSlice = CreateAppSlice({
    name: "search",
    initialState,
    reducers: create => ({
        searchAsync: create.asyncThunk(
            async payload => {
                const response = await SearchErrorDatas(payload)
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
    },
})

export const { searchAsync } = searchSlice.actions;
export const { selectData, selectStatus } = searchSlice.selectors;