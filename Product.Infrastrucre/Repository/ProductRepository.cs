using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Product.Core;
using Product.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Infrastructure
{
    public class ProductRepository : GenericRepository<Products>, IProductRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IFileProvider _fileProvider;
        private readonly IMapper _mapper;
       
        public ProductRepository(ApplicationDbContext context, IFileProvider fileProvider, IMapper mapper) : base(context)
        {
            _context = context;
            _fileProvider = fileProvider;
            _mapper = mapper;
           // _unitOfWork = unitOfWork;
        }

        public async Task<bool> AddAsync(CreateProductDto dto)
        {
            if(dto.image is not null)
            {
                var root = "/images/product/";
                var productName = $"{Guid.NewGuid()}" + dto.image.FileName;
                if (!Directory.Exists(root))
                {
                    Directory.CreateDirectory(root);
                }
                var src = root + productName;
                var pic_info = _fileProvider.GetFileInfo(src);
                var root_path = pic_info.PhysicalPath;
                using (var file_stream = new FileStream(root_path, FileMode.Create))
                {
                    await dto.image.CopyToAsync(file_stream);
                }

                //Create New Product
                var res = _mapper.Map<Products>(dto);
                res.ProductPicture = src;
                await _context.Products.AddAsync(res);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        //update
        public async Task<bool> UpdateAsync(int id, UpdateProductDto dto)
        {
            //check id
            var currentProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(currentProduct is not null)
            {
                var src = "";
                if(dto.image is not null)
                {
                    var root = "/images/product/";
                    var productName = $"{Guid.NewGuid()}" + dto.image.FileName;
                    if (!Directory.Exists(root))
                    {
                        Directory.CreateDirectory(root);
                    }
                    src = root + productName;
                    var pic_info = _fileProvider.GetFileInfo(src);
                    var root_path = pic_info.PhysicalPath;
                    using (var file_stream = new FileStream(root_path, FileMode.Create))
                    {
                        await dto.image.CopyToAsync(file_stream);
                    }

                }
                //Remove Old Image
                if (!string.IsNullOrEmpty(currentProduct.ProductPicture))
                {
                    var picinfo = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                    var rootpath = picinfo.PhysicalPath;
                    System.IO.File.Delete(rootpath);
                }
                //update product
                var res = _mapper.Map<Products>(dto);
                res.ProductPicture = src;
                res.Id = id;
                _context.Products.Update(res);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }
    }
}
