using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    public class OrderedItems : CateringItem
    {
        public int OrderedQuantity { get; set; } = 0;
        public decimal totalPrice
        {
            get
            {
                return OrderedQuantity * this.Price;
            }
        }

        List<OrderedItems> orderedItems = new List<OrderedItems>(); // Creating a list specific to ordered items to recap when the customer checks out

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
        
        // To achieve the smallest number of bills and coins, we use integer division to get the whole number,
        // subtract that total from the balance, and use the remainer to calculate the next bill
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
            change.Add(nickles);

            return change;
        }
    }
}
