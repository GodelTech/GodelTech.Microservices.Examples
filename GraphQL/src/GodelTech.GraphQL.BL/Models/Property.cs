using System;

namespace GodelTech.GraphQL.BL.Models
{
    public class Property
    {
        public string Id { get; set; }
        
        public string CountryCode { get; set; }
        
        public int NumFloors { get; set; }

        public int NumBedrooms { get; set; }
        
        public decimal Latitude { get; set; }
        
        public string AgentAddress { get; set; }
        
        public string Category { get; set; }
        
        public string PropertyType { get; set; }
        
        public decimal Longitude { get; set; }
        
        public string Description { get; set; }
        
        public string PostTown { get; set; }
        
        public string DetailsUrl { get; set; }
        
        public string ShortDescription { get; set; }
        
        public string County { get; set; }
        
        public int Price { get; set; }

        public string Status { get; set; }

        public string AgentName { get; set; }
        
        public string Country { get; set; }

        public DateTime FirstPublishedDate { get; set; }
        
        public string StreetName { get; set; }
        
        public int NumBathrooms { get; set; }
        
        public PriceChange[] PriceChanges { get; set; }
        
        public string AgentPhone { get; set; }

        public string ImageUrl { get; set; }
        
        public DateTime LastPublishedDate { get; set; }

        public string Note { get; set; }
    }
}