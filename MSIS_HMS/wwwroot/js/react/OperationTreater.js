//const { array } = require("prop-types");

const { Component, useState, useEffect, useRef, Fragment } = React;

const OTForm = (props) => {
    const { selectedItemId, selectedUnitId, selectedBatchId } = props;
    const [items, setItems] = useState([]);
    const [otitems, setOTItems] = useState([]);  //addnewbyakh
    const [asitems, setASItems] = useState([]);  //addnewbyakh
    const [services, setServices] = useState([]);
    const [instruments, setInstruments] = useState([]);
    const [referrers, setReferrers] = useState([]);
    const [doctors, setDoctors] = useState([]);
    const [aneasthetist, setAneasthetist] = useState([]);
    const [staffs, setStaffs] = useState([]);
    const [operationItems, setoperationItems] = useState(_operationItems);
    const [operationOTItems, setoperationOTItems] = useState(_operationOTItems); //addnewbyakh
    const [operationASItems, setoperationASItems] = useState(_operationASItems);//addnewbyakh
    const [operationServices, setoperationServices] = useState(_operationServices);
    const [operationInstruments, setoperationInstruments] = useState(_operationInstruments);
    const [operationDoctors, setotDoctors] = useState(_otDoctors);
    const [operationStaffs, setotStaffs] = useState(_otStaffs);
    const [operationAnaesthetist, setotAnaesthetists] = useState(_otAneasthetist);
    const [total, setTotal] = useState(0);
    const [tax, setTax] = useState(_tax);
    const [discount, setDiscount] = useState(_discount);
    const [error, setError] = useState(_error);
    const [doctorTypeEnums, setdoctorTypes] = useState([]);
    const [staffTypeEnums, setstaffTypes] = useState(_otStaffTypeEnum);
    const [chiefSurgeonFee, setChiefSurgeonFee] = useState(_surgeonFee);

    // Merge the two arrays into a new array
    const mergedItems = [...operationItems.slice(0,-1), ...operationOTItems];
    //console.log(mergedItems);
    // Convert the merged array to a JSON string and parse it
    const jsonString = JSON.stringify(mergedItems);
    const parsedJson = JSON.parse(jsonString);
    //setoperationItems(parsedJson);
    // Use the parsed JSON data as needed
    const operationItemLength = operationItems.length;
    const operationOTItemLength = operationOTItems.length;
    console.log(parsedJson);
    console.log(operationItemLength);
    console.log(operationOTItemLength);

    useEffect(() => {
        //debugger;


        console.log("OTStaffTypeEnum=", staffTypeEnums);
        console.log("ChiefSurgeon=", chiefSurgeonFee);
        $.ajax({
            url: `/outlets/GetOutletItemsByUserOutlet`,
            type: 'get',
            success: function (res) {
                setItems(JSON.parse(JSON.stringify(res || [])));
                onAddItem();
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });

        $.ajax({
            url: `/outlets/GetOutletOTItemsByUserOutlet`,
            type: 'get',
            success: function (res) {
                setOTItems(JSON.parse(JSON.stringify(res || [])));
                onAddOTItem();
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });

        $.ajax({
            url: `/outlets/GetOutletanaesthetistItemsByUserOutlet`,
            type: 'get',
            success: function (res) {
                setASItems(JSON.parse(JSON.stringify(res || [])));
                onAddASItem();
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });

        $.ajax({
            url: `/services/GetAll`,
            type: 'get',
            success: function (res) {
                setServices(JSON.parse(JSON.stringify(res || [])));
                onAddService();
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });

        $.ajax({
            url: `/instruments/GetAll`,
            type: 'get',
            success: function (res) {
                setInstruments(JSON.parse(JSON.stringify(res || [])));
                onAddInstrument();
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });

        //$.ajax({
        //    url: `/Doctors/GetAll`,
        //    type: 'get',
        //    success: function (res) {
        //        setDoctors(JSON.parse(JSON.stringify(res || [])));
        //        onAddDoctor();
        //        setAneasthetist(JSON.parse(JSON.stringify(res || [])));
        //        onAddAnaesthetist();
        //    },
        //    error: function (jqXhr, textStatus, errorMessage) {

        //    }
        //});
        $.ajax({
            url: `/Staffs/GetAllByPosition?PositionId=${2}`,
            type: 'get',
            success: function (res) {
                setDoctors(JSON.parse(JSON.stringify(res || [])));
                onAddDoctor();
                setAneasthetist(JSON.parse(JSON.stringify(res || [])));
                onAddAnaesthetist();

            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });
        $.ajax({
            url: `/Staffs/GetAllByPosition?PositionId=${1}`,
            type: 'get',
            success: function (res) {
                setStaffs(JSON.parse(JSON.stringify(res || [])));
                onAddStaff();
                
            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });
       

        $.ajax({
            url: `/Doctors/GetDoctorTypeEnum`,
            type: 'get',
            success: function (res) {
                setdoctorTypes(JSON.parse(JSON.stringify(res || [])));
                /* onAddStaff();*/
                console.log("Doctor Type=", res);

            },
            error: function (jqXhr, textStatus, errorMessage) {

            }
        });
        //debugger;
        //$.ajax({        
        //    url: `/Staffs/GetStaffTypeEnum`,
        //    type: 'get',
        //    success: function (res) {
        //        console.log("Edit=", res);
        //        setstaffTypes(JSON.parse(JSON.stringify(res || [])));
        //        /* onAddStaff();*/
        //        //console.log("Doctor Type=", res);

        //    },
        //    error: function (jqXhr, textStatus, errorMessage) {

        //    }
        //});
        
    }, []);

    useEffect(() => {
        console.log("Hello !!");
        console.log(operationItems, operationOTItems,operationASItems,operationServices, operationInstruments)
        setTotal(calculateTotal(operationItems.map(x => ({ amount: x.qty * x.unitPrice })), "amount") + calculateTotal(operationOTItems.map(x => ({ amount: x.qty * x.unitPrice })), "amount") + calculateTotal(operationASItems.map(x => ({ amount: x.qty * x.unitPrice })), "amount") + calculateTotal(operationServices.map(x => ({ amount: x.qty * x.unitPrice })), "amount") + calculateTotal(operationInstruments.map(x => ({ amount: x.qty * x.unitPrice })), "amount") + calculateTotal(operationDoctors.map(x => ({ amount: x.fee })), "amount") + calculateTotal(operationAnaesthetist.map(x => ({ amount: x.fee })), "amount") + calculateTotal(operationStaffs.map(x => ({ amount: x.fee })), "amount"));
    }, [operationItems, operationOTItems, operationASItems,operationServices, operationInstruments, operationDoctors, operationStaffs])

    const onAddItem = () => {
        var orderItem = {
            itemId: '',
            unitId: '',
            qty: 0,
            qtyInSmallestUnit: 0,
            unitPrice: 0,
            isFOC: false
        };
        operationItems.push(orderItem);
        setoperationItems(JSON.parse(JSON.stringify(operationItems)));
    }

    //addnewbyakh

    const onAddOTItem = () => {
        var orderOTItem = {
            itemId: '',
            unitId: '',
            qty: 0,
            qtyInSmallestUnit: 0,
            unitPrice: 0,
            isFOC: false
        };
        operationOTItems.push(orderOTItem);
        setoperationOTItems(JSON.parse(JSON.stringify(operationOTItems)));
    }

    const onAddASItem = () => {
        var orderASItem = {
            itemId: '',
            unitId: '',
            qty: 0,
            qtyInSmallestUnit: 0,
            unitPrice: 0,
            isFOC: false
        };
        operationASItems.push(orderASItem);
        setoperationASItems(JSON.parse(JSON.stringify(operationASItems)));
    }

    //addnewbyakh


    const onAddDoctor = () => {
        var otDoctor = {
            doctorId: '',
            otDoctorTypeEnum:'',
            fee: 0
         
        };
        operationDoctors.push(otDoctor);
        setotDoctors(JSON.parse(JSON.stringify(operationDoctors)));
    }
    const onAddAnaesthetist = () => {
        var otAnaesthetist = {
            doctorId: '',
            otDoctorTypeEnum: '',
            fee: 0

        };
        operationAnaesthetist.push(otAnaesthetist);
        setotAnaesthetists(JSON.parse(JSON.stringify(operationAnaesthetist)));
    }
    
    const onAddStaff = () => {
        var otStaff = {
            staffId: '',
            otStaffTypeEnum: 0,
            fee: 0

        };
        operationStaffs.push(otStaff);
        setotStaffs(JSON.parse(JSON.stringify(operationStaffs)));
    }
    const onUpdateItem = (index, key, value) => {
        operationItems[index][key] = value;
        setoperationItems(JSON.parse(JSON.stringify(operationItems)));
    }


    const onDeleteItem = (index) => {
        arrayRemoveByIndex(operationItems, index);
        setoperationItems(JSON.parse(JSON.stringify(operationItems)));
    }

    //addnewbyakh

    //OT

    const onUpdateOTItem = (index, key, value) => {
        operationOTItems[index][key] = value;
        setoperationOTItems(JSON.parse(JSON.stringify(operationOTItems)));
    }

    const onDeleteOTItem = (index) => {
        arrayRemoveByIndex(operationOTItems, index);
        setoperationOTItems(JSON.parse(JSON.stringify(operationOTItems)));
    }

    //Anaesthetists

    const onUpdateASItem = (index, key, value) => {
        operationASItems[index][key] = value;
        setoperationASItems(JSON.parse(JSON.stringify(operationASItems)));
    }

    const onDeleteASItem = (index) => {
        arrayRemoveByIndex(operationASItems, index);
        setoperationASItems(JSON.parse(JSON.stringify(operationASItems)));
    }
    //addnewbyakh


    const onDeleteDoctor = (index) => {
        arrayRemoveByIndex(operationDoctors, index);
        setotDoctors(JSON.parse(JSON.stringify(operationDoctors)));
    }
    const onDeleteAnaesthetist = (index) => {
        arrayRemoveByIndex(operationAnaesthetist, index);
        setotAnaesthetists(JSON.parse(JSON.stringify(operationAnaesthetist)));
    }
    const onDeleteStaff = (index) => {
        arrayRemoveByIndex(operationStaffs, index);
        setotStaffs(JSON.parse(JSON.stringify(operationStaffs)));
    }
    const onUpdateDoctor = (index, key, value) => {
        operationDoctors[index][key] = value;
        setotDoctors(JSON.parse(JSON.stringify(operationDoctors)));
    }
    const onUpdateAnaesthetist = (index, key, value) => {
        operationAnaesthetist[index][key] = value;
        setotAnaesthetists(JSON.parse(JSON.stringify(operationAnaesthetist)));
    }
    const onUpdateStaff = (index, key, value,staffEnum) => {
        operationStaffs[index][key] = value;
        operationStaffs[index]["otStaffTypeEnum"] = staffEnum;
        setotStaffs(JSON.parse(JSON.stringify(operationStaffs)));
    }
    const onUpdateStaffType = (index, key, value) => {
        operationStaffs[index][key] = value;
        setotStaffs(JSON.parse(JSON.stringify(operationStaffs)));
    }

    const onAddService = () => {
        var orderService = {
            serviceId: '',
            referrerId: '',
            qty: 0,
            feeType: 0,
            fee: 0,
            referralFee: 0,
            unitPrice: 0,
            isFOC: false
        };
        operationServices.push(orderService);
        setoperationServices(JSON.parse(JSON.stringify(operationServices)));
    }

    const onUpdateService = (index, key, value) => {
        console.log(index, key, value)
        operationServices[index][key] = value;
        setoperationServices(JSON.parse(JSON.stringify(operationServices)));
    }

    const onDeleteService = (index) => {
        arrayRemoveByIndex(operationServices, index);
        setoperationServices(JSON.parse(JSON.stringify(operationServices)));
    }

    //new add by akh

    const onAddInstrument = () => {
        var orderInstrument = {
            instrumentId: '',
            referrerId: '',
            qty: 0,
            fee: 0,
            referralFee: 0,
            unitPrice: 0,
            isFOC: false
        };
        operationInstruments.push(orderInstrument);
        setoperationInstruments(JSON.parse(JSON.stringify(operationInstruments)));
    }

    const onUpdateInstrument = (index, key, value) => {
        console.log(index, key, value)
        operationInstruments[index][key] = value;
        setoperationInstruments(JSON.parse(JSON.stringify(operationInstruments)));
    }

    const onDeleteInstrument = (index) => {
        arrayRemoveByIndex(operationInstruments, index);
        setoperationInstruments(JSON.parse(JSON.stringify(operationInstruments)));
    }



    //new add by akh

    return (
        <div className="col-md-12 form-group">
            {
                error && <span className="text-danger mb-5">{error}</span>
            }
            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Ward Item</th>
                        <th width="20%">Unit</th>
                        <th width="15%" className="money">Unit Price</th>
                        <th width="10%">Qty</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationItems, "sortOrder").map((orderItem, index) => {
                            return (
                                <OrderItemRow key={index}
                                    orderItem={orderItem}
                                    index={index}
                                    items={items}
                                    onUpdateItem={onUpdateItem}
                                    onAddItem={onAddItem}
                                    onDeleteItem={onDeleteItem}
                                    isLastItem={index == (operationItems.length - 1)}
                                />
                            )
                        })
                       
                    }
                </tbody>
            </table>

            {/*addnewbyakh*/}

            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">OT Item</th>
                        <th width="20%">Unit</th>
                        <th width="15%" className="money">Unit Price</th>
                        <th width="10%">Qty</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationOTItems, "sortOrder").map((orderOTItem, index) => {
                            return (
                                <OrderOTItemRow key={index}
                                    orderOTItem={orderOTItem}
                                    index={index}
                                    otitems={otitems}
                                    onUpdateOTItem={onUpdateOTItem}
                                    onAddOTItem={onAddOTItem}
                                    onDeleteOTItem={onDeleteOTItem}
                                    isLastOTItem={index == (operationOTItems.length - 1)}
                                    operationItemLength={operationItemLength}
                                    operationOTItemLength={operationOTItemLength}
                                />                             
                            )
                        })
                    }

                </tbody>
            </table>

            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Anaesthetist Item</th>
                        <th width="20%">Unit</th>
                        <th width="15%" className="money">Unit Price</th>
                        <th width="10%">Qty</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationASItems, "sortOrder").map((orderASItem, index) => {
                            return (
                                <OrderASItemRow key={index}
                                    orderASItem={orderASItem}
                                    index={index}
                                    asitems={asitems}
                                    onUpdateASItem={onUpdateASItem}
                                    onAddASItem={onAddASItem}
                                    onDeleteASItem={onDeleteASItem}
                                    isLastASItem={index == (operationASItems.length - 1)}
                                    operationItemLength={operationItemLength}
                                    operationOTItemLength={operationOTItemLength}
                                />
                             )
                        })                     
                    }
                </tbody>
            </table>

            {/*addnewbyakh*/}


            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Service</th>
                        <th width="20%"></th>
                        <th width="15%" className="money">Unit Price</th>
                        <th width="10%">Qty</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationServices, "sortOrder").map((orderService, index) => {
                            return (
                                <OrderServiceRow key={index}
                                    orderService={orderService}
                                    //referrers={referrers}
                                    index={index}
                                    services={services}
                                    onUpdateService={onUpdateService}
                                    onAddService={onAddService}
                                    onDeleteService={onDeleteService}
                                    isLastService={index == (operationServices.length - 1)}
                                />
                            )
                        })
                    }
                </tbody>
                
            </table>

            {/*added by akh*/}

            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Special Instrument</th>
                        <th width="20%"></th>
                        <th width="15%" className="money">Unit Price</th>
                        <th width="10%">Qty</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationInstruments, "sortOrder").map((orderInstrument, index) => {
                            return (
                                <OrderInstrumentRow key={index}
                                    orderInstrument={orderInstrument}
                                    index={index}
                                    instruments={instruments}
                                    onUpdateInstrument={onUpdateInstrument}
                                    onAddInstrument={onAddInstrument}
                                    onDeleteInstrument={onDeleteInstrument}
                                    isLastInstrument={index == (operationInstruments.length - 1)}
                                />

                                )
                        })
                    }
                </tbody>

            </table>

            {/*added by akh*/}

            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Assistant Doctor For Chief Surgeon</th>
                        <th width="20%"></th>
                        <th width="15%" ></th>
                        <th width="10%" >Fee</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationDoctors, "sortOrder").map((otDoctor, index) => {
                            return (
                                <OTDoctorRow key={index}
                                    otDoctor={otDoctor}
                                    //referrers={referrers}
                                    index={index}
                                    doctors={doctors}
                                    onUpdateDoctor={onUpdateDoctor}
                                    onAddDoctor={onAddDoctor}
                                    onDeleteDoctor={onDeleteDoctor}
                                    isLastDoctor={index == (operationDoctors.length - 1)}
                                    otDoctorTypeEnum={doctorTypeEnums}
                                />
                            )
                        })
                    }
                </tbody>
              
            </table>

            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Assistant Doctor For Aneasthetist</th>
                        <th width="20%"></th>
                        <th width="15%" ></th>
                        <th width="10%" >Fee</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationAnaesthetist, "sortOrder").map((otAnaesthetist, index) => {
                            return (
                                <OTAnaesthetistRow key={index}
                                    otAnaesthetist={otAnaesthetist}
                                    //referrers={referrers}
                                    index={index}
                                    doctors={doctors}
                                    onUpdateAnaesthetist={onUpdateAnaesthetist}
                                    onAddAnaesthetist={onAddAnaesthetist}
                                    onDeleteAnaesthetist={onDeleteAnaesthetist}
                                    isLastDoctor={index == (operationAnaesthetist.length - 1)}
                                    otDoctorTypeEnum={doctorTypeEnums}
                                />
                            )
                        })
                    }
                </tbody>

            </table>

            <table className="display table table-hover table-condensed">
                <thead>
                    <tr>
                        <th>No.</th>
                        <th width="30%">Staff</th>
                        <th width="20%"></th>
                        <th width="15%" ></th>
                        <th width="10%" >Fee</th>
                        <th width="15%" className="money">Amount</th>
                        <th width="10%" style={{ textAlign: 'center' }}></th>
                    </tr>
                </thead>
                <tbody>
                    {
                        sort(operationStaffs, "sortOrder").map((otStaff, index) => {
                            return (
                                <OTStaffRow key={index}
                                    otStaff={otStaff}
                                    //referrers={referrers}
                                    index={index}
                                    staffs={staffs}
                                    onUpdateStaff={onUpdateStaff}
                                    onAddStaff={onAddStaff}
                                    onDeleteStaff={onDeleteStaff}
                                    isLastStaff={index == (operationStaffs.length - 1)}
                                    otStaffTypeEnum={staffTypeEnums}
                                />
                            )
                        })
                    }
                </tbody>
                <tfoot>
                    <tr><td colSpan={7}></td></tr>
                    <tr>
                        <td colSpan={5} className="money">Sub Total</td>
                        <td className="money">{total.formatMoney()}</td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colSpan={5} className="money pt-td">Tax</td>
                        <td><input type="number" name={`Tax`} className="form-control money-input" onChange={(e) => setTax(Number(e.target.value))} value={tax} /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colSpan={5} className="money pt-td">Discount</td>
                        <td><input type="number" name={`Discount`} className="form-control money-input" onChange={(e) => setDiscount(Number(e.target.value))} value={discount} /></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colSpan={5} className="money">Grand Total</td>
                        <td className="money">{(Number(total) + Number(tax) - Number(discount)).formatMoney()}</td>
                        <td></td>
                    </tr>
                </tfoot>
            </table>
        </div>
    );
}

