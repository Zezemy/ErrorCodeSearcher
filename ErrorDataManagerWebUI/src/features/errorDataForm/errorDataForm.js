import React, { useEffect, useState } from "react";
import '../../App.css';
import { SearchErrorDatas, AddErrorDatas, UpdateErrorDatas, DeleteErrorDatas } from './fetchErrorDatas';
import { useDispatch, useSelector } from 'react-redux';
import { searchAsync, getSelectedRows } from './errorDataApiSearchSlice';
import { setId, setCategory, setDeviceClassName, setErrorCode, setDescription, setTag, selectState } from './errorDataFormSlice';
import { setRowSelection } from './appStateSlice';
import { store } from '../../app/Store';
import ErrorDataGrid from './errorDataGrid';
import AppBar from '@mui/material/AppBar';
import Toolbar from '@mui/material/Toolbar';
import Typography from '@mui/material/Typography';
import IconButton from '@mui/material/IconButton';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import MenuIcon from '@mui/icons-material/Menu';
import Button from '@mui/material/Button';
import SearchIcon from '@mui/icons-material/Search';
import AddIcon from '@mui/icons-material/Add';
import MoveUpIcon from '@mui/icons-material/MoveUp';
import FileUploadIcon from '@mui/icons-material/FileUpload';
import DeleteIcon from '@mui/icons-material/Delete';
import CleaningServicesIcon from '@mui/icons-material/CleaningServices';
import Logout from '../components/Logout'


