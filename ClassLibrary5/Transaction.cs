using System;

namespace SmartphoneTechnology.Core
{
    public abstract class Transaction
    {
        public DateTime Date { get; set; }
        public string PaymentMethod { get; set; }

        public abstract decimal CalculateTotal();
    }
}
