using System.ComponentModel.DataAnnotations;

namespace Microservice.Crm.DataLayer.Entities
{
    public class Client
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(128)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(128)]
        public string LastName { get; set; }
    }
}
