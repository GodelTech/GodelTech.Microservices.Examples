using System.Collections.Generic;
using System.Threading.Tasks;
using GodelTech.GraphQL.BL.Models;

namespace GodelTech.GraphQL.BL.Services
{
    public interface IPropertiesService
    {
        Task<IEnumerable<Property>> GetAllPropertiesAsync();

        Task AddNoteToPropertyAsync(string propertyId, string note);
    }
}