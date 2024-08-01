﻿using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> GetAll()
        {
            var res = await _uow.ProductRepository.GetAllAsync(x => x.Category);
            var result = _mapper.Map<List<ProductDto>>(res);
            return Ok(result);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await _uow.ProductRepository.GetByIdAsync(id,x => x.Category);
            var result = _mapper.Map<ProductDto>(res);
            return Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> AddProduct([FromForm] CreateProductDto createProductDto)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    //var res = _mapper.Map<Products>(createProductDto);
                   var res = await _uow.ProductRepository.AddAsync(createProductDto);
                    return res ? Ok(createProductDto) : BadRequest(res);
                }
                return BadRequest(createProductDto);
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}