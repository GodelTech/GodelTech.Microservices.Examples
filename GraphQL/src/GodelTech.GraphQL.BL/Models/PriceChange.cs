using System;

namespace GodelTech.GraphQL.BL.Models
{
    public class PriceChange
    {
        public string Direction { get; set; }
        
        public DateTime DateTime { get; set; }
        
        public string Percent { get; set; }
        
        public int Price { get; set; }
    }
}