using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Helpers;
using MSIS_HMS.Infrastructure.Interfaces;
using MSIS_HMS.Infrastructure.Repositories.Base;
using MSIS_HMS.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Infrastructure.Repositories.Base
{
    public class CollectionGroupRepository : Repository<CollectionGroup>, ICollectionGroupRepository
    {
        public CollectionGroupRepository(ApplicationDbContext context, IConfigService configService) : base(context, configService)
        {
        }
        public override List<CollectionGroup> GetAll()
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollectionGroups");
            var collectionGroup = ds.Tables[0].ToList<CollectionGroup>();
            return collectionGroup;
        }
        public override List<CollectionGroup> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollectionGroups", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var collectionGroup = ds.Tables[0].ToList<CollectionGroup>();
            return (collectionGroup);
        }
        public List<CollectionGroup> GetAll(int? BranchId = null, string Name = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollectionGroups", new Dictionary<string, object>() {
                {"BranchId",BranchId },
                {"Name", Name },
            });
            var collectionGroup = ds.Tables[0].ToList<CollectionGroup>();
            return collectionGroup;
        }
        public override CollectionGroup Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollectionGroups", new Dictionary<string, object> { { "CollectionGroupId", Id } });
            var collectionGroup = ds.Tables[0].ToList<CollectionGroup>();
            return collectionGroup.Count > 0 ? collectionGroup[0] : null;
        }
        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var collectionGroup = await _context.CollectionGroups.FindAsync(Id);
                    if (collectionGroup == null)
                    {
                        return false;
                    }
                    collectionGroup.IsDelete = true;
                    await _context.SaveChangesAsync();
                    await transaction.CommitAsync();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    await transaction.RollbackAsync();

                }
            }
            return false;
        }
    }
}
