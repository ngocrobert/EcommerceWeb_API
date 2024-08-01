﻿using Product.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Core
{
    public interface IProductRepository : IGenericRepository<Products>
    {
        Task<bool> AddAsync(CreateProductDto dto);
    }
}