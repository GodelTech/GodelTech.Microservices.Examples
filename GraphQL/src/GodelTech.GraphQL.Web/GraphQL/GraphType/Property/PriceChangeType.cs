using GodelTech.GraphQL.BL.Models;
using GraphQL.Types;

namespace GodelTech.GraphQL.Web.GraphQL.GraphType.Property
{
    public sealed class PriceChangeType : ObjectGraphType<PriceChange>
    {
        public PriceChangeType()
        {
            Name = "PriceChange";
            Field(_ => _.Direction, type: typeof(StringGraphType));
            Field(_ => _.DateTime, type: typeof(DateTimeGraphType));
            Field(_ => _.Percent, type: typeof(StringGraphType));
            Field(_ => _.Price, type: typeof(IntGraphType));
        }
    }
}