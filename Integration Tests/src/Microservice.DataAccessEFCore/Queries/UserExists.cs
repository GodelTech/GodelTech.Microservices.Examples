using System;
using System.Threading.Tasks;
using Microservice.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.DataAccessEFCore.Queries
{
    public class UserExists : IUserExists
    {
        private readonly NicknamesContext _context;

        public UserExists(NicknamesContext context)
        {
            _context = context;
        }

        public async Task<bool> ExecuteAsync(string userName)
        {
            if (string.IsNullOrEmpty(userName))
            {
                throw new ArgumentException("Artist name must not be null or Empty", nameof(userName));
            }

            return await _context.Users.AnyAsync(x => x.Name.Equals(userName));
        }
    }
}
