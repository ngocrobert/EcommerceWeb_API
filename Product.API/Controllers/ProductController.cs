using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Errors;
using Product.API.Helper;
using Product.Core;
using Product.Infrastructure;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _uow;
        private readonly IMapper _mapper;

        public ProductController(IUnitOfWork Uow, IMapper mapper)
        {
            _uow = Uow;
            _mapper = mapper;
        }
        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] ProductParams productParams)
        {
            //var res = await _uow.ProductRepository.GetAllAsync(x => x.Category);
            var res = await _uow.ProductRepository.GetAllAsync(productParams);

            var totalitems = await _uow.ProductRepository.CountAsync();

            var result = _mapper.Map<List<ProductDto>>(res);
            return Ok(new Pagination<ProductDto>(productParams.Pagesize, productParams.PageNumber, totalitems, result));
            //return Ok(result);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(BaseCommenResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _uow.ProductRepository.GetByIdAsync(id, x => x.Category);
            if(res is null)
            {
                return NotFound(new BaseCommenResponse(404));
            }
            var result = _mapper.Map<ProductDto>(res);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto createProductDto)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    //var res = _mapper.Map<Products>(createProductDto);
                    var res = await _uow.ProductRepository.AddAsync(createProductDto);
                    return res ? Ok(createProductDto) : BadRequest(res);
                }
                return BadRequest(createProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProduct(int id, [FromForm] UpdateProductDto updateProductDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.UpdateAsync(id, updateProductDto);
                    return res ? Ok(updateProductDto) : BadRequest(res);
                }
                return BadRequest(updateProductDto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var res = await _uow.ProductRepository.DeleteAsync(id);
                    return res ? Ok(res) : BadRequest(res);
                }
                return NotFound($"this id = {id} not found");
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
