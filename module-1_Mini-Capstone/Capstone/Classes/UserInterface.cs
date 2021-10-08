using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class provides all user communications, but not much else.
    /// All the "work" of the application should be done elsewhere
    /// </summary>
    public class UserInterface
    {
        //Creating new classes to call their methods
        private Catering catering = new Catering();
        private FileAccess fileAccess = new FileAccess();
        private OrderedItems stuffOrdered = new OrderedItems();

        private decimal balance = 0M;

        public void RunMainMenu()
        {
            //Initializes the menu of items
            fileAccess.LoadMenu(catering);

            bool done = false;

            // First user input
            while (!done)
            {
                Console.Clear();
                Console.WriteLine("Welcome to Deerendra and Phillip's Capstone");
                Console.WriteLine();
                Console.WriteLine("What do you want to do?");
                Console.WriteLine();
                Console.WriteLine("(1) Display Catering Items");
                Console.WriteLine("(2) Order");
                Console.WriteLine("(3) Quit");
                Console.WriteLine();

                string answer = Console.ReadLine();

                switch (answer)
                {
                    case "1":
                        DisplayItems();
                        break;

                    case "2":
                        OrderItems();
                        break;

                    case "3":
                        done = true;
                        break;

                    default:
                        Console.WriteLine("Please read the options again and select a valid answer");
                        break;
                }
            }
        }
        private void DisplayItems()
        {
            //Displaying all items to the console that are available to order and those that are sold out
            Console.WriteLine();
            Console.WriteLine("Type Code Name                 Price   Quantity");
            Console.WriteLine("---- ---- ----                 -----   --------");

            foreach (CateringItem item in this.catering.AllItems)
            {
                Console.Write(item.Type.PadRight(5));
                Console.Write(item.Code.PadRight(5));
                Console.Write(item.Name.PadRight(21));
                Console.Write(item.Price.ToString("C").PadRight(8));

                if (item.Quantity == 0)
                {
                    Console.WriteLine("SOLD OUT");
                }
                else
                {
                    Console.WriteLine(item.Quantity);
                }

            }
            Console.WriteLine();
            Console.WriteLine("Press enter key to continue");
            Console.ReadLine();
            Console.Clear();
        }

        private void OrderItems()
        {
            bool done = false;
            // Second menu for order related actions
            while (!done)
            {
                Console.WriteLine();
                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");
                Console.WriteLine("Current Account Balance: " + balance.ToString("C"));
                Console.WriteLine();
                string userInput = Console.ReadLine();
                Console.WriteLine();

                switch (userInput)
                {
                    case "1":
                        AddMoney();
                        break;
                    case "2":
                        SelectProduct();
                        break;
                    case "3":
                        ReportOrder();
                        done = true;
                        break;
                    default:
                        Console.WriteLine("Please read the instructions");
                        break;
                }
            }

        }
        public void AddMoney()
        {
            Console.WriteLine("Please enter the amount (Max-Amount = $4200): "); // Thoughts on: (Max-Amount = {4200-balance})??
            string answer = Console.ReadLine();
            try // Making sure the user puts in a decimal instead of anything else
            {
                decimal deposit = decimal.Parse(answer);

                if (deposit < 0)
                {
                    Console.WriteLine("You cannot deposit negative amount");
                }
                else if (balance + deposit > 4200)
                {
                    Console.WriteLine("The current Account Balance cannot exceed $4,200");
                }
                else
                {
                    balance += deposit;
                    fileAccess.SavePoint("ADD MONEY:", deposit, balance);
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a valid amount of money.");
            }
        }

        public void SelectProduct()
        {
            //Getting the user's input for the order
            Console.WriteLine("Enter a product code");
            string productCode = Console.ReadLine();
            Console.WriteLine();

            Console.WriteLine("Enter Quantity");
            string quantity = Console.ReadLine();
            Console.WriteLine();
            try
            {

                int quantityInt = int.Parse(quantity);

                bool exists = false;

                foreach (CateringItem item in this.catering.AllItems) // Scrolling through the array to find the item and check its stock
                {
                    if (productCode == item.Code)
                    {
                        exists = true;

                        if (quantityInt > item.Quantity)
                        {
                            Console.WriteLine("Not enough items to fulfill");
                        }
                        else if (balance < quantityInt * item.Price)
                        {
                            Console.WriteLine("Please deposit more money.");
                        }
                        else
                        {
                            item.Quantity -= quantityInt; // Adjusting the stock, adding the item to the list of purchased items
                            OrderedItems ordered = new OrderedItems();
                            ordered.OrderedQuantity = quantityInt;
                            ordered.Code = item.Code;
                            ordered.Type = item.Type;
                            ordered.Name = item.Name;
                            ordered.Price = item.Price;

                            stuffOrdered.AddItem(ordered);

                            balance -= quantityInt * item.Price; // Adjusting the user's balance, adding the item to the Log.txt file
                            fileAccess.SavePoint($"{quantityInt} {ordered.Name} {ordered.Code}", quantityInt * item.Price, balance);
                        }
                    }
                }

                if (exists == false) // If the foreach loop fails to find the item
                {
                    Console.WriteLine("The product doesn't exist");
                }
            }
            catch (FormatException ex)
            {
                Console.WriteLine("Please enter a valid quantity (an integer).");
            }
        }

        public void ReportOrder()
        {
            // The customer has chosen to complete this order
            // Setting the total to 0 which will be added to as the list is looped through and displayed

            decimal orderTotal = 0;
            Console.Clear();
            Console.WriteLine("Today's order:");
            Console.WriteLine();
            Console.WriteLine("Quantity Type Name                 Unit Price Total Price");
            Console.WriteLine("-------- ---- ----                 ---------- -----------");
            foreach (OrderedItems item in this.stuffOrdered.AllItems)
            {
                {
                    Console.Write(item.OrderedQuantity.ToString().PadRight(9));
                    Console.Write(item.Type.PadRight(5));
                    Console.Write(item.Name.PadRight(21));
                    Console.Write(item.Price.ToString("C").PadRight(11));
                    Console.Write(item.totalPrice.ToString("C"));
                    orderTotal += item.totalPrice;
                    Console.WriteLine();
                }

            }
            Console.WriteLine();
            Console.WriteLine("Your total today: " + orderTotal.ToString("C"));
            Console.WriteLine();

            //Makes change and outputs the result
            List<decimal> change = stuffOrdered.MakeChange(balance);
            Console.WriteLine("Your change is: ");
            Console.WriteLine(change[0] + " twenties");
            Console.WriteLine(change[1] + " tens");
            Console.WriteLine(change[2] + " fives");
            Console.WriteLine(change[3] + " ones");
            Console.WriteLine(change[4] + " quarters");
            Console.WriteLine(change[5] + " dimes");
            Console.WriteLine(change[6] + " nickles");
            Console.WriteLine();

            //Writes the action and old balance to Log.txt and resets the balance
            fileAccess.SavePoint("GIVE CHANGE:", balance, 0M);
            balance = 0;
            /*
             * 
             * 
             * Do we need to clear the shopping list to ensure that the next run through is clear?
             * Might need to check with Matt on this
             * 
             * 
             */

            Console.ReadLine();
        }
    }
}
