using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microservice.DataAccess.Interfaces;
using Microservice.DataAccessEFCore.Exceptions;
using Microservice.DataAccessEFCore.Mappers;
using Microservice.Domain;
using Microsoft.EntityFrameworkCore;

namespace Microservice.DataAccessEFCore.Queries
{
    public class AddNicknames : IAddNicknames
    {
        private readonly NicknamesContext _context;

        public AddNicknames(NicknamesContext context)
        {
            _context = context;
        }

        public async Task ExecuteAsync(string userName, IEnumerable<Nickname> nicknames)
        {
            if (userName == null)
            {
                throw new ArgumentNullException(nameof(userName));
            }

            if (nicknames == null)
            {
                throw new ArgumentNullException(nameof(nicknames));
            }

            var user = await _context.Users.FirstOrDefaultAsync(x => x.Name.Equals(userName));

            if (user == null)
            {
                throw new UserNotFoundException(userName);
            }

            var userNicknames = _context.Nicknames.Where(x => x.Id == user.Id);

            foreach (var nickname in nicknames)
            {
                var existedNickname = await userNicknames.FirstOrDefaultAsync(x => x.Name.Equals(nickname.Name));

                if (existedNickname != null)
                {
                    existedNickname.Age = nickname.Age ?? default;
                    continue;
                }

                await _context.Nicknames.AddAsync(nickname.ToDao(user.Id));
            }

            await _context.SaveChangesAsync();
        }
    }
}
