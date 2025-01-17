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
        Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams);

        Task<bool> AddAsync(CreateProductDto dto);
        Task<bool> UpdateAsync(int id, UpdateProductDto dto);
        Task<bool> DeleteAsync(int id);
    }
}
