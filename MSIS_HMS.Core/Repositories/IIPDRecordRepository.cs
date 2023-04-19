using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Entities.DTOs;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;

namespace MSIS_HMS.Core.Repositories
{
    public interface IIPDRecordRepository : IRepository<IPDRecord>
    {
        List<IPDRecord> GetAll(int? BranchId = null, int? IPDRecordId = null, string Status = null, int? PaymentType = null, int? BedId = null, int? RoomId = null, string VoucherNo = null, string? BarCode = null, string? QRCode = null, int? TreatmentProcess = null, DateTime? DOA = null, DateTime? DODC = null, DateTime? StartDate = null, DateTime? EndDate = null, int? AdmissionType = null);
        List<IPDRecord> GetAllDischarge(int? BranchId = null, int? IPDRecordId = null, string Status = null, int? PaymentType = null, int? BedId = null, int? RoomId = null, string VoucherNo = null, int? TreatmentProcess = null, DateTime? DOA = null, DateTime? DODC = null, DateTime? StartDate = null, DateTime? EndDate = null, string DiseaseName = null, string DiseaseSummary = null, string PhotographicExaminationAnswer = null, string MedicalTreatment = null, int? DischargeTypeId = null);

        List<Room> GetAvailableRoomsandBeds(int floorId);
        IPDRecord GetIPDSingleRecord(int IPDRecordId);
        decimal GetIncomeForIPD(int? BranchId = null, DateTime? StartDate = null, DateTime? EndDate = null);
        List<IPDRecordForDashboardDTO> GetIPDRecordForDashboard(int? BranchId = null);
        List<LabOrder> GetLabOrderByIPDRecord(int iPDRecordId, DateTime selectedDate);
        List<ImagingOrder> GetImgOrderByIPDRecord(int iPDRecordId, DateTime selectedDate);
    }

}
