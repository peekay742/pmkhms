using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Repositories
{
    public interface IImagingResultRepository : IRepository<ImagingResult>
    {
        List<ImagingResult> GetAll(int? BranchId = null, int? ImgResultId = null, int? PatientId = null, string ResultNo = null, int? TechnicianId = null, int? ConsultantId = null, bool? IsCompleted = null, DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, DateTime? CompletedDate = null, DateTime? StartCompletedDate = null, DateTime? EndCompletedDate = null, int? LabTestId = null);

    }
}
