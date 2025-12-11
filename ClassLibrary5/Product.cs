namespace SmartphoneTechnology.Core
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public decimal SalePrice { get; set; }
        public decimal Cost { get; set; }
        public int Stock { get; set; }

        public bool ValidateData()
        {
            return !string.IsNullOrWhiteSpace(Name)
                && !string.IsNullOrWhiteSpace(Brand)
                && !string.IsNullOrWhiteSpace(Model)
                && SalePrice > 0
                && Cost >= 0
                && Stock >= 0;
        }
    }
}
