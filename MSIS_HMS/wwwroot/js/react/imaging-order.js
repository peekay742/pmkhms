const { Component, useState, useEffect, useRef, Fragment } = React;
$(document).ready(function () {

});
const OrderForm = (props) => {
    const [labTests, setLabTests] = useState([]);
    const [referrers, setReferrers] = useState([]);
    const [imgResults, setImgResults] = useState([]);
    const [imgOrderTests, setImgOrderTests] = useState(_imgOrderTests);
    const [total, setTotal] = useState(0);
    const [tax, setTax] = useState(_tax);
    const [discount, setDiscount] = useState(_discount);
    const [error, setError] = useState(_error);

    useEffect(() => {
        console.log("ImaginOrderTest", imgOrderTests);
        $.ajax({
            url: `/labTests/GetAll?isImaging=${true}`,
            type: 'get',
            success: function (res) {
                setLabTests(JSON.parse(JSON.stringify(res || [])));
                onAddImgTest();
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });

        $.ajax({
            url: `/referrers/GetAll`,
            type: 'get',
            success: function (res) {
                setReferrers(JSON.parse(JSON.stringify(res || [])));
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });
        $.ajax({
            url: `/ImagingResult/GetAll`,
            type: 'get',
            success: function (res) {
                setImgResults(JSON.parse(JSON.stringify(res || [])));
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });
    }, []);

    useEffect(() => {
        console.log(imgResults.map(x => ({ value: x.id, label: `${x.resultNo}` })).find(x => x.value == 1))
    }, [imgResults])
    useEffect(() => {
        setTotal(calculateTotal(imgOrderTests.map(x => ({ amount: x.qty * x.unitPrice })), "amount"));
    }, [imgOrderTests])

    const onAddImgTest = () => {
        debugger;
        var imgOrderTest = {
            labTestId: '',
            referrerId: '',
            imgResultId: '',
            qty: 1,
            feeType: 0,
            fee: 0,
            referralFee: 0,
            unitPrice: 0,
        };
        imgOrderTests.push(imgOrderTest);
        setImgOrderTests(JSON.parse(JSON.stringify(imgOrderTests)));
    }
    const onUpdateLabTest = (index, key, value) => {
        console.log(index, key, value)
        imgOrderTests[index][key] = value;
        setImgOrderTests(JSON.parse(JSON.stringify(imgOrderTests)));
    }

    const onDeleteLabTest = (index) => {
        arrayRemoveByIndex(imgOrderTests, index);
        setImgOrderTests(JSON.parse(JSON.stringify(imgOrderTests)));
    }
    return (
        <div className="col-md-12 form-group">
            {
                error && <span className="text-danger mb-5">{error}</span>
            }
            <table className="display table table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="25%">Test</th>
                        <th width="25%">Referrer</th>
                        <th width="20%">Result No</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(imgOrderTests, "sortOrder").map((imgOrderTest, index) => {
                            return (
                                <ImgOrderTestRow key={index}
                                    imgOrderTest={imgOrderTest}
                                    referrers={referrers}
                                    imgResults={imgResults}
                                    index={index}
                                    labTests={labTests}
                                    onUpdateLabTest={onUpdateLabTest}
                                    onAddImgTest={onAddImgTest}
                                    onDeleteLabTest={onDeleteLabTest}
                                    isLastLabTest={index == (imgOrderTests.length - 1)}
                                />
                            )
                        })
                    }
                </tbody>
                <tfoot>
                    <tr>
                        <td colSpan={4} className="money">Sub Total</td>
                        <td className="money">{total.formatMoney()}</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colSpan={4} className="money pt-td">Tax</td>
                        <td><input type="number" name={`Tax`} className="form-control money-input" onChange={(e) => setTax(Number(e.target.value))} value={tax} /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colSpan={4} className="money pt-td">Discount</td>
                        <td><input type="number" name={`Discount`} className="form-control money-input" onChange={(e) => setDiscount(Number(e.target.value))} value={discount} /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colSpan={4} className="money">Grand Total</td>
                        <td className="money">{(Number(total) + Number(tax) - Number(discount)).formatMoney()}</td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    );
}
const ImgOrderTestRow = (props) => {
    const { labTests, referrers, imgResults, index, imgOrderTest, onUpdateLabTest, onAddImgTest, onDeleteLabTest, isLastLabTest } = props;

    useDidUpdateEffect(() => {
        var selectedLabTest = labTests.find(x => x.id == imgOrderTest.labTestId);
        if (selectedLabTest) {
            onUpdateLabTest(index, 'unitPrice', selectedLabTest.unitPrice);
            if (imgOrderTest.qty == 0) {
                onUpdateLabTest(index, 'qty', 1);
            }
            if (isLastLabTest) {
                onAddImgTest();
            }
        } else {
            onUpdateLabTest(index, 'qty', 0);
        }
    }, [imgOrderTest.labTestId])

    var name = imgOrderTest && imgOrderTest.labTestId && imgOrderTest.labTestId > 0 ? `${_serviceType}[${index}].` : '';

    return (
        <React.Fragment>
            <tr>
                <td>
                    <input name={`${name}SortOrder`} value={index + 1} hidden />
                    {index + 1}
                </td>
                <td>
                    <input name={`${name}LabTestId`} value={imgOrderTest.labTestId} hidden />
                    <Select
                        value={isLastLabTest ? null : labTests.map(x => ({ value: x.id, label: `${x.name} (${x.code})` })).find(x => x.value == imgOrderTest.labTestId)}
                        onChange={(e) => onUpdateLabTest(index, 'labTestId', e.value)}
                        options={labTests.map(x => ({ value: x.id, label: `${x.name} (${x.code})` }))}
                    />
                </td>
                <td>
                    <input name={`${name}ReferrerId`} value={imgOrderTest.referrerId} hidden />
                    <Select
                        isClearable={true}
                        value={isLastLabTest ? null :referrers.map(x => ({ value: x.id, label: `${x.name}` })).find(x => x.value == imgOrderTest.referrerId)}
                        onChange={(e) => onUpdateLabTest(index, 'referrerId', e ? e.value : '')}
                        options={referrers.map(x => ({ value: x.id, label: `${x.name}` }))}
                    />
                </td>
                <td>
                    <input name={`${name}LabResultId`} value={imgOrderTest.labResultId} hidden />
                    <input name={`${name}UnitPrice`} value={imgOrderTest.unitPrice} hidden />
                    <input name={`${name}Qty`} value={imgOrderTest.qty} hidden />
                    <Select
                        isClearable={true}
                        value={isLastLabTest ? null :imgResults.map(x => ({ value: x.id, label: `${x.resultNo}` })).find(x => x.value == imgOrderTest.imgaingResultId)}
                        onChange={(e) => onUpdateLabTest(index, 'labResultId', e ? e.value : '')}
                        options={imgResults.map(x => ({ value: x.id, label: `${x.resultNo}` }))}
                    />
                </td>
                <td className="money pt-td">{((imgOrderTest.qty || 0) * (imgOrderTest.unitPrice || 0)).formatMoney()}</td>
                <td className="pt-td">
                    {
                        !isLastLabTest && <button type="button" onClick={() => onDeleteLabTest(index)} className="btn btn-xs btn-secondary">Delete</button>
                    }
                </td>
            </tr>
        </React.Fragment>
    )
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
            <OrderForm {...this.state} reset={this.clear} />
        );
    }
}

const rootElement = document.getElementById('root')
ReactDOM.render(<App ref={component => window.reactComponent = component} />, rootElement)