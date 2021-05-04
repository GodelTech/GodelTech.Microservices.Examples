using System.Linq;
using System.Threading.Tasks;
using Microservice.DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Microservice.DataAccessEFCore.Queries
{
    public class GetTheYoungestNickname : IGetTheYoungestNickname
    {
        private readonly NicknamesContext _context;

        public GetTheYoungestNickname(NicknamesContext context)
        {
            _context = context;
        }

        public async Task<string> ExecuteAsync(string username)
        {
            var nickname = await _context.Nicknames
                .Include(x => x.User)
                .Where(x => x.User.Name.Equals(username))
                .OrderBy(x => x.Age)
                .FirstOrDefaultAsync();

            return nickname?.Name;
        }
    }
}
