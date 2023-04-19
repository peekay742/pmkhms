using Microsoft.EntityFrameworkCore;
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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIS_HMS.Infrastructure.Repositories
{
   public class ImagingResultRepository: Repository<ImagingResult>, IImagingResultRepository
    {
        public ImagingResultRepository(ApplicationDbContext context, IConfigService config) : base(context, config)
        {

        }
        public List<ImagingResult> GetAll(int? BranchId = null, int? ImgResultId = null, int? PatientId = null, string ResultNo = null, int? TechnicianId = null, int? ConsultantId = null, bool? IsCompleted = null, DateTime? Date = null, DateTime? StartDate = null, DateTime? EndDate = null, DateTime? CompletedDate = null, DateTime? StartCompletedDate = null, DateTime? EndCompletedDate = null, int? LabTestId = null)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgResults", new Dictionary<string, object>() {
                { "BranchId", BranchId },
                { "ImgResultId", ImgResultId },
                { "PatientId", PatientId },
                { "ResultNo", ResultNo },
                { "IsCompleted", IsCompleted },
                { "Date", Date },
                { "StartDate", StartDate },
                { "EndDate", EndDate },
                { "CompletedDate", CompletedDate },
                { "StartCompletedDate", StartCompletedDate },
                { "EndCompletedDate", EndCompletedDate },
                { "LabTestId", LabTestId },
            });
            var imgResults = ds.Tables[0].ToList<ImagingResult>();
            foreach(var img in imgResults)
            {
                img.ImagingResultDetails = GetLabResultDetails(img.Id);
            }
            return imgResults;
        }
        public override ImagingResult Get(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgResults", new Dictionary<string, object>() { { "ImgResultId", Id } });
            var imgResults = ds.Tables[0].ToList<ImagingResult>();
            var imgResult = imgResults.Count > 0 ? imgResults[0] : null;
            if (imgResult != null)
            {
                imgResult.ImagingResultDetails = GetLabResultDetails(Id);
                //labResult.LabResultFiles = GetLabResultFiles(Id);
            }

            return imgResult;
        }
        public List<ImagingResultDetail> GetLabResultDetails(int Id)
        {
            DataSet ds = EfCoreExtensions.Execute_SP(_connectionString, "SP_GetImgResultDetails", new Dictionary<string, object>() { { "ImgResultId", Id } });
            var imgResultTests = ds.Tables[0].ToList<ImagingResultDetail>();
            return imgResultTests;
        }

        public override async Task<ImagingResult> UpdateAsync(ImagingResult imgResult)
        {
            if (imgResult != null)
            {
                if (imgResult.ImagingResultDetails != null)
                {
                    imgResult.ImagingResultDetails.ToList().ForEach(x => x.ImagingResultId = imgResult.Id);
                    var existingLabResult = _context.ImagingResult
                        .Where(p => p.Id == imgResult.Id)
                        .Include(x => x.ImagingResultDetails)
                        .SingleOrDefault();

                    if (existingLabResult != null)
                    {
                        // Update parent
                        imgResult.CreatedAt = existingLabResult.CreatedAt;
                        imgResult.CreatedBy = existingLabResult.CreatedBy;
                        if (imgResult.LabTestId != existingLabResult.LabTestId)
                        {
                            imgResult.UnitPrice = _context.LabTests.SingleOrDefault(x => x.Id == imgResult.LabTestId).UnitPrice;
                        }
                        if (imgResult.TechnicianId != null)
                        {
                            if (imgResult.TechnicianId != existingLabResult.TechnicianId || imgResult.LabTestId != existingLabResult.LabTestId)
                            {
                                var technicianFee = _context.LabPerson_LabTests.SingleOrDefault(x => x.LabPersonId == imgResult.TechnicianId && x.LabTestId == imgResult.LabTestId);
                                imgResult.TechnicianFee = technicianFee.Fee;
                                imgResult.TechnicianFeeType = technicianFee.FeeType;
                            }
                        }
                        else
                        {
                            imgResult.TechnicianFee = default(decimal);
                            imgResult.TechnicianFeeType = Core.Enums.EnumFeeType.FeeTypeEnum.FixedAmount;
                        }
                        if (imgResult.ConsultantId != null)
                        {
                            if (imgResult.ConsultantId != existingLabResult.ConsultantId || imgResult.LabTestId != existingLabResult.LabTestId)
                            {
                                var consultantFee = _context.LabPerson_LabTests.SingleOrDefault(x => x.LabPersonId == imgResult.ConsultantId && x.LabTestId == imgResult.LabTestId);
                                imgResult.ConsultantFee = consultantFee.Fee;
                                imgResult.ConsultantFeeType = consultantFee.FeeType;
                            }
                        }
                        else
                        {
                            imgResult.ConsultantFee = default(decimal);
                            imgResult.ConsultantFeeType = Core.Enums.EnumFeeType.FeeTypeEnum.FixedAmount;
                        }
                        _context.Entry(existingLabResult).CurrentValues.SetValues(imgResult);

                        // Delete children
                        _context.ImagingResultDetail.RemoveRange(existingLabResult.ImagingResultDetails);

                        // Insert children
                        existingLabResult.ImagingResultDetails = imgResult.ImagingResultDetails;

                        await _context.SaveChangesAsync();
                        return imgResult;
                    }
                }
               
            }
            return null;
        }

        public override async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var labResult = await _context.ImagingResult.FindAsync(Id);
                if (labResult == null)
                {
                    return false;
                }
                labResult.IsDelete = true;
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbException e)
            {
                Console.WriteLine(e.Message);
                return false;
            }
        }
    }
}
