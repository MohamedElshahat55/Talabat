using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Module
{
    public class Order : ModelBase
    {
        public Order()
        {
            
        }
        public Order(string buyerEmail, Address shippingAddress, DeliveryMethod deliveryMethod, ICollection<OrderItem> items, decimal subTotal)
        {
            BuyerEmail = buyerEmail;
            ShippingAddress = shippingAddress;
            DeliveryMethod = deliveryMethod;
            Items = items;
            SubTotal = subTotal;
        }


        public string BuyerEmail { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now; // Date Of Order Creation

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        public Address ShippingAddress { get; set; }

        //public int DeliveryMethodId { get; set; } // Forign Key
        public DeliveryMethod DeliveryMethod { get; set; } // navigational property[1-1]

        public ICollection<OrderItem> Items { get; set; } = new HashSet<OrderItem>();
        public decimal SubTotal { get; set; }

        // this method will not mapped in data base becouse EF can't mapped any Methods
        public decimal GetTotal()
        => SubTotal + DeliveryMethod.Cost;
        

        public string PaymentIntendId { get; set; } = String.Empty;

    }
}
