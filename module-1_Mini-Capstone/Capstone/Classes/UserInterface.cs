﻿using System;
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
        private Catering catering = new Catering();
        private FileAccess fileAccess = new FileAccess();
        private OrderedItems stuffOrdered = new OrderedItems();

        private decimal balance = 0M;

        public void RunMainMenu()
        {
            fileAccess.LoadMenu(catering);

            bool done = false;

            while (!done)
            {
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
        }

        private void OrderItems()
        {
            bool done = false;

            while (!done)
            {

                Console.WriteLine("(1) Add Money");
                Console.WriteLine("(2) Select Products");
                Console.WriteLine("(3) Complete Transaction");
                Console.WriteLine("Current Account Balance: " + balance);

                string userInput = Console.ReadLine();

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
            Console.WriteLine("Please enter the amount (Max-Amount = 4200): ");
            string answer = Console.ReadLine();

            decimal deposit = decimal.Parse(answer);

            if (deposit < 0)
            {
                Console.WriteLine("You cannot deposit negative amount");
            }

            else if (balance + deposit > 4200)
            {
                Console.WriteLine("The current Account Balance cannot exceed $4200");
            }

            else
            {
                balance += deposit;
            }

        }

        public void SelectProduct()
        {
            Console.WriteLine("Enter a product code");
            string productCode = Console.ReadLine();

            Console.WriteLine("Enter Quantity");
            string quantity = Console.ReadLine();

            int quantityInt = int.Parse(quantity);

            bool exists = false;

            foreach (CateringItem item in this.catering.AllItems)
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
                        item.Quantity -= quantityInt;
                        OrderedItems ordered = new OrderedItems();
                        ordered.OrderedQuantity = quantityInt;
                        ordered.Type = item.Type;
                        ordered.Name = item.Name;
                        ordered.Price = item.Price;

                        stuffOrdered.AddItem(ordered);

                        balance -= quantityInt * item.Price;
                    }
                }
            }

            if (exists == false)
            {
                Console.WriteLine("The product doesn't exist");
            }
        }

        public void ReportOrder()
        {
            decimal orderTotal = 0;
            foreach (OrderedItems item in this.stuffOrdered.AllItems)
            {
                 {
                    Console.Write(item.OrderedQuantity.ToString().PadRight(5));
                    Console.Write(item.Type.PadRight(5));
                    Console.Write(item.Name.PadRight(21));
                    Console.Write(item.Price.ToString("C").PadRight(10));
                    Console.Write(item.totalPrice.ToString("C").PadRight(10));
                    orderTotal += item.totalPrice;
                    Console.WriteLine();
                 }

            }
            Console.WriteLine("Your total today: " + orderTotal.ToString("C"));
            

            //Make change
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
            balance = 0;
            Console.ReadLine();
        }
    }
}
