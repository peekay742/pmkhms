using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories;
using MSIS_HMS.Infrastructure.Data;
using MSIS_HMS.Infrastructure.Interfaces;
using MSIS_HMS.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace MSIS_HMS.Infrastructure.Repositories
{
   public class IPDLabRepository:Repository<IPDLab>,IIPDLabRepository
    {
        public IPDLabRepository(ApplicationDbContext context, IConfigService config) : base(context, config)
        {

        }

    }
}
