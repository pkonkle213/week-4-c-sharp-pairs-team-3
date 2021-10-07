using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Capstone.Classes
{
    /// <summary>
    /// This class should contain any and all details of access to files
    /// </summary>
    public class FileAccess
    {
        // All external data files for this application should live in this directory.
        // You will likely need to create this directory and copy / paste any needed files.
        public string filePath = @"C:\Catering\cateringsystem.csv";

        public void LoadMenu(Catering items)
        {

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine();

                        string[] parts = line.Split("|");

                        CateringItem item = new CateringItem();
                        item.Type = parts[0];
                        item.Code = parts[1];
                        item.Name = parts[2];
                        item.Price = decimal.Parse(parts[3]);

                        items.AddItem(item);
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Problem reading from " + filePath);
            }

        }
    }
}