using System.Collections.Generic;

namespace Microservice.Application.Models
{
    public class UserNicknames
    {
        public string UserName { get; set; }
        public IEnumerable<string> Nicknames { get; set; }
    }
}
