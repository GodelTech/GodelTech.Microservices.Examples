using System;

namespace Microservice.Crm.v1.Contracts.Documents
{
    public class ClientDocument
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
