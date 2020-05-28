using System;

namespace Presentations.Api.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, Inherited = true, AllowMultiple = false)]
    public class CommaSeparatedAttribute : Attribute
    {
    }
}