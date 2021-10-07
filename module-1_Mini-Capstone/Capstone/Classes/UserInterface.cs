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

        private decimal balance = 0M;

        private FileAccess fileAccess = new FileAccess();

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
                Console.WriteLine("Curernet Account Balance: " + balance);

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

        }


    }
}