const OrderItemRow = (props) => {
    const { items, index, orderItem, onUpdateItem, onAddItem, onDeleteItem, isLastItem } = props;
    const [packingUnits, setPackingUnits] = useState([]);

    useEffect(() => {
        var selectedItem = items.find(x => x.id == orderItem.itemId);
        if (selectedItem) {
            setPackingUnits(JSON.parse(JSON.stringify(selectedItem.packingUnits)));
            if (isLastItem) {
                onAddItem();
            }
        } else {
            setPackingUnits(JSON.parse(JSON.stringify([])));
        }
    }, [items])

    useDidUpdateEffect(() => {
        var selectedItem = items.find(x => x.id == orderItem.itemId);
        if (selectedItem) {
            setPackingUnits(JSON.parse(JSON.stringify(selectedItem.packingUnits)));
            if (orderItem.qty == 0) {
                onUpdateItem(index, 'qty', 1);
            }
            if (isLastItem) {
                onAddItem();
            }
        } else {
            setPackingUnits(JSON.parse(JSON.stringify([])));
            onUpdateItem(index, 'qty', 0);
        }
    }, [orderItem.itemId]);

    useDidUpdateEffect(() => {
        var selectedUnit = packingUnits.find(x => x.unitId == orderItem.unitId);
        onUpdateItem(index, 'unitPrice', selectedUnit ? selectedUnit.saleAmount : 0);
    }, [orderItem.unitId]);

    useEffect(() => {
        if (orderItem.unitId) {
            var _qtyInSmallestUnit = calculateQtyInSmallestUnit(packingUnits, orderItem.unitId, orderItem.qty);
            onUpdateItem(index, 'qtyInSmallestUnit', _qtyInSmallestUnit);
        }
    }, [orderItem.qty, orderItem.unitId]);
    
    useDidUpdateEffect(() => {
        if (!orderItem.unitId) {
            var smallestUnit = (packingUnits && packingUnits.length > 0) ? packingUnits[packingUnits.length - 1] : null;
            onUpdateItem(index, 'unitId', smallestUnit ? smallestUnit.unitId : '');
        }
    }, [packingUnits]);

    var name = orderItem && orderItem.itemId && orderItem.itemId > 0 ? `${_itemType}[${index}].` : '';

    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}ItemId`} value={orderItem.itemId} hidden />
                <Select
                    value={isLastItem ? null : items.map(x => ({ value: x.id, label: `${x.name} (${x.chemicalName})` })).find(x => x.value == orderItem.itemId)}
                    onChange={(e) => onUpdateItem(index, 'itemId', e.value)}
                    options={items.map(x => ({ value: x.id, label: `${x.name} (${x.chemicalName})` }))}
                />
            </td>
            <td>
                <input name={`${name}UnitId`} value={orderItem.unitId} hidden />
                <Select
                    value={packingUnits.map(x => ({ value: x.unitId, label: x.unitName })).find(x => x.value == orderItem.unitId)}
                    onChange={(e) => onUpdateItem(index, 'unitId', e.value)}
                    options={packingUnits.map(x => ({ value: x.unitId, label: x.unitName }))}
                />
            </td>
            <td className="money pt-td">
                <input name={`${name}IsFOC`} value={orderItem.isFOC} hidden />
                <input name={`${name}UnitPrice`} value={orderItem.unitPrice} hidden />
                {orderItem.isFOC ? 'FOC' : orderItem.unitPrice.formatMoney()}
            </td>
            <td>
                <input name={`${name}QtyInSmallestUnit`} value={orderItem.qtyInSmallestUnit} hidden />
                <input type="number" name={`${name}Qty`} className="form-control" onChange={(e) => onUpdateItem(index, 'qty', e.target.value)} value={orderItem.qty} />
            </td>
            <td className="money pt-td">{((orderItem.qty || 0) * (orderItem.unitPrice || 0)).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastItem && <button type="button" onClick={() => onDeleteItem(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>
        </tr>
    )
}

/*addnewbyakh*/
const OrderOTItemRow = (props) => {

    const { otitems, index, orderOTItem, onUpdateOTItem, onAddOTItem, onDeleteOTItem, isLastOTItem, operationItemLength, operationOTItemLength } = props;
    const [packingUnits, setPackingUnits] = useState([]);
    useEffect(() => {
        var selectedOTItem = otitems.find(x => x.id == orderOTItem.itemId);
        if (selectedOTItem) {
            setPackingUnits(JSON.parse(JSON.stringify(selectedOTItem.packingUnits)));
            if (isLastOTItem) {
                onAddOTItem();
            }
        } else {
            setPackingUnits(JSON.parse(JSON.stringify([])));
        }
    }, [otitems])

    useDidUpdateEffect(() => {
        var selectedOTItem = otitems.find(x => x.id == orderOTItem.itemId);
        if (selectedOTItem) {
            setPackingUnits(JSON.parse(JSON.stringify(selectedOTItem.packingUnits)));
            if (orderOTItem.qty == 0) {
                onUpdateOTItem(index, 'qty', 1);
            }
            if (isLastOTItem) {
                onAddOTItem();
            }
        } else {
            setPackingUnits(JSON.parse(JSON.stringify([])));
            onUpdateOTItem(index, 'qty', 0);
        }
    }, [orderOTItem.itemId]);

    useDidUpdateEffect(() => {
        var selectedUnit = packingUnits.find(x => x.unitId == orderOTItem.unitId);
        onUpdateOTItem(index, 'unitPrice', selectedUnit ? selectedUnit.saleAmount : 0);
    }, [orderOTItem.unitId]);

    useEffect(() => {
        if (orderOTItem.unitId) {
            var _qtyInSmallestUnit = calculateQtyInSmallestUnit(packingUnits, orderOTItem.unitId, orderOTItem.qty);
            onUpdateOTItem(index, 'qtyInSmallestUnit', _qtyInSmallestUnit);
        }
    }, [orderOTItem.qty, orderOTItem.unitId]);

    useDidUpdateEffect(() => {
        if (!orderOTItem.unitId) {
            var smallestUnit = (packingUnits && packingUnits.length > 0) ? packingUnits[packingUnits.length - 1] : null;
            onUpdateOTItem(index, 'unitId', smallestUnit ? smallestUnit.unitId : '');
        }
    }, [packingUnits]);
    //changeIndexPMK
    var name = orderOTItem && orderOTItem.itemId && orderOTItem.itemId > 0 ? `${_otitemType}[${(operationItemLength - 1) + index}].` : '';

    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}ItemId`} value={orderOTItem.itemId} hidden />
                <Select
                    value={isLastOTItem ? null : otitems.map(x => ({ value: x.id, label: `${x.name} (${x.chemicalName})` })).find(x => x.value == orderOTItem.itemId)}
                    onChange={(e) => onUpdateOTItem(index, 'itemId', e.value)}
                    options={otitems.map(x => ({ value: x.id, label: `${x.name} (${x.chemicalName})` }))}
                />
            </td>
            <td>
                <input name={`${name}UnitId`} value={orderOTItem.unitId} hidden />
                <Select
                    value={packingUnits.map(x => ({ value: x.unitId, label: x.unitName })).find(x => x.value == orderOTItem.unitId)}
                    onChange={(e) => onUpdateOTItem(index, 'unitId', e.value)}
                    options={packingUnits.map(x => ({ value: x.unitId, label: x.unitName }))}
                />
            </td>
            <td className="money pt-td">
                <input name={`${name}IsFOC`} value={orderOTItem.isFOC} hidden />
                <input name={`${name}UnitPrice`} value={orderOTItem.unitPrice} hidden />
                {orderOTItem.isFOC ? 'FOC' : orderOTItem.unitPrice.formatMoney()}
            </td>
            <td>
                <input name={`${name}QtyInSmallestUnit`} value={orderOTItem.qtyInSmallestUnit} hidden />
                <input type="number" name={`${name}Qty`} className="form-control" onChange={(e) => onUpdateOTItem(index, 'qty', e.target.value)} value={orderOTItem.qty} />
            </td>
            <td className="money pt-td">{((orderOTItem.qty || 0) * (orderOTItem.unitPrice || 0)).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastOTItem && <button type="button" onClick={() => onDeleteOTItem(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>
        </tr>
        )
}


const OrderASItemRow = (props) => {
    const { asitems, index, orderASItem, onUpdateASItem, onAddASItem, onDeleteASItem, isLastASItem, operationItemLength, operationOTItemLength } = props;
    const [packingUnits, setPackingUnits] = useState([]);
    useEffect(() => {
        var selectedASItem = asitems.find(x => x.id == orderASItem.itemId);
        if (selectedASItem) {
            setPackingUnits(JSON.parse(JSON.stringify(selectedASItem.packingUnits)));
            if (isLastASItem) {
                onAddASItem();
            }
        } else {
            setPackingUnits(JSON.parse(JSON.stringify([])));
        }
    }, [asitems])

    useDidUpdateEffect(() => {
        var selectedASItem = asitems.find(x => x.id == orderASItem.itemId);
        if (selectedASItem) {
            setPackingUnits(JSON.parse(JSON.stringify(selectedASItem.packingUnits)));
            if (orderASItem.qty == 0) {
                onUpdateASItem(index, 'qty', 1);
            }
            if (isLastASItem) {
                onAddASItem();
            }
        } else {
            setPackingUnits(JSON.parse(JSON.stringify([])));
            onUpdateASItem(index, 'qty', 0);
        }
    }, [orderASItem.itemId]);

    useDidUpdateEffect(() => {
        var selectedUnit = packingUnits.find(x => x.unitId == orderASItem.unitId);
        onUpdateASItem(index, 'unitPrice', selectedUnit ? selectedUnit.saleAmount : 0);
    }, [orderASItem.unitId]);

    useEffect(() => {
        if (orderASItem.unitId) {
            var _qtyInSmallestUnit = calculateQtyInSmallestUnit(packingUnits, orderASItem.unitId, orderASItem.qty);
            onUpdateASItem(index, 'qtyInSmallestUnit', _qtyInSmallestUnit);
        }
    }, [orderASItem.qty, orderASItem.unitId]);

    useDidUpdateEffect(() => {
        if (!orderASItem.unitId) {
            var smallestUnit = (packingUnits && packingUnits.length > 0) ? packingUnits[packingUnits.length - 1] : null;
            onUpdateASItem(index, 'unitId', smallestUnit ? smallestUnit.unitId : '');
        }
    }, [packingUnits]);

    var name = orderASItem && orderASItem.itemId && orderASItem.itemId > 0 ? `${_asitemType}[${(operationItemLength - 1) + (operationOTItemLength - 1) + index}].` : '';

    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}ItemId`} value={orderASItem.itemId} hidden />
                <Select
                    value={isLastASItem ? null : asitems.map(x => ({ value: x.id, label: `${x.name} (${x.chemicalName})` })).find(x => x.value == orderASItem.itemId)}
                    onChange={(e) => onUpdateASItem(index, 'itemId', e.value)}
                    options={asitems.map(x => ({ value: x.id, label: `${x.name} (${x.chemicalName})` }))}
                />
            </td>
            <td>
                <input name={`${name}UnitId`} value={orderASItem.unitId} hidden />
                <Select
                    value={packingUnits.map(x => ({ value: x.unitId, label: x.unitName })).find(x => x.value == orderASItem.unitId)}
                    onChange={(e) => onUpdateASItem(index, 'unitId', e.value)}
                    options={packingUnits.map(x => ({ value: x.unitId, label: x.unitName }))}
                />
            </td>
            <td className="money pt-td">
                <input name={`${name}IsFOC`} value={orderASItem.isFOC} hidden />
                <input name={`${name}UnitPrice`} value={orderASItem.unitPrice} hidden />
                {orderASItem.isFOC ? 'FOC' : orderASItem.unitPrice.formatMoney()}
            </td>
            <td>
                <input name={`${name}QtyInSmallestUnit`} value={orderASItem.qtyInSmallestUnit} hidden />
                <input type="number" name={`${name}Qty`} className="form-control" onChange={(e) => onUpdateASItem(index, 'qty', e.target.value)} value={orderASItem.qty} />
            </td>
            <td className="money pt-td">{((orderASItem.qty || 0) * (orderASItem.unitPrice || 0)).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastASItem && <button type="button" onClick={() => onDeleteASItem(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>
        </tr>
        )

}


/*addnewbyakh*/


const OrderServiceRow = (props) => {
    //debugger;
    const { services, referrers, index, orderService, onUpdateService, onAddService, onDeleteService, isLastService } = props;
    const [packingUnits, setPackingUnits] = useState([]);

    useDidUpdateEffect(() => {
        console.log("OrderService", orderService.serviceId);
        var selectedService = services.find(x => x.id == orderService.serviceId);
        if (selectedService) {
            onUpdateService(index, 'unitPrice', selectedService.serviceFee);
            if (orderService.qty == 0) {
                onUpdateService(index, 'qty', 1);
            }
            if (isLastService) {
                onAddService();
            }
        } else {
            onUpdateService(index, 'qty', 0);
        }
    }, [orderService.serviceId])

    var name = orderService && orderService.serviceId && orderService.serviceId > 0 ? `${_serviceType}[${index}].` : '';
    console.log("Services=", (orderService.qty || 0) * (orderService.unitPrice || 0));
    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}ServiceId`} value={orderService.serviceId} hidden />
                <Select
                    value={isLastService ? null : services.map(x => ({ value: x.id, label: `${x.name} (${x.code})` })).find(x => x.value == orderService.serviceId)}
                    onChange={(e) => onUpdateService(index, 'serviceId', e.value)}
                    options={services.map(x => ({ value: x.id, label: `${x.name} (${x.code})` }))}
                />
            </td>
            <td>
                {/*<input name={`${name}ReferrerId`} value={orderService.referrerId} hidden />*/}
                {/*<Select*/}
                {/*    isClearable={true}*/}
                {/*    value={referrers.map(x => ({ value: x.id, label: `${x.name}` })).find(x => x.value == orderService.referrerId)}*/}
                {/*    onChange={(e) => onUpdateService(index, 'referrerId', e ? e.value : '')}*/}
                {/*    options={referrers.map(x => ({ value: x.id, label: `${x.name}` }))}*/}
                {/*/>*/}
            </td>
            <td className="money pt-td">
                <input name={`${name}IsFOC`} value={orderService.isFOC} hidden />
                <input name={`${name}UnitPrice`} value={orderService.unitPrice} hidden />
                {orderService.isFOC ? 'FOC' : orderService.unitPrice.formatMoney()}
            </td>
            <td>
                <input type="number" name={`${name}Qty`} className="form-control" onChange={(e) => onUpdateService(index, 'qty', e.target.value)} value={orderService.qty} />
            </td>
            <td className="money pt-td">{((orderService.qty || 0) * (orderService.unitPrice || 0)).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastService && <button type="button" onClick={() => onDeleteService(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>
        </tr>
    )
}


 /*Added by akh*/

const OrderInstrumentRow = (props) => {
    //debugger;
    const { instruments, index, orderInstrument, onUpdateInstrument, onAddInstrument, onDeleteInstrument, isLastInstrument } = props;
    const [packingUnits, setPackingUnits] = useState([]);

    useDidUpdateEffect(() => {
        console.log("OrderInstrument", orderInstrument.instrumentId);
        var selectedInstrument = instruments.find(x => x.id == orderInstrument.instrumentId);
        if (selectedInstrument) {
            onUpdateInstrument(index, 'unitPrice', selectedInstrument.fee);
            if (orderInstrument.qty == 0) {
                onUpdateInstrument(index, 'qty', 1);
            }
            if (isLastInstrument) {
                onAddInstrument();
            }
        } else {
            onUpdateInstrument(index, 'qty', 0);
        }
    }, [orderInstrument.instrumentId])

    var name = orderInstrument && orderInstrument.instrumentId && orderInstrument.instrumentId > 0 ? `${_instrumentType}[${index}].` : '';
    console.log("Instruments=", (orderInstrument.qty || 0) * (orderInstrument.unitPrice || 0));

    return (
        
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}InstrumentId`} value={orderInstrument.instrumentId} hidden />
                <Select
                    value={isLastInstrument ? null : instruments.map(x => ({ value: x.id, label: `${x.name}` })).find(x => x.value == orderInstrument.instrumentId)}
                    onChange={(e) => onUpdateInstrument(index, 'instrumentId', e.value)}
                    options={instruments.map(x => ({ value: x.id, label: `${x.name}` }))}
                />
            </td>
            <td>

            </td>
            <td className="money pt-td">
                <input name={`${name}IsFOC`} value={orderInstrument.isFOC} hidden />
                <input name={`${name}UnitPrice`} value={orderInstrument.unitPrice} hidden />
                {orderInstrument.isFOC ? 'FOC' : orderInstrument.unitPrice.formatMoney()}
            </td>
            <td>
                <input type="number" name={`${name}Qty`} className="form-control" onChange={(e) => onUpdateInstrument(index, 'qty', e.target.value)} value={orderInstrument.qty} />
            </td>
            <td className="money pt-td">{((orderInstrument.qty || 0) * (orderInstrument.unitPrice || 0)).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastInstrument && <button type="button" onClick={() => onDeleteInstrument(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>


        </tr>
        
        
        )

}

/*Added by akh*/


const OTDoctorRow = (props) => {
    //debugger;
    const { doctors, index, otDoctor, onUpdateDoctor, onAddDoctor, onDeleteDoctor, isLastDoctor,otDoctorTypeEnum } = props;
    const [packingUnits, setPackingUnits] = useState([]);

    console.log("otDoctorTypeEnum", otDoctorTypeEnum);
    useDidUpdateEffect(() => {
        var selectedItem = doctors.find(x => x.id == otDoctor.doctorId);
        if (selectedItem) {
           // setPackingUnits(JSON.parse(JSON.stringify(selectedItem.packingUnits)));
            //if (orderItem.qty == 0) {
            //    onUpdateItem(index, 'qty', 1);
            //}
            if (isLastDoctor) {
                onAddDoctor();
            }
        } else {
            //setPackingUnits(JSON.parse(JSON.stringify([])));
            onUpdateDoctor(index, 'fee', 0);
        }
    }, [otDoctor.doctorId]);

    var name = otDoctor && otDoctor.doctorId && otDoctor.doctorId > 0 ? `${_otDoctorType}[${index}].` : '';

  
    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}DoctorId`} value={otDoctor.doctorId} hidden />
                <Select
                    value={isLastDoctor ? null : doctors.map(x => ({ value: x.id, label: `${x.name} (${x.code})` })).find(x => x.value == otDoctor.doctorId)}
                    onChange={(e) => onUpdateDoctor(index, 'doctorId', e.value)}
                    options={doctors.map(x => ({ value: x.id, label: `${x.name} (${x.code})` }))}
                />
            </td>
            <td>
                <input name={`${name}OTDoctorTypeEnum`} value={otDoctor.otDoctorTypeEnum} hidden />
                <Select
                    isClearable={true}
                    value={otDoctorTypeEnum.map(x => ({ value: x.value, label: `${x.name}` })).find(x => x.value == otDoctor.otDoctorTypeEnum)}
                    onChange={(e) => onUpdateDoctor(index, 'otDoctorTypeEnum', e ? e.value : '')}
                    options={otDoctorTypeEnum.map(x => ({ value: x.value, label: `${x.name}` }))}
                />
                
            </td>
            <td >
                
            </td>
            <td>
                <input type="number" name={`${name}Fee`} className="form-control" onChange={(e) => onUpdateDoctor(index, 'fee', e.target.value)} value={otDoctor.fee} />
            </td>
            <td className="money pt-td">{Number(otDoctor.fee).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastDoctor && <button type="button" onClick={() => onDeleteDoctor(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>
        </tr>
    )
}

const OTAnaesthetistRow = (props) => {
    //debugger;
    const { doctors, index, otAnaesthetist, onUpdateAnaesthetist, onAddAnaesthetist, onDeleteAnaesthetist, isLastDoctor, otDoctorTypeEnum } = props;
    const [packingUnits, setPackingUnits] = useState([]);

    console.log("otDoctorTypeEnum", otDoctorTypeEnum);
    useDidUpdateEffect(() => {
        var selectedItem = doctors.find(x => x.id == otAnaesthetist.doctorId);
        if (selectedItem) {
            // setPackingUnits(JSON.parse(JSON.stringify(selectedItem.packingUnits)));
            //if (orderItem.qty == 0) {
            //    onUpdateItem(index, 'qty', 1);
            //}
            if (isLastDoctor) {
                onAddAnaesthetist();
            }
        } else {
            //setPackingUnits(JSON.parse(JSON.stringify([])));
            onUpdateAnaesthetist(index, 'fee', 0);
        }
    }, [otAnaesthetist.doctorId]);

    var name = otAnaesthetist && otAnaesthetist.doctorId && otAnaesthetist.doctorId > 0 ? `${_otAnaesthetistType}[${index}].` : '';


    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}DoctorId`} value={otAnaesthetist.doctorId} hidden />
                <Select
                    value={isLastDoctor ? null : doctors.map(x => ({ value: x.id, label: `${x.name} (${x.code})` })).find(x => x.value == otAnaesthetist.doctorId)}
                    onChange={(e) => onUpdateAnaesthetist(index, 'doctorId', e.value)}
                    options={doctors.map(x => ({ value: x.id, label: `${x.name} (${x.code})` }))}
                />
            </td>
            <td>
                <input name={`${name}OTDoctorTypeEnum`} value={otAnaesthetist.otDoctorTypeEnum} hidden />
                <Select
                    isClearable={true}
                    value={otDoctorTypeEnum.map(x => ({ value: x.value, label: `${x.name}` })).find(x => x.value == otAnaesthetist.otDoctorTypeEnum)}
                    onChange={(e) => onUpdateAnaesthetist(index, 'otDoctorTypeEnum', e ? e.value : '')}
                    options={otDoctorTypeEnum.map(x => ({ value: x.value, label: `${x.name}` }))}
                />

            </td>
            <td >

            </td>
            <td>
                <input type="number" name={`${name}Fee`} className="form-control" onChange={(e) => onUpdateAnaesthetist(index, 'fee', e.target.value)} value={otAnaesthetist.fee} />
            </td>
            <td className="money pt-td">{Number(otAnaesthetist.fee).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastDoctor && <button type="button" onClick={() => onDeleteAnaesthetist(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>

        </tr>
    )
}
const OTStaffRow = (props) => {
    //debugger;
    const { staffs, index, otStaff, onUpdateStaff, onAddStaff, onDeleteStaff, isLastStaff, otStaffTypeEnum,fee } = props;
    const [packingUnits, setPackingUnits] = useState([]);


    useDidUpdateEffect(() => {
        //debugger;
        var selectedItem = staffs.find(x => x.id == otStaff.staffId);
        if (selectedItem) {
            //onUpdateStaff(JSON.parse(JSON.stringify(selectedItem.fee)));
            //if (orderItem.qty == 0) {
            //    onUpdateItem(index, 'qty', 1);
            //}
            if (isLastStaff) {
                onAddStaff();
            }
        } else {
            //setPackingUnits(JSON.parse(JSON.stringify([])));
            //onUpdateStaff(index, 'fee', 0,0);
        }
        
    }, [otStaff.staffId]);
   
    const handleChange = event => {
        var selectedStaffType = otStaffTypeEnum.find(x => x.name == event.label);
        console.log("Fee=", selectedStaffType.fee);
        onUpdateStaff(index, 'fee', selectedStaffType.fee, selectedStaffType.id);
    };

    var name = otStaff && otStaff.staffId && otStaff.staffId > 0 ? `${_otStaffType}[${index}].` : '';


    return (
        <tr>
            <td>
                <input name={`${name}SortOrder`} value={index + 1} hidden />
                {index + 1}
            </td>
            <td>
                <input name={`${name}StaffId`} value={otStaff.staffId} hidden />
                <Select
                    value={isLastStaff ? null : staffs.map(x => ({ value: x.id, label: `${x.name} (${x.positionName})` })).find(x => x.value == otStaff.staffId)}
                    onChange={(e) => onUpdateStaff(index, 'staffId', e.value,0)}
                    options={staffs.map(x => ({ value: x.id, label: `${x.name} (${x.positionName})` }))}
                />
            </td>
            <td>
                <input name={`${name}OTStaffTypeEnum`} value={otStaff.otStaffTypeEnum} hidden />
                <Select
                    isClearable={true}
                    value={otStaffTypeEnum.map(x => ({ value: x.id, label: `${x.name}` })).find(x => x.value == otStaff.otStaffTypeEnum)}
                    onChange={handleChange}
                    options={otStaffTypeEnum.map(x => ({ value: x.id, label: `${x.name}` }))}
                />
            
            </td>
            <td >

            </td>
            <td>
                <input type="number" name={`${name}Fee`} className="form-control" onChange={(e) => onUpdateStaff(index, 'fee', e.target.value)} value={otStaff.fee} />
            </td>
            <td className="money pt-td">{Number(otStaff.fee).formatMoney()}</td>
            <td className="pt-td">
                {
                    !isLastStaff && <button type="button" onClick={() => onDeleteStaff(index)} className="btn btn-xs btn-secondary">Delete</button>
                }
            </td>
        </tr>
    )
}


class App extends Component {
    constructor(props) {
        super(props);

        this.state = {
            selectedItemId: undefined,
            selectedUnitId: undefined,
            selectedBatchId: undefined,
        }

        this.clear = this.clear.bind(this);
    }
    componentWillUnmount() {
        this.clear();
    }

    clear = () => this.setState({ selectedItemId: undefined, selectedUnitId: undefined, selectedBatchId: undefined });
    render() {
        return (
            <OTForm {...this.state} reset={this.clear} />
        );
    }
}

const rootElement = document.getElementById('root')
ReactDOM.render(<App ref={component => window.reactComponent = component} />, rootElement)