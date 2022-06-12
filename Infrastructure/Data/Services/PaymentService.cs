using Core.Entities;
using Core.Entities.OrderAggregatte;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.Extensions.Configuration;
using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Order = Core.Entities.OrderAggregatte.Order;
using Product = Core.Entities.Product;

namespace Infrastructure.Data.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IBasketRepository _basketrepo;
        private readonly IConfiguration _config;
        private readonly IUnitOfWork _unitofwork;

        public PaymentService(IBasketRepository basketrepo, IConfiguration config, IUnitOfWork unitofwork)
        {
            _basketrepo = basketrepo;
            _config = config;
            _unitofwork = unitofwork;
        }
        public async Task<CustomerBasket> CreateOrUpdatePaymentIntent(string basketid)

        {
            //get secretkey and evaluate it with apikey in stripe
            StripeConfiguration.ApiKey = _config["StripeSetting:SecretKey"];
            //get the basket
            var basket = await _basketrepo.getBasketAsync(basketid);

            if (basket == null) {
                return null;
            }
            var shippingPrice = 0m;
            //in this stage we will add some new props to basketcustomer lik deliverymethod to get shipping cost
            //and another props because of stripe like paymentintentid and client secret

            if (basket.DeliveryMethodId.HasValue)
            {
                var deliveryMethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync((int)basket.DeliveryMethodId);
                shippingPrice = deliveryMethod.Price;

            }

            //here we ant to confirm prices inside the basket is accurate
            //i can't trust prices that client sent
            foreach (var item in basket.items)
            {
                var productitem = await _unitofwork.Repository<Product>().GetByIdAsync(item.id);

                if (productitem.Price != item.Price)
                {
                    item.Price = productitem.Price;
                }
            }

                var intentService = new PaymentIntentService();
                //create instance of paymentintent
                PaymentIntent intent;
                //we want to check if we create new paymnetintent or update it

                if (string.IsNullOrEmpty(basket.PaymentIntentId))
                {
                    //create new paymentintent
                    var options = new PaymentIntentCreateOptions
                    {
                        //stripe ake the number in long format

                        //price is decimal and to convert it tolong i should multiply by 100 
                        Amount = (long)basket.items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,
                        Currency = "usd",
                        PaymentMethodTypes = new List<string> {"card"},


                    };
                    intent = await intentService.CreateAsync(options);
                    basket.PaymentIntentId = intent.Id;
                    basket.ClientSecret = intent.ClientSecret;



                }

                else
                //it happens when client checkout and then return again toadd ne witems to basket or change items to basket
                {
                    var options = new PaymentIntentUpdateOptions
                    {
                        Amount = (long)basket.items.Sum(i => i.Quantity * (i.Price * 100)) + (long)shippingPrice * 100,

                    };

                    await intentService.UpdateAsync(basket.PaymentIntentId, options);
                }

                //update basket with new values in database
                await _basketrepo.UpdateBasketAsync(basket);
            return basket;
                
            
        }

        public async Task<Core.Entities.OrderAggregatte.Order> updateOrderPaymentFaield(string paymnetintetntid)
        {
            var spec = new GetOrderWithPaymnetIntentIdSpecifications(paymnetintetntid);
            var order = await _unitofwork.Repository<Order>().GetEntityWithSpecification(spec);
            if (order == null)
            {
                return null;
            }

            order.Status = OrderStatus.PaymentFaild;
            _unitofwork.Repository<Order>().update(order);
            await _unitofwork.Complete();
            return order;
        }

        public async Task<Core.Entities.OrderAggregatte.Order> updateOrderPaymentSuccess(string paymnetintetntid)
        {
            var spec = new GetOrderWithPaymnetIntentIdSpecifications(paymnetintetntid);
           var order= await _unitofwork.Repository<Order>().GetEntityWithSpecification(spec);
            if (order == null) {
                return null;
            }

            order.Status = OrderStatus.PayementRecived;
          _unitofwork.Repository<Order>().update(order);
            await _unitofwork.Complete();
            return order;




        }
    }
}
