using System.Collections.Generic;

namespace SmartphoneTechnology.Core
{
    public class Customer : Person
    {
        public int CustomerId { get; set; }
        public string Address { get; set; }

        public override bool ValidateData()
        {
            return
                !string.IsNullOrWhiteSpace(FirstName) &&
                !string.IsNullOrWhiteSpace(LastName) &&
                !string.IsNullOrWhiteSpace(Phone);
        }

        public ICollection<Sale> Sales { get; set; } = new List<Sale>();
    }
}
