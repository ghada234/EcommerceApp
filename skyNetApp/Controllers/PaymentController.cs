using Core.Entities;
using Core.Interfaces;
using Infrastructure.Data.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using skyNetApp.Errors;
using Stripe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Order = Core.Entities.OrderAggregatte.Order;





namespace skyNetApp.Controllers
{
    public class PaymentController : BaseApiController
    {
        private readonly IPaymentService _paymentservice;
        private readonly ILogger<PaymentController> _logger;
        private readonly IConfiguration _config;
        //we will get the value of whsecret from stripe to tell us that we can trust what that send to us
        //we get it from cli sripe listen
        private readonly string _WhSecret;


        public PaymentController(IPaymentService paymentservice,ILogger<PaymentController> logger,IConfiguration config)
        {
           _paymentservice = paymentservice;
            _logger = logger;
            _WhSecret = config.GetSection("StripeSetting:WhSecret").Value;
        }
      
       
        [Authorize]
        [HttpPost("{basketId}")]

        public async Task<ActionResult<CustomerBasket>> CreateOrUpdatePaymentIntent (string basketId) {

            var basket = await _paymentservice.CreateOrUpdatePaymentIntent(basketId);

            if (basket == null) {
                return BadRequest(new ApiResponse(400, "there is no basket"));
            }
            return Ok(basket);

        }
        [HttpPost("webhook")]

       // A webhook enables Stripe to push real-time notifications to your app.Stripe uses HTTPS to send these notifications to your app as a 
            //JSON payload.You can then use these notifications to execute actions in your backend systems.
        //we want stripe to listen to specific events and then forward this events tto api server
        //using stripe listen -f /api/webhooks
        public async Task<ActionResult> StripeWebhook() {
            //get the body of the request tha sent by stripe
            //json payload
            var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();
            //parse ajson string form stripe webhooks in to event
            //this is the part we confirming the paymnet from client
            var StripEvent = EventUtility.ConstructEvent(json, Request.Headers["Stripe-Signature"], _WhSecret);
            PaymentIntent intent;
            Order order;
            switch (StripEvent.Type)
            {
                case "payment_intent.succeeded":
                    intent= (PaymentIntent)StripEvent.Data.Object;
                    _logger.LogInformation("payment sucess", intent.Id);
                    //update order with new status
                    order = await _paymentservice.updateOrderPaymentSuccess(intent.Id);
                    _logger.LogInformation("updating order stauts sucessrs", order.Id);


                    break;
                  

                case "payment_intent.failed":

                    intent = (PaymentIntent)StripEvent.Data.Object;
                    _logger.LogInformation("payment faild", intent.Id);
                    //UPDATE ORDER STatus
                    order = await _paymentservice.updateOrderPaymentFaield(intent.Id);
                    _logger.LogInformation("updating order status failed", order.Id);

                    break;
                    

            }
            // if we don't tell the webhooks that we recieved the event the webhooks will send event until 3 days
            return new EmptyResult();



        }
       

    }
}
