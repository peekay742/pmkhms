const { Component, useState, useEffect, useRef, Fragment } = React;

const NursingNote = (props) => {
    const [patientNursingNotes, setPatientNursingNotes] = useState(_patientNursingNotes);
    const [viewName, setViewName] = useState(_viewName);
    const [nursingNotes, setNursingNotes] = useState([]);
    const [nursingNote, setNursingNote] = useState({
        id: '',
        Routine_Complain: '',
        NursingAction: '',
        fromDate: '',
        nurseName:''
    });

    const [editIndex, setEditIndex] = useState(-1);
    const [status, setStatus] = useState('');
    const [error, setError] = useState('');
    useEffect(() => {
        console.log("ViewName:" + viewName);
        setViewName(viewName);
        $('#NursingNoteModal').on('hidden.bs.modal', function (e) {
            clearForm();
            setStatus('');
            setError('');
        })

        //$.ajax({
        //    url: `/symptoms/GetAllByDoctor?id=${_doctorId}`,
        //    type: 'get',
        //    success: function (res) {
        //        setSymptoms(JSON.parse(JSON.stringify(res.map(x => ({ label: x.name, value: x.id })) || [])));
        //    }
        //});
    }, [])

    useEffect(() => {
        console.log(nursingNote)
    }, [nursingNote])

    const clearForm = () => {
        setNursingNote(JSON.parse(JSON.stringify({
            id: '',
            Routine_Complain: '',
            NursingAction: '',
            fromDate: '',
            nurseName: ''
        })));
        setEditIndex(-1);
    }

    const setState = (key, value) => {
        //console.log(key, value)
        //if (key == 'symptomId') {
        //    if (value) {
        //        symptom['symptomId'] = value.value;
        //        symptom['title'] = value.label;
        //    } else {
        //        symptom['symptomId'] = null;
        //    }
        //} else {
        nursingNote[key] = value;
        
        setNursingNote(JSON.parse(JSON.stringify(nursingNote)));
    }

    const onSave = async () => {
        await setStatus('');
        await setError('');
        if (nursingNote) {
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
        patientNursingNotes.push(nursingNote);
        setPatientNursingNotes(JSON.parse(JSON.stringify(patientNursingNotes)));
        setStatus('success');
        clearForm();
    }

    const onClickEdit = (index) => {
        setNursingNote(JSON.parse(JSON.stringify(patientNursingNotes[index])));
        setEditIndex(index);
        $("#NursingNoteModal").modal('show');
    }

    const onEdit = () => {
        patientNursingNotes[editIndex] = nursingNote;
        patientNursingNotes(JSON.parse(JSON.stringify(patientNursingNotes)));
        $("#NursingNoteModal").modal('hide');
    }

    const onClickDelete = (index) => {
        arrayRemoveByIndex(patientNursingNotes, index);
        setPatientNursingNotes(JSON.parse(JSON.stringify(patientNursingNotes)));
    }

    return (
        <div className="row">
            <div className="col-xs-12">
                {viewName ? <div></div> : <button type="button" className="btn btn-click btn-primary gradient-blue pull-right" data-toggle="modal" data-target="#NursingNoteModal">Add Nursing Note</button>

                }

                <table id="example" className="display table table-hover table-condensed">
                    <thead>
                        <tr>
                            <th>No</th>
                            <th>Routine/Complain</th>
                            <th>Nursing Action</th>
                            <th>Date/Time</th>
                            <th>Name</th> 
                            <th></th>
                        </tr>
                    </thead>
                    <tbody>
                        {
                            patientNursingNotes.map((nursingNote,index)=>
                                <tr>
                                    <td>
                                        <input name={`PatientNursingNotes[${index}].Id`} value={nursingNote.Id} hidden />
                                        {index + 1}
                                    </td>
                                    <td>
                                        <input name={`PatientNursingNotes[${index}].RoutineComplain`} value={nursingNote.Routine_Complain} hidden />{nursingNote.Routine_Complain}
                                    </td>
                                    <td>
                                        <input name={`PatientNursingNotes[${index}].NursingAction`} value={nursingNote.NursingAction} hidden />{nursingNote.NursingAction}
                                    </td>
                                    <td>
                                        <input name={`PatientNursingNotes[${index}].fromDate`} value={nursingNote.fromDate} hidden />{nursingNote.fromDate}
                                    </td>
                                    <td>
                                        <input name={`PatientNursingNotes[${index}].Name`} value={nursingNote.nurseName} hidden />{nursingNote.nurseName}
                                    </td>
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
            <div className="modal fade" id="NursingNoteModal" tabindex="-1" role="dialog" aria-labelledby="exampleModalLabel" aria-hidden="true">
                <div className="modal-dialog" role="document">

                    <div className="modal-content">

                        <div className="modal-header">
                            <h5 className="modal-title pull-left">{editIndex > -1 ? 'Edit' : 'Add'} Nursing Note</h5>
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
                                                <strong>Success:</strong> Nursing Note added successfully.</div>
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
                                        <label className="form-label" for="Name">Routine/Complain</label>
                                        <span className="desc">*</span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('Routine_Complain', e.target.value)} value={nursingNote.Routine_Complain} />
                                        </div>
                                    </div>

                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Nursing Action</label>
                                        <span className="desc">*</span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('NursingAction', e.target.value)} value={nursingNote.NursingAction} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Date/Time</label>
                                        <span className="desc"></span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('fromDate', e.target.value)} value={nursingNote.fromDate} />
                                        </div>
                                    </div>
                                    <div className="col-xs-12 form-group">
                                        <label className="form-label" for="Name">Nurse Name</label>
                                        <span className="desc">*</span>
                                        <div className="controls">
                                            <input className="form-control" onChange={(e) => setState('nurseName', e.target.value)} value={nursingNote.nurseName} />
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
            <NursingNote {...this.state} reset={this.clear} />
        );
    }
}

const rootElement = document.getElementById('nursingNote_root')
ReactDOM.render(<App ref={component => window.nursingNoteComponent = component} />, rootElement)


