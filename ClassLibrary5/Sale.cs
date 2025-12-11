using SmartphoneTechnology;
using System;
using System.Collections.Generic;

namespace SmartphoneTechnology.Core
{
    public partial class Sale
    {
        public int SaleId { get; set; }
        public DateTime Date { get; set; }
        public int CustomerId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<SaleDetail> SaleDetails { get; set; } = new List<SaleDetail>();
    }
}
