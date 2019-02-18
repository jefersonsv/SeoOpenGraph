using System;
using System.Collections.Generic;
using System.Text;

namespace SeoOpenGraph.ObjectTypes
{
    [Namespace("product", "http://ogp.me/ns/product#")]
    public class Product : IObjectType
    {
        public Product(double amount, string currency, string retailerItemId, Condition condition, Availability availability)
        {
            this.Price = new List<Currency>();
            this.Price.Add(new Currency
            {
                Amount = amount,
                CurrencyText = currency
            });

            this.Sale_Price = new List<Currency>();

            this.Retailer_Item_Id = retailerItemId;
            this.Condition = condition;
            this.Availability = availability;
        }

        public string Brand { get; set; }
        public string Category { get; set; }
        public string Color { get; set; }
        public string Material { get; set; }
        public List<Currency> Price { get; set; }
        public List<Currency> Sale_Price { get; set; }
        public string Retailer_Category { get; set; }
        public string Retailer_Group_Id { get; set; }
        public string Retailer_Item_Id { get; set; }
        public Condition Condition { get; set; }
        public Availability Availability { get; set; }
    }
}
