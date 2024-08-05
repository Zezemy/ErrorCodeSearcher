import DataTable from "react-data-table-component";

function Table() {
    return (
        <>
            <div className="container my-5">
                <DataTable />
            </div>
        </>
    );

    const columns = [
        {
            colName: 'Id',
            selector: row => row.id,
        },
        {
            colName: 'Code',
            selector: row => row.code,
        },
        {
            colName: 'Description',
            selector: row => row.description,
        },
        {
            colName: 'Category',
            selector: row => row.category,
        },
        {
            colName: 'DeviceClass',
            selector: row => row.deviceClass,
        },
        {
            colName: 'Tag',
            selector: row => row.tag,
        },
        {
            colName: 'CreatedBy',
            selector: row => row.createdBy,
        },
        {
            colName: 'CreateDate',
            selector: row => row.createDate,
        },
        {
            colName: 'UpdatedBy',
            selector: row => row.updatedBy,
        },
        {
            colName: 'UpdateDate',
            selector: row => row.updateDate,
        },
        {
            colName: '',
            selector: row => row.delete,
        },
    ];

    const data = [
        {
            Id: 1
        },
        {
            Id:2
        },
    ]

    function MyComponent() {
        return (
            <DataTable
                columns={columns}
                data={data}
                fixedHeader
                pagination
                selectableRows
            />
        );
    };
}

export default Table;