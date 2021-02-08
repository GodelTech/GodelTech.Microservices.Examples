using GodelTech.GraphQL.BL.Services;
using GodelTech.GraphQL.Web.GraphQL.GraphType.PropertyNote;
using GodelTech.GraphQL.Web.Models;
using GraphQL;
using GraphQL.Types;

namespace GodelTech.GraphQL.Web.GraphQL
{
    public class PropertiesMutation : ObjectGraphType
    {
        public PropertiesMutation(IPropertiesService service)
        {
            FieldAsync<StringGraphType>(
                "createOrUpdatePropertyNote",
                arguments: new QueryArguments(
                    new QueryArgument<NonNullGraphType<CreateOrUpdatePropertyNoteInputType>> { Name = "propertyNote" }),
                resolve: async context =>
                {
                    var input = await context.GetValidatedArgumentAsync<PropertyNoteInput>("propertyNote");

                    await service.AddNoteToPropertyAsync(input.PropertyId, input.Note);

                    return input.Note;
                });
        }
    }
}