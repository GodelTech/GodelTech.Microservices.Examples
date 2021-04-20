using System.Collections.Generic;
using Microservice.IntegrationTests.DefaultTestData;
using Microservice.DataAccessEFCore;
using Microservice.DataAccessEFCore.Models;

namespace Microservice.IntegrationTests.Extensions
{
    public static class InMemoryDatabaseExtensions
    {
        public static void AddNickname(this NicknamesContext context, Nickname nickname)
        {
            context.Nicknames.Add(nickname);
            context.SaveChanges();
        }

        public static void AddNicknames(this NicknamesContext context, IEnumerable<Nickname> nicknames)
        {
            context.Nicknames.AddRange(nicknames);
            context.SaveChanges();
        }

        public static void AddDefaultUser(this NicknamesContext context)
        {
            context.Users.Add(DataAccessTestData.DefaultUser);
            context.SaveChanges();
        }
    }
}
