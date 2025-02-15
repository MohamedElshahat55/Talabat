﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Talabat.Core.Entities.Order_Module
{
    public class DeliveryMethod : ModelBase
    {
        public DeliveryMethod()
        {
            
        }
        public DeliveryMethod( string shortName, string description, string deliveryTime, decimal cost)
        {
            ShortName = shortName;
            Description = description;
            DeliveryTime = deliveryTime;
            Cost = cost;
        }

        public string ShortName { get; set; }
        public string Description { get; set; }
        public string DeliveryTime { get; set; } //  e.g => 1 day or 2 days
        public decimal Cost { get; set; }
    }
}
