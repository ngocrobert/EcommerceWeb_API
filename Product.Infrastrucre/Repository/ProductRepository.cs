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

        public async Task<IEnumerable<ProductDto>> GetAllAsync(ProductParams productParams)
        {
            var query = await _context.Products.Include(x => x.Category).AsNoTracking().ToListAsync();

            //Filter by category Id
            if (productParams.CategoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == productParams.CategoryId.Value).ToList();
            }

            //Sorting
           
            if(!string.IsNullOrEmpty(productParams.Sorting))
            {
                query = productParams.Sorting switch
                {
                    "PriceAsc" => query.OrderBy(x => x.Price).ToList(),
                    "PriceDesc" => query.OrderByDescending(x => x.Price).ToList(),
                    _ => query.OrderBy(x => x.Name).ToList(),
                };
            }
            var _result = _mapper.Map<List<ProductDto>>(query);
            return _result;
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

        //Delete
        public async Task<bool> DeleteAsync(int id)
        {
            var currentProduct = await _context.Products.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);
            if(!string.IsNullOrEmpty(currentProduct.ProductPicture)) {
                //delete old pic 
                var pic_info = _fileProvider.GetFileInfo(currentProduct.ProductPicture);
                var root_path = pic_info.PhysicalPath;
                System.IO.File.Delete($"{root_path}");

                //delete DB
                _context.Products.Remove(currentProduct);
                await _context.SaveChangesAsync();
                return true;
            }
            return false;
        }

        
    }
}
