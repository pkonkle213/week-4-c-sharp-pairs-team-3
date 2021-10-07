using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class OrderItems : CateringItem
    {

        public int OrderQuantity { get; set; }

        public decimal totalPrice
        {
            get
            {
                return OrderQuantity * this.Price;
            }
        }

    }
}
