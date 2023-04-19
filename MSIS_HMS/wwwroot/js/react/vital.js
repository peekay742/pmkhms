const { Component, useState, useEffect, useRef, Fragment } = React;

const Vital = (props) => {
    const [patientVitals, setPatientVitals] = useState(_patientVitals);
    const [viewName, setViewName] = useState(_viewName);
    const [vitals, setVitals] = useState([]);
    const [vital, setVital] = useState({
        id: '',
        gcs: '',
        bpSystolic: '',
        bpDiastolic: '',
        temperature: '',
        pulse: '',
        spo2: '',
        respiratoryRate: '',
        weight: '',
        height: ''

    });
    const [editIndex, setEditIndex] = useState(-1);
    const [status, setStatus] = useState('');
    const [error, setError] = useState('');
    useEffect(() => {
        console.log("ViewName:" + viewName);
        setViewName(viewName);
        $('#VitalModal').on('hidden.bs.modal', function (e) {
            clearForm();
            setStatus('');
            setError('');
        })
    }, [])

    useEffect(() => {
        console.log(vital)
    }, [vital])

    const clearForm = () => {
        setVital(JSON.parse(JSON.stringify({
            id: '',
            gcs: '',
            bpSystolic: '',
            bpDiastolic: '',
            temperature: '',
            pulse: '',
            spo2: '',
            respiratoryRate: '',
            weight: '',
            height: ''
        })));
        setEditIndex(-1);
    }

    const setState = (key, value) => {
    //    console.log(key, value)
    //    if (key == 'symptomId') {
    //        if (value) {
    //            symptom['symptomId'] = value.value;
    //            symptom['title'] = value.label;
    //        } else {
    //            symptom['symptomId'] = null;
    //        }
    //    } else {
        vital[key] = value;
        
        setVital(JSON.parse(JSON.stringify(vital)));
    }

    const onSave = async () => {
        await setStatus('');
        await setError('');
        if (vital) {
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
        patientVitals.push(vital);
        setPatientVitals(JSON.parse(JSON.stringify(patientVitals)));
        setStatus('success');
        clearForm();
    }

    const onClickEdit = (index) => {
        setVital(JSON.parse(JSON.stringify(patientVitals[index])));
        setEditIndex(index);
        $("#VitalModal").modal('show');
    }

    const onEdit = () => {
        patientVitals[editIndex] = vital;
        setPatientVitals(JSON.parse(JSON.stringify(patientVitals)));
        $("#VitalModal").modal('hide');
    }

    const onClickDelete = (index) => {
        arrayRemoveByIndex(patientVitals, index);
        setPatientVitals(JSON.parse(JSON.stringify(patientVitals)));
    }
    return (
        <div className="row">
            <div className="col-xs-12">
                {viewName ? <div></div> : <button type="button" className="btn btn-click btn-primary gradient-blue pull-right" data-toggle="modal" data-target="#VitalModal">Add Vital</button>

                }
                <table id="example" className="display table table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>GCS</th>
                            <th>BPSystolic</th>
                            <th>BPDiastolic</th>
                            <th>Temperature</th>
                            <th>Pulse</th>
                            <th>SPO2</th>
                            <th>RespiratoryRate</th>
                            <th>Weight</th>
                            <th>Height</th>
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            patientVitals.map((vital, index) =>
                                <tr>
                                    <td>
                                         <input name={`PatientVitals[${index}].Id`} value={vital.id} hidden /> 
                                        {/*<input name={`PatientSymptoms[${index}].SymptomId`} value={symptom.symptomId} hidden />*/}
                                        {index + 1}
                                    </td>
                                    <td><input name={`PatientVitals[${index}].GCS`} value={vital.gcs} hidden />{vital.gcs}</td>
                                    <td><input name={`PatientVitals[${index}].BPSystolic`} value={vital.bpSystolic} hidden />{vital.bpSystolic}</td>
                                    <td><input name={`PatientVitals[${index}].BPDiastolic`} value={vital.bpDiastolic} hidden />{vital.bpDiastolic}</td>
                                    <td><input name={`PatientVitals[${index}].Temperature`} value={vital.temperature} hidden />{vital.temperature}</td>
                                    <td><input name={`PatientVitals[${index}].Pulse`} value={vital.pulse} hidden />{vital.pulse}</td>
                                    <td><input name={`PatientVitals[${index}].SPO2`} value={vital.spo2} hidden />{vital.spo2}</td>
                                    <td><input name={`PatientVitals[${index}].RespiratoryRate`} value={vital.respiratoryRate} hidden />{vital.respiratoryRate}</td>
                                    <td><input name={`PatientVitals[${index}].Weight`} value={vital.weight} hidden />{vital.weight}</td>
                                    <td><input name={`PatientVitals[${index}].Height`} value={vital.height} hidden />{vital.height}</td>
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
            <div className="modal fade" id="VitalModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog" role="document">
                    <div className="modal-content">
                        <div className="modal-header">
                            <h5 className="modal-title pull-left">{editIndex > -1 ? 'Edit' : 'Add'} Vital</h5>
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
                                                <strong>Success:</strong> Vital added successfully.</div>
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
                                        <label className="form-label" for="Name">GCS</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('gcs', e.target.value)} value={vital.gcs} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">BPSystolic</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('bpSystolic', e.target.value)} value={vital.bpSystolic} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">BPDiastolic</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('bpDiastolic', e.target.value)} value={vital.bpDiastolic} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Temperature</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('temperature', e.target.value)} value={vital.temperature} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Pulse</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('pulse', e.target.value)} value={vital.pulse} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">SPO2</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('spo2', e.target.value)} value={vital.spo2} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">RespiratoryRate</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('respiratoryRate', e.target.value)} value={vital.respiratoryRate} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Weight</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('weight', e.target.value)} value={vital.weight} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Height</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('height', e.target.value)} value={vital.height} />
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
            <Vital {...this.state} reset={this.clear} />
        );
    }
}

const rootElement = document.getElementById('vital_root')
ReactDOM.render(<App ref={component => window.vitalComponent = component} />, rootElement)