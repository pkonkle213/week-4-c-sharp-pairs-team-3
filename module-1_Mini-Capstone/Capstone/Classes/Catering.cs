using System.Collections.Generic;

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
   
        public void AddItem(CateringItem item) // This method adds the passed in variable to the list of catering items avaiable to order
        {
            items.Add(item);
        }

        public CateringItem[] AllItems // Converts the list to an array to call later
        {
            get
            {
                return items.ToArray();
            }
        }
    }
}
