using Core.Entities;
using Core.Entities.OrderAggregatte;
using Core.Interfaces;
using Core.Specifications;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data.Services
{
    //we create oredre service to decrease load of controller
    public class OrderService : IorderService
    {

        private readonly IBasketRepository _basketrepo;
        private readonly IUnitOfWork _unitofwork;

        public OrderService(

             IBasketRepository basketrepo,
             IUnitOfWork unitofwork

            )
        {

            _basketrepo = basketrepo;
            _unitofwork = unitofwork;
        }
        public async Task<Order> CreateOrder(string BuyerEmail, int DeliveryMethodId, string BasketId, Address ShippingAddress)
        {
            //get basket fronm repo and here i  just trust items and quantity of items in basket not price and iw will get price from database

            var basket = await _basketrepo.getBasketAsync(BasketId);
            //get item from produvt repo
            //create new list of oredr items
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.items)
            {
                //get the product ittem from product repo ttoknow exactly itis price
                //we send the item of the basket from client so item id of the baasket is the same of item in product table
                var productItem = await _unitofwork.Repository<Product>().GetByIdAsync(item.id);
                var productItemOrdered = new ProductItemOrdered(productItem.Id, productItem.Name, productItem.PicureUrl);
                //every item of the basket is order item include it is quantity 
                var Orderitem = new OrderItem(productItemOrdered, productItem.Price, item.Quantity);
                orderItems.Add(Orderitem);

            }

            //get deliverymethod
            var delieryMethod = await _unitofwork.Repository<DeliveryMethod>().GetByIdAsync(DeliveryMethodId);
            //calculate subtotal
            var subTotal = orderItems.Sum(oi => oi.Price * oi.Quantity);

            //check if order exist or not using paymnetinentid
            //.where(o=>o.paymentIntentId==paymenintentid).firstordefaultAsync()
            var spec = new GetOrderWithPaymnetIntentIdSpecifications();
            var OrderExist = await _unitofwork.Repository<Order>().GetEntityWithSpecification(spec);
            if (OrderExist!=null) {
                // we delete order if we do order before and payment faildand we come to creatte order again
                _unitofwork.Repository<Order>().Delete(OrderExist);
            }


            var order = new Order(orderItems, BuyerEmail, ShippingAddress, delieryMethod, subTotal,basket.PaymentIntentId);
            //create order
            //add order to oredr table
            _unitofwork.Repository<Order>().Add(order);
            //save to database
          var result=  await _unitofwork.Complete();
            //tahtt maean that no save to database
            if (result <= 0) {
                return null;
            }
            //delete basket when make create order i don't need it any more
            //we will delete basket after payment success not order success
            //await _basketrepo.DeleteCustomerBasket(BasketId);
            //return oredr
            return order;
        }

        public async Task<IReadOnlyList<DeliveryMethod>> GetDeliveryyMethodsAsync()
        {
            return await _unitofwork.Repository<DeliveryMethod>().GetAllListAsync();
        }

        public async Task<Order> GetOrderByIdAsync(int id, string BuyerEmail)
        {
            var spec = new OrdersWithItemsAndDMSpecification(id,BuyerEmail);
            return await _unitofwork.Repository<Order>().GetEntityWithSpecification(spec);
        }

        public async Task<IReadOnlyList<Order>> GetOrdersForUserAsync(string BuyerEmail)
        {
            var spec = new OrdersWithItemsAndDMSpecification(BuyerEmail);
            return await _unitofwork.Repository<Order>().GetListWithSpecification(spec);
        }
    }
}
