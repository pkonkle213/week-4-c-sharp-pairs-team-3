using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    class OrderedItems : CateringItem
    {
        public int OrderedQuantity { get; set; } = 0;
        public decimal totalPrice
        {
            get
            {
                return OrderedQuantity * this.Price;
            }
        }

        List<OrderedItems> orderedItems = new List<OrderedItems>();

        public OrderedItems[] AllItems
        {
            get
            {
                return orderedItems.ToArray();
            }
        }
        public void AddItem(OrderedItems item)
        {
            orderedItems.Add(item);
        }

        public List<decimal> MakeChange(decimal balance)
        {
            List<decimal> change = new List<decimal>();
            int twenties = (int)(balance / 20);
            balance -= 20 * twenties;
            change.Add(twenties);

            int tens = (int)(balance / 10);
            balance -= 10 * tens;
            change.Add(tens);

            int fives = (int)(balance / 5);
            balance -= 5 * fives;
            change.Add(fives);

            int ones = (int)(balance / 1);
            balance -= ones;
            change.Add(ones);

            int quarters = (int)(balance / .25M);
            balance -= .25M * quarters;
            change.Add(quarters);

            int dimes = (int)(balance / .1M);
            balance -= .1M * dimes;
            change.Add(dimes);

            int nickles = (int)(balance / .05M);
            balance -= .05M * nickles;
            change.Add(nickles);

            return change;
        }
    }
}
