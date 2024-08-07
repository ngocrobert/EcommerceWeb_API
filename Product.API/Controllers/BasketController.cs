using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.Core;
using Product.Core.Dto;
using Product.Core.Entities;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BasketController : ControllerBase
    {
        private readonly IUnitOfWork _uOW;
        private readonly IMapper _mapper;

        public BasketController(IUnitOfWork UOW, IMapper mapper)
        {
            _uOW = UOW;
            _mapper = mapper;
        }

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetBasketById(string Id)
        {
            var _basket = await _uOW.BasketRepository.GetBasketAsync(Id);
            return Ok(_basket ?? new CustomerBasket(Id));
        }

        [HttpPost]
        public async Task<IActionResult> UpdateBasket(CustomerBasket customerBasket)
        {

            //var result = _mapper.Map<CustomerBasketDto, CustomerBasket>(customerBasket);
            var _basket = await _uOW.BasketRepository.UpdateBasketAsync(customerBasket);

            return Ok(_basket);
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> DeleteBasket(string Id)
        {
            return Ok(await _uOW.BasketRepository.DeleteBasketAsync(Id));
        }
    }
}
