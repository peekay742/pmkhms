const { Component, useState, useEffect, useRef, Fragment } = React;

const Oncall = (props) => {
    const [oncalls, setOncalls] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [oncall, setOncall] = useState({
        id: '',
        date: _selectedDate,
        iPDRecordId: _iPDRecordId,
        doctorId: '',
        fee: '',
        sortOrder: ''
    });

    const [editId, setEditId] = useState(-1);
    const [status, setStatus] = useState('');
    const [error, setError] = useState('');

    const GetAllIPDOncalls = () => {
        $.ajax({
            url: `/ipdrecords/GetIPDOncalls?id=${_iPDRecordId}&date=${_selectedDate}`,
            type: 'get',
            success: function (res) {

                setOncalls(JSON.parse(JSON.stringify(res)));

            }
        });
    }

    useEffect(() => {
        $('#OncallModal').on('hidden.bs.modal', function (e) {
            clearForm();
            setStatus('');
            setError('');
        })
        GetAllIPDOncalls();
        $.ajax({
            url: `/doctors/GetAll?DepartmentType=2`,
            type: 'get',
            success: function (res) {
                setDoctors(JSON.parse(JSON.stringify(res || [])));
            }
        });
    }, [])

    useEffect(() => {
        console.log(oncall)
    }, [oncall])

    const clearForm = () => {
        setOncall(JSON.parse(JSON.stringify({
            id: '',
            date: _selectedDate,
            iPDRecordId: _iPDRecordId,
            doctorId: '',
            sortOrder: ''
        })));
        setEditId(-1);
    }

    const setState = (key, value) => {
        console.log('Setting state:', key, value);
        if (key == "doctorId") {
            if (value) {
                console.log(value)
                var selectedOncall = doctors.find(x => x.id == value);
                console.log(doctors, value, selectedOncall)
                if (selectedOncall) {
                    oncall["fee"] = selectedOncall.oncallFee || '';
                }
            } else {
                oncall["fee"] = '';
            }
        }
        oncall[key] = value;
        console.log(oncall)
        setOncall(JSON.parse(JSON.stringify(oncall)));
    }


    const onSave = async () => {
        await setStatus('');
        await setError('');
        if (oncall && oncall.doctorId) {
            if (editId > -1) {
                onEdit();
            } else {
                onAdd();
            }
        } else {
            setError('* fields are required.');
        }
    }

    const onAdd = () => {
        $.ajax({
            url: `/ipdrecords/AddOncall`,
            type: 'post',
            data: oncall,
            success: function (res) {
                // rounds.push(round);
                // setRounds(JSON.parse(JSON.stringify(res)));
                GetAllIPDOncalls();
                setStatus('success');
                clearForm();
            }
        });
    }

    const onClickEdit = (id) => {

        setOncall(JSON.parse(JSON.stringify(oncalls.find(x => x.id == id))));
        setEditId(id);
        $("#OncallModal").modal('show');
    }

    const onEdit = () => {
        $.ajax({
            url: `/ipdrecords/UpdateOncall`,
            type: 'post',
            data: oncall,
            success: function (res) {
                GetAllIPDOncalls();
                $("#OncallModal").modal('hide');
            }
        });
    }

    const onClickDelete = (id) => {
        DeleteFnc(() => onDelete(id));
    }

    const onDelete = (id) => {
        $.ajax({
            url: `/ipdrecords/DeleteOncall/${id}`,
            type: 'get',
            success: function (res) {
                DeleteAlert('center', 'success', 'Oncall');
                GetAllIPDOncalls();
            }
        });
    }

    return (
        <div className="row">
            <div className="col-xs-12">
                <button type="button" className="btn btn-click btn-primary gradient-blue pull-right form-group" data-toggle="modal" data-target="#OncallModal">Add Oncall</button>
                <table id="example" className="table vm table-small-font no-mb table-bordered table-striped">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>Doctor</th>
                            <th>Oncall Fee</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            oncalls.map((oncall, index) =>
                                <tr>
                                    <td>{index + 1}</td>
                                    <td>{oncall.doctorName}</td>
                                    <td>{oncall.fee}</td>
                                   
                                    <td>
                                        <a onClick={() => onClickEdit(oncall.id)} className="btn btn-xs btn-secondary" style={{ marginRight: 3 }}>Edit</a>
                                        <a onClick={() => onClickDelete(oncall.id)} className="btn btn-xs btn-secondary">Delete</a>
                                    </td>
                                </tr>
                            )
                        }
                    </tbody>
                </table>
            </div>
            <div className="modal fade" id="OncallModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-drug pull-left">{editId > -1 ? 'Edit' : 'Add'} Oncall</h5>
                            <button type="button" className="close pull-right" data-dismiss="modal" aria-label="Close">
                                <span aria-hidden="true">&times;</span>
                            </button>
                        </div>
                    
                        <div className="modal-body">
                            <form>
                                <div className="row">
                                    {
                                        status === 'success' &&
                                        <div className="col-xs-12">
                                            <div className="alert alert-success alert-dismissible fade in">
                                                <button type="button" className="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                                <strong>Success:</strong> Oncall added successfully.</div>
                                        </div>
                                    }
                                    {
                                        error &&
                                        <div className="col-xs-12">
                                            <div className="alert alert-warning alert-dismissible fade in">
                                                <button type="button" className="close" data-dismiss="alert" aria-label="Close"><span aria-hidden="true">×</span></button>
                                                <strong>Warning:</strong> {error}.</div>
                                        </div>
                                    }
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Date</label>
                                        <span className="desc">*</span>
                                        <div className="controls">
                                            <input type="date" className="form-control" onChange={(e) => setState('date', e.target.value)} value={moment(oncall.date).format('yyyy-MM-DD')} />
                                        </div>
                                    </div>
                                    {
                                        doctors.length > 0 &&
                                        <div className="col-xs-12 form-group">
                                            <label className="form-label" for="Name">Doctor</label>
                                            <span className="desc">*</span>
                                            <div className="controls">
                                                <Select
                                                        isClearable={true}
                                                        value={(!!oncall.doctorId && oncall.doctorId > 0) ? doctors.map(x => ({ label: x.name, value: x.id })).find(x => x.value == oncall.doctorId) : null}
                                                        onChange={(e) => { setState('doctorId', e ? e.value : null) }}
                                                        options={doctors.map(x => ({ label: x.name, value: x.id }))}
                                                    />
                                               
                                            </div>
                                        </div>
                                    }
                                
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Oncall Fee</label>
                                        <span className="desc">*</span>
                                        <div className="controls">
                                            <input type="number" className="form-control" onChange={(e) => setState('fee', e.target.value)} value={oncall.fee} />
                                        </div>
                                    </div>

                                </div>
                            </form>
                        </div>
                    
                    <div className="modal-footer">
                        <button type="button" className="btn btn-secondary" data-dismiss="modal">Back</button>
                        <button type="button" className="btn btn-primary" onClick={onSave}>{editId > -1 ? 'Save' : 'Add'}</button>
                    </div>
                </div>
            </div>
            </div>
        </div>


        );
}

class App extends Component {
    constructor(props) {
        super(props);

        this.state = {

        }

        this.clear = this.clear.bind(this);
    }

    componentWillUnmount() {
        this.clear();
    }

    clear = () => this.setState({});

    render() {
        return (
            <Oncall {...this.state} reset={this.clear} />
        );
    }
}

const rootElement = document.getElementById('oncall_root')
ReactDOM.render(<App ref={component => window.oncallComponent = component} />, rootElement)