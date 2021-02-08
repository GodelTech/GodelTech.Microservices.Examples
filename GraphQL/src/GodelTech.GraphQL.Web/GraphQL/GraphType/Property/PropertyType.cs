using GraphQL.Types;

namespace GodelTech.GraphQL.Web.GraphQL.GraphType.Property
{
    public sealed class PropertyType : ObjectGraphType<BL.Models.Property>
    {
        public PropertyType()
        {
            Name = "Property";
            Field(_ => _.Id, type: typeof(StringGraphType));
            Field(_ => _.CountryCode, type: typeof(StringGraphType));
            Field(_ => _.NumFloors, type: typeof(IntGraphType));
            Field(_ => _.NumBedrooms, type: typeof(IntGraphType));
            Field(_ => _.Latitude, type: typeof(DecimalGraphType));
            Field(_ => _.AgentAddress, type: typeof(StringGraphType));
            Field(_ => _.Category, type: typeof(StringGraphType));
            Field(_ => _.PropertyType, type: typeof(StringGraphType));
            Field(_ => _.Longitude, type: typeof(DecimalGraphType));
            Field(_ => _.Description, type: typeof(StringGraphType));
            Field(_ => _.PostTown, type: typeof(StringGraphType));
            Field(_ => _.DetailsUrl, type: typeof(StringGraphType));
            Field(_ => _.ShortDescription, type: typeof(StringGraphType));
            Field(_ => _.County, type: typeof(StringGraphType));
            Field(_ => _.Price, type: typeof(IntGraphType));
            Field(_ => _.Status, type: typeof(StringGraphType));
            Field(_ => _.AgentName, type: typeof(StringGraphType));
            Field(_ => _.Country, type: typeof(StringGraphType));
            Field(_ => _.FirstPublishedDate, type: typeof(DateTimeGraphType));
            Field(_ => _.StreetName, type: typeof(StringGraphType));
            Field(_ => _.NumBathrooms, type: typeof(IntGraphType));
            Field(_ => _.PriceChanges, type: typeof(ListGraphType<PriceChangeType>));
            Field(_ => _.AgentPhone, type: typeof(StringGraphType));
            Field(_ => _.ImageUrl, type: typeof(StringGraphType));
            Field(_ => _.LastPublishedDate, type: typeof(DateTimeGraphType));
            Field(_ => _.Note, type: typeof(StringGraphType));
        }
    }
}