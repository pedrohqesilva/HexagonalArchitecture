using Microsoft.AspNetCore.Mvc.ApplicationModels;
using Presentations.Api.Attributes;
using System.Linq;

namespace Presentations.Api.Conventions
{
    public class CommaSeparatedRouteConvention : IActionModelConvention
    {
        public void Apply(ActionModel action)
        {
            SeparatedRouteAttribute attribute = null;
            foreach (var parameter in action.Parameters)
            {
                if (parameter.Attributes.OfType<CommaSeparatedAttribute>().Any())
                {
                    if (attribute == null)
                    {
                        attribute = new SeparatedRouteAttribute(",");
                        parameter.Action.Filters.Add(attribute);
                    }

                    attribute.AddKey(parameter.ParameterName);
                }
            }
        }
    }
}