function ErrorDataForm() {
    const dispatch = useDispatch();
    const selectedRows = useSelector(getSelectedRows);
    let errorDataFormState = useSelector(selectState);

    const [categoryErrorState, setCategoryErrorState] = useState(false);
    const [deviceClassNameErrorState, setDeviceClassNameErrorState] = useState(false);
    const [codeErrorState, setCodeErrorState] = useState(false);
    const [descriptionErrorState, setDescriptionErrorState] = useState(false);

    /*    const [category, setCategory] = useState("");*/
    /*   const [deviceClassName, setDeviceClassName] = useState("");*/
    /*    const [code, setCode] = useState("");*/
    /*    const [description, setDescription] = useState("");*/
    /*    const [tag, setTag] = useState("");*/

    const handleCategoryChange = (event) => {
        console.log("handleCategoryChange called");
        console.log("event");
        console.log(event);
        dispatch(setCategory(event.target.value));
        errorDataFormState = store.getState().errorData.errorData;

    }

    const handleDeviceClassNameChange = (event) => {
        dispatch(setDeviceClassName(event.target.value));
        errorDataFormState = store.getState().errorData;
    }

    const handleSubmit = (event) => {
        errorDataFormState = store.getState().errorData.errorData;
        console.log("errorDataFormState");
        console.log(errorDataFormState);
        event.preventDefault();
        if (event.nativeEvent.submitter.name == "search") {
            let payload = {
                "errorDataList": [{
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": errorDataFormState.description,
                    "tag": errorDataFormState.tag
                }]
            };
            dispatch(searchAsync(payload));
        }

        else if (event.nativeEvent.submitter.name == "add") {

            if (errorDataFormState.category != '') {
                setCategoryErrorState(false);
            } else {
                setCategoryErrorState(true);
            }
            if (errorDataFormState.deviceClassName != '') {
                setDeviceClassNameErrorState(false);
            }
            else {
                setDeviceClassNameErrorState(true);
            }
            if (errorDataFormState.code != '') {
                setCodeErrorState(false);
            }
            else {
                setCodeErrorState(true);
            }
            if (errorDataFormState.description != '') {
                setDescriptionErrorState(false);
            }
            else {
                setDescriptionErrorState(true);
            }

            AddErrorDatas({
                "errorDataArray": [{
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": errorDataFormState.description,
                    "tag": errorDataFormState.tag
                }]
            });
        }

        else if (event.nativeEvent.submitter.name == "update") {
            UpdateErrorDatas({
                "errorDataArray": [{
                    "id": errorDataFormState.id,
                    "category": errorDataFormState.category,
                    "deviceClassName": errorDataFormState.deviceClassName,
                    "code": errorDataFormState.code,
                    "description": errorDataFormState.description,
                    "tag": errorDataFormState.tag
                }]
            });
        }

        else if (event.nativeEvent.submitter.name == "delete") {
            DeleteErrorDatas({ "id": errorDataFormState.id });
        }
        else if (event.nativeEvent.submitter.name == "reset") {
            dispatch(setId(0));
            dispatch(setCategory(''));
            dispatch(setDeviceClassName(''));
            dispatch(setErrorCode(''));
            dispatch(setDescription(''));
            dispatch(setTag(''));
            dispatch(setRowSelection([]));
        }
    }

    const categories = [
        {
            value: '',
            label: '',
        },
        {
            value: 'XFS',
            label: 'XFS',
        },
        {
            value: 'Simax',
            label: 'Simax',
        },
    ];

    const deviceClassNames = [
        {
            value: '',
            label: '',
        },
        {
            value: 'XFSGeneral',
            label: 'XFSGeneral',
        },
        {
            value: 'PTR',
            label: 'PTR',
        },
        {
            value: 'IDC',
            label: 'IDC',
        },
        {
            value: 'CDM',
            label: 'CDM',
        },
        {
            value: 'PIN',
            label: 'PIN',
        },
        {
            value: 'CHK',
            label: 'CHK',
        },
        {
            value: 'DEP',
            label: 'DEP',
        },
        {
            value: 'TTU',
            label: 'TTU',
        },
        {
            value: 'SIU',
            label: 'SIU',
        },
        {
            value: 'VDM',
            label: 'VDM',
        },
        {
            value: 'CAM',
            label: 'CAM',
        },
        {
            value: 'ALM',
            label: 'ALM',
        },
        {
            value: 'CEU',
            label: 'CEU',
        },
        {
            value: 'CIM',
            label: 'CIM',
        },
        {
            value: 'CRD',
            label: 'CRD',
        },
        {
            value: 'BCR',
            label: 'BCR',
        },
        {
            value: 'IPM',
            label: 'IPM',
        },
        {
            value: 'BIO',
            label: 'BIO',
        },
    ];

    return (
        <div id="form" className="form">
            <header className="form-header">
            </header>

            <Box
                component="form"
                onSubmit={handleSubmit}
                sx={{ display: 'flex' }}
                noValidate
                autoComplete="off"
                sx={{ width: 'auto' }}
            >
                <AppBar position="static" color="primary" sx={{ m: 1 }}>
                    <Toolbar>
                        <IconButton
                            size="large"
                            edge="start"
                            color="default"//inherit
                            aria-label="menu"
                            sx={{ mr: 2 }}
                        >
                            <MenuIcon />
                        </IconButton>
                        <Typography variant="h6" component="div" sx={{ flexGrow: 1 }}>
                            ATM Error Data Manager
                        </Typography>

                        <Logout />

                    </Toolbar>
                </AppBar>
                <div>
                    <TextField
                        id="outlined-basic-errorCode"
                        label="Error Code"
                        variant="outlined"
                        size="small"
                        error={codeErrorState}
                        value={errorDataFormState.code}
                        onChange={(e) => dispatch(setErrorCode(e.target.value))}
                        sx={{ m: 1, width: '25ch' }}
                    >
                    </TextField>

                    <TextField
                        id="outlined-select-category"
                        select
                        label="Category"
                        defaultValue={errorDataFormState.category}
                        size="small"
                        error={categoryErrorState}
                        value={errorDataFormState.category}
                        onChange={handleCategoryChange}
                        sx={{ m: 1, width: '25ch' }}
                    >
                        {categories.map((option) => (
                            <MenuItem key={option.value} value={option.value} sx={{ fontSize: 12 }}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>

                    <TextField
                        id="outlined-select-deviceClassName"
                        select
                        label="Device Class"
                        defaultValue=""
                        size="small"
                        error={deviceClassNameErrorState}
                        value={errorDataFormState.deviceClassName}
                        onChange={handleDeviceClassNameChange}
                        sx={{ m: 1, width: '25ch' }}
                    >
                        {deviceClassNames.map((option) => (
                            <MenuItem key={option.value} value={option.value} sx={{ fontSize: 12 }}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>

                    <TextField
                        id="outlined-basic-tag"
                        label="Tag"
                        variant="outlined"
                        size="small"
                        value={errorDataFormState.tag}
                        onChange={(e) => dispatch(setTag(e.target.value))}
                        sx={{ m: 1, width: '25ch' }}
                    >
                    </TextField>

                    <TextField
                        id="outlined-multiline-static-description"
                        label="Description"
                        fullWidth
                        multiline
                        rows={3}
                        defaultValue=""
                        error={descriptionErrorState}
                        value={errorDataFormState.description}
                        onChange={(e) => dispatch(setDescription(e.target.value))}
                        sx={{ m: 1 }}
                    >
                    </TextField>

                    <Button
                        variant="outlined"
                        color="info"//secondary, info
                        type="submit"
                        name="search"
                        value="Search"
                        sx={{ m: 1 }}
                        startIcon={<SearchIcon />}
                    >
                        Search
                    </Button>

                    <Button
                        variant="contained"
                        color="success"
                        type="submit"
                        name="add"
                        value="Add"
                        sx={{ m: 1 }}
                        startIcon={<AddIcon />}
                    >
                        Add
                    </Button>

                    <Button
                        variant="contained"
                        type="submit"
                        name="update"
                        value="Update"
                        sx={{ m: 1 }}
                        startIcon={<MoveUpIcon />}
                    >
                        Update
                    </Button>

                    <Button
                        variant="contained"
                        color="error"
                        type="submit"
                        name="delete"
                        value="Delete"
                        sx={{ m: 1 }}
                        startIcon={<DeleteIcon />}
                    >
                        Delete
                    </Button>

                    <Button
                        variant="outlined"
                        color="warning"
                        type="submit"
                        name="reset"
                        value="Reset"
                        sx={{ m: 1 }}
                        startIcon={<CleaningServicesIcon />}
                    >
                        Reset
                    </Button>

                    <Button
                        variant="outlined"
                        color="inherit"
                        type="submit"
                        name="bulkInsert"
                        value="Bulk Insert"
                        sx={{ m: 1 }}
                        startIcon={<FileUploadIcon />}
                    >
                        Bulk Insert
                    </Button>
                </div>
            </Box >
            <div class="container">
                <ErrorDataGrid />
            </div>
        </div >
    );
}
export default ErrorDataForm;