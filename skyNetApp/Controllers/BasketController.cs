using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Microsoft.AspNetCore.Mvc;
using skyNetApp.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace skyNetApp.Controllers
{
    public class BasketController : BaseApiController
    {
        private readonly IBasketRepository _basketrepo;
        private readonly IMapper _mapper;

        public BasketController(IBasketRepository basketrepo, IMapper mapper)
        {
            _basketrepo = basketrepo;
            _mapper = mapper;
        }


        [HttpGet]

        //from query
        public async Task<ActionResult<CustomerBasket>> getBasketById(string id)
        {
            var basketfromrepo = await _basketrepo.getBasketAsync(id);
            //inary operation
            return Ok(basketfromrepo ?? new CustomerBasket(id));

        }


        //from body
        [HttpPost]
        public async Task<ActionResult<CustomerBasket>> UpdateBasket(CustomerBasketDto basketdto)

        {
            var basket = _mapper.Map<CustomerBasketDto, CustomerBasket>(basketdto);
            var UpdatedBasket = await _basketrepo.UpdateBasketAsync(basket);
            return Ok(UpdatedBasket);

        }

        //from query
        [HttpDelete]

        public async Task<ActionResult> DeleteBaske(string id)
        {

            var deleteResult = await _basketrepo.DeleteCustomerBasket(id);
            if (deleteResult)
            {
                return Ok("basket was deleted");
            }
            return BadRequest("can not delet basket");


        }
    }
}
