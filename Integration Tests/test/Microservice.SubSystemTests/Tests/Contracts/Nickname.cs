namespace Microservice.SubSystemTests.Tests.Contracts
{
    public class Nickname
    {
        public Nickname(
            string name,
            int? age)
        {
            Name = name;
            Age = age;
        }

        public string Name { get; }

        public int? Age { get; }
    }
}