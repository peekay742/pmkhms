
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Entities.DTOs;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Repositories
{
    public interface IReferrerRepository:IRepository<Referrer>
    {
        List<Referrer> GetAll();
        List<ReferrerReportDTO> GetReferrerReport(int? Id, DateTime? startDate, DateTime? endDate);
        //List <Referrer> GetEachReferrerReport(int? Id);
        List<Referrer> GetEachReferrerReport(int referrerId);
    }
}
