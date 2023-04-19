using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Repositories
{
    public interface IBookRepository : IRepository<Book>
    {
        List<Book> GetAll(int? BranchId = null, int? BookId = null, string BookNo = null, DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, int? PatientId = null, int? DoctorId = null, BookStatusEnum? Status = null);

    }
}
