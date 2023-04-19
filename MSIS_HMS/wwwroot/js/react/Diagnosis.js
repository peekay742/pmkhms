const { Component, useState, useEffect, useRef, Fragment } = React;

const Diagnosis = (props) => {
    const [patientDiagnosis, setPatientDiagnosis] = useState(_patientDiagnosis);
    const [viewName, setViewName] = useState(_viewName);
    const [diagnoses, setDiagnoses] = useState([]);
    const [diagnosis, setDiagnosis] = useState({
        id: '',
        diagnosisId: '',
        title: '',
        fromDate: '',
        detail: ''
    });
    const [editIndex, setEditIndex] = useState(-1);
    const [status, setStatus] = useState('');
    const [error, setError] = useState('');
    useEffect(() => {
        console.log("ViewName:" + viewName);
        setViewName(viewName);
        $('#DiagnosisModel').on('hidden.bs.modal', function (e) {
            clearForm();
            setStatus('');
            setError('');
        })

        $.ajax({
            url: `/diagnosis/GetDiagnosis`,
            type: 'get',
            success: function (res) {
                setDiagnoses(JSON.parse(JSON.stringify(res.map(x => ({ label: x.name, value: x.id })) || [])));
            }
        });
    }, [])

    useEffect(() => {
        console.log(diagnosis)
    }, [diagnosis])

    const clearForm = () => {
        setDiagnosis(JSON.parse(JSON.stringify({
            id: '',
            diagnosisId: '',
            title: '',
            fromDate: '',
            detail: ''
        })));
        setEditIndex(-1);
    }

    const setState = (key, value) => {
        console.log(key, value)
        if (key == 'diagnosisId') {
            if (value) {
                diagnosis['diagnosisId'] = value.value;
                diagnosis['title'] = value.label;
            } else {
                diagnosis['diagnosisId'] = null;
            }
        } else {
            diagnosis[key] = value;
        }
        setDiagnosis(JSON.parse(JSON.stringify(diagnosis)));
    }

    const onSave = async () => {
        await setStatus('');
        await setError('');
        if (diagnosis && diagnosis.title) {
            if (editIndex > -1) {
                onEdit();
            } else {
                onAdd();
            }
        } else {
            setError('* fields are required.');
        }
    }

    const onAdd = () => {
        patientDiagnosis.push(diagnosis);
        setPatientDiagnosis(JSON.parse(JSON.stringify(patientDiagnosis)));
        setStatus('success');
        clearForm();
    }

    const onClickEdit = (index) => {
        setDiagnosis(JSON.parse(JSON.stringify(patientDiagnosis[index])));
        setEditIndex(index);
        $("#DiagnosisModel").modal('show');
    }

    const onEdit = () => {
        patientDiagnosis[editIndex] = diagnosis;
        setPatientDiagnosis(JSON.parse(JSON.stringify(patientDiagnosis)));
        $("#DiagnosisModel").modal('hide');
    }

    const onClickDelete = (index) => {
        arrayRemoveByIndex(patientDiagnosis, index);
        setPatientDiagnosis(JSON.parse(JSON.stringify(patientDiagnosis)));
    }
    return (

        <div className="row">
            <div className="col-xs-12">
                {viewName ? <div></div> : <button type="button" className="btn btn-click btn-primary gradient-blue pull-right" data-toggle="modal" data-target="#DiagnosisModel">Add Diagnosis</button>

                }
                <table id="example" className="display table table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>Title</th>
                            <th>Detail</th>
                            <th>From Date</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            patientDiagnosis.map((diagnosis, index) =>
                                <tr>
                                    <td>
                                        {/* <input name={`PatientSymptoms[${index}].Id`} value={symptom.id} hidden /> */}
                                        <input name={`PatientDiagnoses[${index}].DiagnosisId`} value={diagnosis.diagnosisId} hidden />
                                        {index + 1}
                                    </td>
                                    <td><input name={`PatientDiagnoses[${index}].Title`} value={diagnosis.title} hidden />{diagnosis.title}</td>
                                    <td><input name={`PatientDiagnoses[${index}].FromDate`} value={diagnosis.fromDate} hidden />{diagnosis.fromDate}</td>
                                    <td><input name={`PatientDiagnoses[${index}].Detail`} value={diagnosis.detail} hidden />{diagnosis.detail}</td>
                                    {viewName ? <td></td> : <td>
                                        <a onClick={() => onClickEdit(index)} className="btn btn-xs btn-secondary" style={{ marginRight: 3 }}>Edit</a>
                                        <a onClick={() => onClickDelete(index)} className="btn btn-xs btn-secondary">Delete</a>
                                    </td>

                                    }

                                </tr>
                            )
                        }
                    </tbody>
                </table>
            </div>
            <div className="modal fade" id="DiagnosisModel" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title pull-left">{editIndex > -1 ? 'Edit' : 'Add'} Symptom</h5>
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
                                                <strong>Success:</strong> Diagnosis added successfully.</div>
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
                                        <label className="form-label" for="Name">Diagnosis</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <Select
                                                isClearable={true}
                                                value={(!!diagnosis.diagnosisId && diagnosis.diagnosisId > 0) ? diagnoses.find(x => x.value == diagnosis.diagnosisId) : null}
                                                onChange={(e) => { console.log(e); setState('diagnosisId', e) }}
                                                options={diagnoses}
                                            />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Title</label>
                                        <span className="desc">*</span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('title', e.target.value)} value={diagnosis.title} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">From Date</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('fromDate', e.target.value)} value={diagnosis.fromDate} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Detail</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <textarea className="form-control" onChange={(e) => setState('detail', e.target.value)} value={diagnosis.detail}></textarea>
                                        </div>
                                    </div>
                                </div>
                            </form>
                        </div>
                        <div className="modal-footer">
                            <button type="button" className="btn btn-secondary" data-dismiss="modal">Back</button>
                            <button type="button" className="btn btn-primary" onClick={onSave}>{editIndex > -1 ? 'Save' : 'Add'}</button>
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
            <Diagnosis {...this.state} reset={this.clear} />
        );
    }
}

const rootElement = document.getElementById('diagnosis_root')
ReactDOM.render(<App ref={component => window.diagnosisComponent = component} />, rootElement)