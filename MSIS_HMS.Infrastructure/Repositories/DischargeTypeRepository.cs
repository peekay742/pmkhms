using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Threading.Tasks;
using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Interfaces;
using MSIS_HMS.Infrastructure.Repositories.Base;

namespace MSIS_HMS.Infrastructure.Repositories
{
    public class DischargeTypeRepository : Repository<DischargeType>, IDischargeTypeRepository
    {
        public DischargeTypeRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {

        }
        public override List<DischargeType> GetAll()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetDischargeTypes");
            var dischargeTypes = ds.Tables[0].ToList<DischargeType>();
            return dischargeTypes;
        }
        public override List<DischargeType> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetDischargeTypes", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var dischargeTypes = ds.Tables[0].ToList<DischargeType>();
            return dischargeTypes;
        }
        public override DischargeType Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetDischargeTypes", new Dictionary<string, object>() { { "DischargeTypeId", Id } });
            var dischargeTypes = ds.Tables[0].ToList<DischargeType>();
            return dischargeTypes.Count > 0 ? dischargeTypes[0] : null;
        }
        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var dischargeType = await _context.DischargeTypes.FindAsync(Id);
                    if (dischargeType == null)
                    {
                        return false;
                    }
                    dischargeType.IsDelete = true;
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