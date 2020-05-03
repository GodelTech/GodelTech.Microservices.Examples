using System.Linq;
using GodelTech.Microservices.EntityFrameworkCore.Specifications;
using Microservice.Crm.DataLayer.Entities;

namespace Microservice.Crm.DataLayer.Specifications
{
    public class ClientSearchSpecification : Specification<Client>
    {
        public string FirstName { get; set; }

        public override IQueryable<Client> AddPredicates(IQueryable<Client> query)
        {
            if (!string.IsNullOrWhiteSpace(FirstName))
                query = query.Where(x => x.FirstName == FirstName);

            return query;
        }

        public override IQueryable<Client> AddSorting(IQueryable<Client> query)
        {
            return query.OrderBy(x => x.Id);
        }
    }
}
