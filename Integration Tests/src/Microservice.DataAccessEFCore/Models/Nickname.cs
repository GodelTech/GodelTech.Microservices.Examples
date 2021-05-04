using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Microservice.DataAccessEFCore.Models
{
    public class Nickname
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(250)")]
        public string Name { get; set; }

        [ForeignKey(nameof(UserId))]
        public User User { get; set; }

        public int UserId { get; set; }

        public int Age { get; set; }
    }
}
