using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Core.Repositories
{
    public interface ICollectionGroupRepository : IRepository <CollectionGroup>
    {
        List<CollectionGroup>GetAll(int?BarnchId= null,string Name=null);
        //List<CollectionGroup> GetAll(int? BarnchId = null, string Name = null);
    }
}
