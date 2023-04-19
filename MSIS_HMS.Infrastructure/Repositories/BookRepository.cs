using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Enums;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Interfaces;
using MSIS_HMS.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Infrastructure.Repositories
{
    public class BookRepository : Repository<Book>, IBookRepository
    {
        public BookRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {

        }

        public override List<Book> GetAll()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetBooks");
            var book = ds.Tables[0].ToList<Book>();
            return book;
        }
        public override List<Book> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetBooks", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var books = ds.Tables[0].ToList<Book>();
            return books;
        }
        public List<Book> GetAll(int? BranchId = null, int? BookId = null, string BookNo = null, DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, int? PatientId = null, int? DoctorId = null, BookStatusEnum? Status = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetBooks", new Dictionary<string, object>() {
                { "BranchId", BranchId },
                { "BookId", BookId },
                { "BookNo", BookNo },
                { "Date", Date },
                { "StartDate", StartDate },
                { "EndDate", EndDate },
                { "PatientId", PatientId },
                { "DoctorId", DoctorId },
                { "Status", Status },
            });
            var books = ds.Tables[0].ToList<Book>();
            return books;
        }

        public override Book Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetBooks", new Dictionary<string, object>() { { "BookId", Id } });
            var books = ds.Tables[0].ToList<Book>();
            books.ForEach(x => x.Doctor = GetDoctor(x.DoctorId));
            return books.Count > 0 ? books[0] : null;
        }

        private Doctor GetDoctor(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetDoctors", new Dictionary<string, object>() { { "DoctorId", Id } });
            var doctors = ds.Tables[0].ToList<Doctor>();
            return doctors.Count > 0 ? doctors[0] : null;
        }

        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var book = await _context.Books.FindAsync(Id);
                    if (book == null)
                    {
                        return false;
                    }
                    book.IsDelete = true;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (DbException e)
                {
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                }
            }
            return false;
        }
    }
}
