using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GodelTech.GraphQL.BL.Models;

namespace GodelTech.GraphQL.BL.Services.Impl
{
    public class PropertiesService : IPropertiesService
    {
        public Task<IEnumerable<Property>> GetAllPropertiesAsync()
        {
            var properties = new[]
            {
                new Property
                {
                    Id = Guid.NewGuid().ToString(),
                    AgentAddress = nameof(Property.AgentAddress),
                    AgentName = nameof(Property.AgentName),
                    CountryCode = nameof(Property.CountryCode),
                    NumFloors = 1,
                    NumBedrooms = 3,
                    Latitude = (decimal) 50.0001,
                    Longitude = (decimal) -1.0001,
                    Category = nameof(Property.Category),
                    PropertyType = nameof(Property.PropertyType),
                    Description = nameof(Property.Description),
                    PostTown = nameof(Property.PostTown),
                    DetailsUrl = nameof(Property.DetailsUrl),
                    ShortDescription = nameof(Property.ShortDescription),
                    County = nameof(Property.County),
                    Price = 5000000,
                    Status = nameof(Property.Status),
                    FirstPublishedDate = DateTime.UtcNow,
                    LastPublishedDate = DateTime.UtcNow,
                    StreetName = nameof(Property.StreetName),
                    NumBathrooms = 12,
                    AgentPhone = nameof(Property.AgentPhone),
                    ImageUrl = nameof(Property.ImageUrl),
                    Note = nameof(Property.Note),
                    Country = "Belarus",
                    PriceChanges = new[]
                    {
                        new PriceChange
                        {
                            Direction = "",
                            DateTime = DateTime.UtcNow,
                            Percent = "0%",
                            Price = 5000000,
                        },
                        new PriceChange
                        {
                            Direction = "Up",
                            DateTime = DateTime.UtcNow,
                            Percent = "10%",
                            Price = 5500000,
                        }
                    }
                }
            };
            
            return Task.FromResult(properties.AsEnumerable());
        }

        public Task AddNoteToPropertyAsync(string propertyId, string note)
        {
            return Task.CompletedTask;
        }
    }
}