using System;
using System.Collections.Generic;
using System.Text;

namespace Capstone.Classes
{
    /// <summary>
    /// This class should contain all the "work" for catering
    /// </summary>
    public class Catering
    {
        private List<CateringItem> items = new List<CateringItem>();
        
        public Catering() : base()
        {

        }
   
        public void AddItem(CateringItem item)
        {
            items.Add(item);
        }

        public CateringItem[] AllItems
        {
            get
            {
                return items.ToArray();
            }
        }
    }
}
