using MSIS_HMS.Core.Entities;
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
    public class InstrumentRepository : Repository<Instrument> , IInstrumentRepository 
    {
        public InstrumentRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {
               
        }

        public override List<Instrument> GetAll()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetInstruments");
            var instruments = ds.Tables[0].ToList<Instrument>();
            return instruments;
        }
        public override List<Instrument> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetInstruments", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var instruments = ds.Tables[0].ToList<Instrument>();
            return instruments;
        }

        public override Instrument Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetInstruments", new Dictionary<string, object>() { { "InstrumentId", Id } });
            var instruments = ds.Tables[0].ToList<Instrument>();
            return instruments.Count > 0 ? instruments[0] : null;
        }

        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var instruments = await _context.Instruments.FindAsync(Id);
                    if (instruments == null)
                    {
                        return false;
                    }
                    instruments.IsDelete = true;
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
