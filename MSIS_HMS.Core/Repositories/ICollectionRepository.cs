using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Repositories
{
    public interface ICollectionRepository : IRepository<Collection>
    {
        List<Collection> GetAll(int? BranchId = null, string collectionName = null, int? Id = null);
    }
}
