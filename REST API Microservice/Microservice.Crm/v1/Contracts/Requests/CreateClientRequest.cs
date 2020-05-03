using System.ComponentModel.DataAnnotations;

namespace Microservice.Crm.v1.Contracts.Requests
{
    public class CreateClientRequest
    {
        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }
    }
}
