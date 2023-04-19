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
    public class CollectionRepository : Repository<Collection>, ICollectionRepository
    {
        public CollectionRepository(ApplicationDbContext context, IConfigService config) : base(context, config)
        {

        }
        public override List<Collection> GetAll(int? BranchId)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollection", new Dictionary<string, object>() { { "BranchId", BranchId } });
            var collections = ds.Tables[0].ToList<Collection>();
            return collections;
        }
        public List<Collection> GetAll(int? BranchId = null, string collectionName = null, int? Id = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollection", new Dictionary<string, object>{
                { "BranchId", BranchId },
                {"Name",collectionName },
                {"Id",Id }
            });
            var collections = ds.Tables[0].ToList<Collection>();
            return collections;
        }
        public override Collection Get(int id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetCollection", new Dictionary<string, object>{
                {"Id",id }
            });
            var collection = ds.Tables[0].ToList<Collection>();
            return collection.Count > 0 ? collection[0] : null;
        }
        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var outlet = await _context.Collections.FindAsync(Id);
                    if (outlet == null)
                    {
                        return false;
                    }
                    outlet.IsDelete = true;
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
