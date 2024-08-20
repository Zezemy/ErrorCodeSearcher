import * as React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Box from '@mui/material/Box';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { selectData } from './userDataApiSearchSlice';
import { setId, setUserName, setPassword, setUserType } from './userDataFormSlice';
import { selectRowSelectionModel, setRowSelection } from '../../../app/AppStateSlice';
import { store } from '../../../app/Store';

const columns = [
    { field: 'id', headerName: 'Id', width: 90 },
    {
        field: 'userName',
        headerName: 'UserName',
        width: 150,
        editable: true,
    },
    {
        field: 'password',
        headerName: 'Password',
        width: 150,
        editable: true,
    },
    {
        field: 'userType',
        headerName: 'User Type',
        type: 'number',
        width: 90,
        editable: true,
    },
    {
        field: 'createdBy',
        headerName: 'Created By',
        type: 'number',
        width: 120,
        editable: true,
    },
    {
        field: 'createDate',
        headerName: 'Create Date',
        type: 'number',
        width: 110,
        editable: true,
    },
    {
        field: 'updatedBy',
        headerName: 'Updated By',
        type: 'number',
        width: 110,
        editable: true,
    },
    {
        field: 'updateDate',
        headerName: 'Update Date',
        type: 'number',
        width: 110,
        editable: true,
    },
];

export default function UserDataGrid() {
    const dispatch = useDispatch();
    const storedData = useSelector(selectData);
    var rowSelectionModel = useSelector(selectRowSelectionModel);

    const setRowSelectionModel = (selectedRowList) => {
        console.log("setRowSelectionModel selectedRowList");
        console.log(selectedRowList);
        if (selectedRowList.length == 1) {
            let selectedRows = storedData.filter(x => x.id == selectedRowList[0]);
            let selected = selectedRows[0];
            console.log("selected");
            console.log(selected);
            dispatch(setId(selected.id));
            dispatch(setUserName(selected.userName));
            dispatch(setPassword(selected.password));
            dispatch(setUserType(selected.userType));
            rowSelectionModel = store.getState().appState.rowSelectionModel;
            console.log("rowSelectionModel");
            console.log(rowSelectionModel);
        }
        else {
            ResetState();
        }
    };

    function ResetState() {
        dispatch(setId(0));
        dispatch(setUserName(''));
        dispatch(setPassword(''));
        dispatch(setUserType(0));
        dispatch(setRowSelection([]));
    }

    return (
        <Box sx={{ height: 400, width: '100%' }}>
            <DataGrid
                rows={storedData}
                columns={columns}
                initialState={{
                    pagination: {
                        paginationModel: {
                            pageSize: 10,
                        },
                    },
                }}
                pageSizeOptions={[10, 25, 50, 100]}
                checkboxSelection
                disableRowSelectionOnClick
                disableMultipleRowSelection
                onRowSelectionModelChange={(selectedRow) => {
                    console.log("selectedRowId");
                    console.log(selectedRow);
                    setRowSelectionModel(selectedRow);
                    dispatch(setRowSelection(selectedRow));
                }}
                rowSelectionModel={rowSelectionModel}
                rowHeight={35} {...storedData}
            />
        </Box>
    );
}
