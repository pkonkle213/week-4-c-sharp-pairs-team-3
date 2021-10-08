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
        public string fileOutPath = @"C:\Catering\Log.txt";

        public void LoadMenu(Catering items)
        {

            try
            {
                using (StreamReader reader = new StreamReader(filePath))
                {
                    while (!reader.EndOfStream)
                    {
                        string line = reader.ReadLine(); // Reads the line of the file and splits it into usable pieces

                        string[] parts = line.Split("|");

                        CateringItem item = new CateringItem();
                        item.Type = parts[0];
                        item.Code = parts[1];
                        item.Name = parts[2];
                        item.Price = decimal.Parse(parts[3]);

                        items.AddItem(item); // Calls the AddItem method to add to the list of available items to order
                    }
                }
            }
            catch (IOException ex)
            {
                Console.WriteLine("Problem reading from " + filePath);
                Console.WriteLine(ex.Message);
            }

        }

        public void SavePoint(string actionTaken, decimal delta, decimal after)
        {
            try
            {
                using (StreamWriter writer = new StreamWriter(fileOutPath,true)) // Opens a file to write a log (or begins the file). Notes the date, action taken, the difference, and the new total
                {
                    writer.WriteLine($"{DateTime.Now} {actionTaken} {delta.ToString("C")} {after.ToString("C")}");
                }
            }
            catch (FileNotFoundException ex)
            {
                Console.WriteLine("No file or folder found." + ex.Message);
            }
        }
    }
}