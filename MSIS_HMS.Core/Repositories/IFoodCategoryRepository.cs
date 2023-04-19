﻿using MSIS_HMS.Core.Entities;
using MSIS_HMS.Core.Repositories.Base;
using System.Collections.Generic;

namespace MSIS_HMS.Core.Repositories
{
    public interface IFoodCategoryRepository : IRepository<FoodCategory>
    {
        List<FoodCategory> GetAll();
    }
}