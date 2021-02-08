using System;
using Microsoft.Extensions.DependencyInjection;
using GraphQLSchema = GraphQL.Types.Schema;

namespace GodelTech.GraphQL.Web.GraphQL
{
    public class Schema : GraphQLSchema
    {
        public Schema(IServiceProvider provider)
            : base(provider)
        {
            Query = provider.GetRequiredService<PropertiesQuery>();
            Mutation = provider.GetRequiredService<PropertiesMutation>();
        }
    }
}