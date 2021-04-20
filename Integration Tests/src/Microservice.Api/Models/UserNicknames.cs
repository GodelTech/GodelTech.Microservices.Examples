using System.Collections.Generic;

namespace Microservice.Api.Models
{
    public class UserNicknames
    {
        public string UserName { get; set; }
        public IEnumerable<string> Nicknames { get; set; }
    }
}
