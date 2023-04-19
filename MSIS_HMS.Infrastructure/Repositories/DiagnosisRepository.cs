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
    public class DiagnosisRepository: Repository<Diagnosis>, IDiagnosisRepository
    {
        public DiagnosisRepository(ApplicationDbContext context, IConfigService config) : base(context, config)
        {

        }
        public List<Diagnosis> GetAll(string diagnosisName = null,int? Id=null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetDiagnosis", new Dictionary<string, object>{
                {"Name",diagnosisName },
                {"Id",Id }
            });
            var symptoms = ds.Tables[0].ToList<Diagnosis>();
            return symptoms;
        }
        public override Diagnosis Get(int id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetDiagnosis", new Dictionary<string, object>{
                {"Id",id }
            });
            var symptom = ds.Tables[0].ToList<Diagnosis>();
            return symptom.Count > 0 ? symptom[0] : null;
        }
        public override async Task<bool> DeleteAsync(int Id)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var outlet = await _context.Diagnosis.FindAsync(Id);
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
