using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Infrastructure.Repositories.Base
{
    public class LabProfileRepository : Repository<LabProfile>, ILabProfileRepository
    {
        public LabProfileRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {
        }
         
        public override List<LabProfile> GetAll()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetLabProfiles");
            var labProfiles = ds.Tables[0].ToList<LabProfile>();
            return labProfiles;
        }

        public override List<LabProfile> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetLabProfiles", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var labProfiles = ds.Tables[0].ToList<LabProfile>();
            return labProfiles;
        }

        public List<LabProfile> GetAll(int? BranchId = null, string Name = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetLabProfiles", new Dictionary<string, object>() {
                { "BranchId", BranchId },
                { "Name", Name },       
            });
            var labProfiles = ds.Tables[0].ToList<LabProfile>();
            return labProfiles;
        }

        public override LabProfile Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetLabProfiles", new Dictionary<string, object>() { { "LabProfileId", Id } });
            var labProfiles = ds.Tables[0].ToList<LabProfile>();
            return labProfiles.Count > 0 ? labProfiles[0] : null;
        }

        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var labProfiles = await _context.LabProfiles.FindAsync(Id);
                    if(labProfiles == null)
                    {
                        return false;
                    }
                    labProfiles.IsDelete= true;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();

                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                    await transaction.RollbackAsync();
                }
            }
            return false;
        }

    }
}
