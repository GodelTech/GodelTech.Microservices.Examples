using GodelTech.GraphQL.Web.Models;
using GraphQL.Types;

namespace GodelTech.GraphQL.Web.GraphQL.GraphType.PropertyNote
{
    public sealed class CreateOrUpdatePropertyNoteInputType : InputObjectGraphType<PropertyNoteInput>
    {
        public CreateOrUpdatePropertyNoteInputType()
        {
            Name = "CreateOrUpdatePropertyNoteInput";
            Field(_ => _.PropertyId, type: typeof(StringGraphType));
            Field(_ => _.Note, type: typeof(StringGraphType));
        }
    }
}