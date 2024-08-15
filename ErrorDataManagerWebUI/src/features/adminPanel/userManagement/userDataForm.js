import React, { useEffect, useState, createRef} from "react";
import '../../../../src/App.css';
import { SearchUserDatas, AddUserDatas, UpdateUserDatas, DeleteUserDatas } from './userDataApi';
import { useDispatch, useSelector } from 'react-redux';
import { searchAsync, getSelectedRows } from './userDataApiSearchSlice';
import { setId, setUserName, setPassword, setUserType, selectState } from './userDataFormSlice';
import { setRowSelection } from '../../../app/AppStateSlice';
import { store } from '../../../app/Store';
import UserDataGrid from './userDataGrid';
import Box from '@mui/material/Box';
import TextField from '@mui/material/TextField';
import MenuItem from '@mui/material/MenuItem';
import Button from '@mui/material/Button';
import SearchIcon from '@mui/icons-material/Search';
import AddIcon from '@mui/icons-material/Add';
import MoveUpIcon from '@mui/icons-material/MoveUp';
import DeleteIcon from '@mui/icons-material/Delete';
import CleaningServicesIcon from '@mui/icons-material/CleaningServices';
import sha256 from 'crypto-js/sha256';

function UserDataForm() {
    const dispatch = useDispatch();
    const selectedRows = useSelector(getSelectedRows);
    let userDataFormState = useSelector(selectState);
    const passwordRef = createRef();

    const [userNameErrorState, setUserNameErrorState] = useState(false);
    const [passwordErrorState, setPasswordErrorState] = useState(false);
    const [userTypeErrorState, setUserTypeErrorState] = useState(false);

    const handleUserNameChange = (event) => {
        console.log("handleUserNameChange called");
        console.log("event");
        console.log(event);
        dispatch(setUserName(event.target.value));
        userDataFormState = store.getState().userData.userData;
    }

    const handlePasswordChange = (event) => {
        dispatch(setPassword(sha256(event.target.value).toString()));
        userDataFormState = store.getState().userData;
    }

    const handleSubmit = (event) => {
        userDataFormState = store.getState().userData.userData;
        console.log("userDataFormState");
        console.log(userDataFormState);
        event.preventDefault();

        if (event.nativeEvent.submitter.name == "search") {
            let payload = {
                "userDataList": [{
                    "userName": userDataFormState.userName,
                    "password": userDataFormState.password,
                    "userType": userDataFormState.userType
                }]
            };
            dispatch(searchAsync(payload));
        }

        else if (event.nativeEvent.submitter.name == "add") {

            if (userDataFormState.userName != '') {
                setUserNameErrorState(false);
            }
            else {
                setUserNameErrorState(true);
            }
            if (userDataFormState.password != '') {
                setPasswordErrorState(false);
            }
            else {
                setPasswordErrorState(true);
            }
            if (userDataFormState.userType != '') {
                setUserTypeErrorState(false);
            }
            else {
                setUserTypeErrorState(true);
            }

            AddUserDatas({
                "userDataArray": [{
                    "userName": userDataFormState.userName,
                    //"password": sha256(userDataFormState.password).toString(),
                    "password": userDataFormState.password,
                    "userType": userDataFormState.userType
                }]
            });
        }

        else if (event.nativeEvent.submitter.name == "update") {
            UpdateUserDatas({
                "userDataArray": [{
                    "id": userDataFormState.id,
                    "userName": userDataFormState.userName,
                    "password": userDataFormState.password,
                    "userType": userDataFormState.userType
                }]
            });
        }

        else if (event.nativeEvent.submitter.name == "delete") {
            DeleteUserDatas({ "id": userDataFormState.id });
        }

        else if (event.nativeEvent.submitter.name == "reset") {
            dispatch(setId(0));
            dispatch(setUserName(''));
            dispatch(setPassword(''));
            dispatch(setUserType(0));
            dispatch(setRowSelection([]));
            passwordRef.current.value = null;
        }
    }

    const userTypes = [
        {
            value: '0',
            label: '',
        },
        {
            value: '1',
            label: 'Admin',
        },
        {
            value: '2',
            label: 'User',
        },
    ];

    return (
        <>
            <Box
                component="form"
                onSubmit={handleSubmit}
                sx={{ display: 'flex', flexDirection: 'column', width: 'auto' }}
                noValidate
                autoComplete="off"
            >
                <Box>
                    <TextField
                        id="outlined-select-userName"
                        label="UserName"
                        size="small"
                        error={userNameErrorState}
                        value={userDataFormState.userName}
                        onChange={handleUserNameChange}
                        sx={{ m: 2, width: '25ch' }}
                    >
                    </TextField>

                    <TextField
                        id="outlined-password"
                        label="Password"
                        size="small"
                        type="password"
                        error={passwordErrorState}
                        /*value={userDataFormState.password}*/
                        inputRef={passwordRef}
                        onChange={handlePasswordChange}
                        sx={{ m: 2, width: '25ch' }}
                    >
                    </TextField>

                    <TextField
                        id="outlined-basic-userType"
                        select
                        label="UserType"
                        defaultValue=""
                        variant="outlined"
                        size="small"
                        error={userTypeErrorState}
                        value={userDataFormState.userType}
                        onChange={(e) => dispatch(setUserType(e.target.value))}
                        sx={{ m: 2, width: '25ch' }}
                    >
                        {userTypes.map((option) => (
                            <MenuItem key={option.value} value={option.value} sx={{ fontSize: 12 }}>
                                {option.label}
                            </MenuItem>
                        ))}
                    </TextField>
                    <Button
                        variant="outlined"
                        color="info"//secondary, info
                        type="submit"
                        name="search"
                        value="Search"
                        sx={{ m: 2 }}
                        startIcon={<SearchIcon />}
                    >
                        Search
                    </Button>
                </Box>
                <Box>
                    <Button
                        variant="contained"
                        color="success"
                        type="submit"
                        name="add"
                        value="Add"
                        sx={{ m: 2 }}
                        startIcon={<AddIcon />}
                    >
                        Add
                    </Button>

                    <Button
                        variant="contained"
                        type="submit"
                        name="update"
                        value="Update"
                        sx={{ m: 2 }}
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
                        sx={{ m: 2 }}
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
                        sx={{ m: 2 }}
                        startIcon={<CleaningServicesIcon />}
                    >
                        Reset
                    </Button>
                </Box>

                <UserDataGrid />
            </Box >
        </>

    );
}
export default UserDataForm;