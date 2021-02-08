using GodelTech.GraphQL.BL.Services;
using GodelTech.GraphQL.Web.GraphQL.GraphType.Property;
using GraphQL.Types;

namespace GodelTech.GraphQL.Web.GraphQL
{
    public class PropertiesQuery : ObjectGraphType
    {
        public PropertiesQuery(
            IPropertiesService propertiesService)
        {
            FieldAsync<ListGraphType<PropertyType>>(
                "Properties",
                resolve: async context => await propertiesService.GetAllPropertiesAsync());
        }
    }
}