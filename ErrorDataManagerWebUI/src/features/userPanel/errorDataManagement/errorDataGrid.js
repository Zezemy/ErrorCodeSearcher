import * as React from 'react';
import { useDispatch, useSelector } from 'react-redux';
import Box from '@mui/material/Box';
import { DataGrid, GridColDef } from '@mui/x-data-grid';
import { selectData, selectStatus } from './errorDataApiSearchSlice';
import { setId, setCategory, setDeviceClassName, setErrorCode, setDescription, setTag, selectState } from './errorDataFormSlice';
import { selectRowSelectionModel, setRowSelection } from '../../../app/AppStateSlice';
import { store } from '../../../app/Store';

const columns = [
    { field: 'id', headerName: 'Id', width: 90 },
    {
        field: 'code',
        headerName: 'Code',
        width: 120,
        editable: true,
    },
    {
        field: 'description',
        headerName: 'Description',
        width: 150,
        editable: true,
    },
    {
        field: 'category',
        headerName: 'Category',
        type: 'number',
        width: 90,
        editable: true,
    },
    {
        field: 'deviceClassName',
        headerName: 'Device Class Name',
        type: 'number',
        width: 110,
        editable: true,
    },
    {
        field: 'tag',
        headerName: 'Tag',
        type: 'number',
        width: 90,
        editable: true,
    },
    {
        field: 'createdBy',
        headerName: 'Created By',
        type: 'number',
        width: 110,
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

export default function ErrorDataGrid() {
    const dispatch = useDispatch();
    const storedData = useSelector(selectData);
    const loadStatus = useSelector(selectStatus);
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
            dispatch(setCategory(selected.category));
            dispatch(setDeviceClassName(selected.deviceClassName));
            dispatch(setErrorCode(selected.code));
            dispatch(setDescription(selected.description));
            dispatch(setTag(selected.tag));
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
        dispatch(setCategory(''));
        dispatch(setDeviceClassName(''));
        dispatch(setErrorCode(''));
        dispatch(setDescription(''));
        dispatch(setTag(''));
        dispatch(setRowSelection([]));
    }

    if (loadStatus == "loading") {
        return (<>
            <div>Loading</div>
        </>
        );
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
