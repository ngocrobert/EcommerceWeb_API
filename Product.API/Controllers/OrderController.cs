using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Product.API.Errors;
using Product.Core;
using Product.Core.Dto;
using Product.Core.Entities;
using Product.Core.Services;
using System.Security.Claims;

namespace Product.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class OrderController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _UOW;
        private readonly IOrderServices _orderServices;

        public OrderController(IMapper mapper, IUnitOfWork UOW, IOrderServices orderServices)
        {
            _mapper = mapper;
            _UOW = UOW;
            _orderServices = orderServices;
        }

        [HttpPost("Create-Order")]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderDto)
        {
            var Email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var Address = _mapper.Map<AddressDto, ShipAddress>(orderDto.ShipToAddress);
            var order = await _orderServices.CreateOrderAsync(Email, orderDto.DeliveryMethodID, orderDto.BasketId, Address);
            if (order is null) return BadRequest(new BaseCommenResponse(400, "Error While Creating Order"));
            return Ok(order);
        }
        // Lấy thông tin Order theo User
        [HttpGet("Get-order-for-user")]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrderForUser()
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var order = await _orderServices.GetOrdersForUserAsync(email);
            var result = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(order);
            //var result = _mapper.Map<List<OrderToReturnDto>>(order);
            return Ok(result);
        }

        //Lấy thông tin Order theo OrderID
        [HttpGet("get-order-by-id/{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderByID(int id)
        {
            var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var order = await _orderServices.GetOrderByIdAsync(id, email);
            if (order is null) return NotFound(new BaseCommenResponse(404));
            var result = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(result);
        }

        //Lấy thông tin các Delivery
        [HttpGet("get-delivery-method")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> GetDeliveryMethod()
        {
            return Ok(await _orderServices.GetDeliveryMethodsAsync());
        }
    }
}
