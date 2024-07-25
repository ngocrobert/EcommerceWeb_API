﻿using Product.Core;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;

        public ICategoryRepository CategoryRepository { get; }

        public IProductRepository ProductRepository { get; }

        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            CategoryRepository = new CategoryRepository(_context);
            ProductRepository = new ProductRepository(_context);
        }
    }
}
