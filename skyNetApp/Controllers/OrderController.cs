using AutoMapper;
using Core.Entities.OrderAggregatte;
using Core.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using skyNetApp.Dto;
using skyNetApp.Errors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace skyNetApp.Controllers
{

    [Authorize]
    public class OrderController : BaseApiController
    {
        private readonly IorderService _orderservice;
        private readonly IMapper _mapper;

        public OrderController(IorderService orderservice, IMapper mapper)
        {
            _orderservice = orderservice;
            _mapper = mapper;
        }


        [HttpPost]
        public async Task<ActionResult<Order>> CreateOrder(OrderDto orderdto)


        {
            //email of current loged in user
            //old method
            //var email = HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == ClaimTypes.Email).Value;
            //new metthod
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var Address = _mapper.Map<AddressDto, Address>(orderdto.ShipToAddress);
            var order = await _orderservice.CreateOrder(email, orderdto.DeliveryMethodId, orderdto.BasketId, Address);

            if (order == null)
            {

                return BadRequest(new ApiResponse(400, "Problem when creating order"));
            }
            return Ok(order);

        }

        //get oredres mehod
        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<OrderToReturnDto>>> GetOrdersForUser()
        {
            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var Orders = await _orderservice.GetOrdersForUserAsync(email);
            var OrdersoReturn = _mapper.Map<IReadOnlyList<Order>, IReadOnlyList<OrderToReturnDto>>(Orders);
            return Ok(OrdersoReturn);
        }
        //get specific order for user

        [HttpGet("{id}")]
        public async Task<ActionResult<OrderToReturnDto>> GetOrderForUser(int id)
        {

            var email = HttpContext.User.FindFirstValue(ClaimTypes.Email);
            var order = await _orderservice.GetOrderByIdAsync(id, email);
            if (order == null)
            {
                return NotFound(new ApiResponse(404));
            }
            var orderToReturn = _mapper.Map<Order, OrderToReturnDto>(order);
            return Ok(orderToReturn);


        }

        //get deliverymethods

        [HttpGet("deliverymethod")]
        public async Task<ActionResult<IReadOnlyList<DeliveryMethod>>> getDeliveryMehods() {

            var deliverymethods = await _orderservice.GetDeliveryyMethodsAsync();
            return Ok(deliverymethods);
        
        }

    }
}
