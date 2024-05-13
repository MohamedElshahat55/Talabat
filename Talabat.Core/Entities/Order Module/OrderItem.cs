using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Module
{
    public class OrderItem : ModelBase
    {
        public OrderItem()
        {
            
        }
        public OrderItem( int productId, string productName, string pictureUrl, decimal price, int quantity)
        {
           
            ProductId = productId;
            ProductName = productName;
            PictureUrl = pictureUrl;
            this.price = price;
            Quantity = quantity;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureUrl { get; set; }
        public decimal price  { get; set; }
        public int Quantity { get; set; }

    }
}
