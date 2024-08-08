//import { useState, useCallback, useMemo } from 'react';
//import DataTable from "react-data-table-component";
//import { useDispatch, useSelector } from 'react-redux';
//import { searchAsync, selectColumns, selectData, selectStatus } from './errorDataApiSearchSlice';
//import { setId, setCategory, setDeviceClassName, setErrorCode, setDescription, setTag, selectState } from './errorDataFormSlice';

//function Table() {
//    const dispatch = useDispatch();
//    const storedData = useSelector(selectData);
//    const columns = useSelector(selectColumns);
//    return (
//        <>
//            <div className="container my-5">
//                <MyComponent />
//            </div>
//        </>
//    );

//    function MyComponent() {
//        //const handleChange = ({ selectedRows }) => {
//        //    console.log('Selected Rows: ', selectedRows);
//        //    if (selectedRows.length == 1) {
//        //        dispatch(setCategory(selectedRows[0].category));
//        //    }
//        //};

//        const [selectedRows, setSelectedRows] = useState([]);
//        const [toggleCleared, setToggleCleared] = useState(false);
//        const [data, setData] = useState(storedData);
//        const handleRowSelected = ({ selectedRows }) => {
//            setSelectedRows(selectedRows);
//            if (selectedRows.length == 1) {
//                dispatch(setId(selectedRows[0].id));
//                dispatch(setCategory(selectedRows[0].category));
//                dispatch(setDeviceClassName(selectedRows[0].deviceClassName));
//                dispatch(setErrorCode(selectedRows[0].code));
//                dispatch(setDescription(selectedRows[0].description));
//                dispatch(setTag(selectedRows[0].tag));
//            }
//            else {
//                handleClearRows();
//            }
//        };

//        const handleClearRows = () => {
//            setToggleCleared(!toggleCleared);
//        }

//        const contextActions = useMemo(() => {
//            const handleDelete = () => {
//                if (window.confirm(`Are you sure you want to delete:\r ${selectedRows.map(r => r.title)}?`)) {
//                    setToggleCleared(!toggleCleared);
//                    /*                    setData(differenceBy(data, selectedRows, 'title'));*/
//                }
//            };
//            return <button key="delete" onClick={handleDelete} style={{
//                backgroundColor: 'red'
//            }} icon="true">
//                Delete
//            </button>;
//        }, [data, selectedRows, toggleCleared]);

//        const customStyles = {
//            cells: {
//                borderLeftStyle: 'solid',
//                borderLeftWidth: '1px',
//                borderLeftColor: 'black',
//            }
//        };

//        return (
//            <>
//                <DataTable
//                    title=" "
//                    columns={columns}
//                    data={data} selectableRows
//                    contextActions={contextActions}
//                    onSelectedRowsChange={handleRowSelected}
//                    clearSelectedRows={toggleCleared}
//                    fixedHeader
//                    pagination
//                    dense
//                    customStyles={customStyles}
//                />;
//            </>);

//        //return (
//        //    <DataTable
//        //        columns={columns}
//        //        data={storedData}
//        //        pagination
//        //        selectableRows
//        //        onSelectedRowsChange={handleChange}
//        //        dense
//        //    />
//        //);
//    };
//}

//export default Table;