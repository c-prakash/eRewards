using System.Text.Json;
using AdminPortal.Web.Application.Interfaces.Serialization.Options;

namespace AdminPortal.Web.Application.Serialization.Options
{
    public class SystemTextJsonOptions : IJsonSerializerOptions
    {
        public JsonSerializerOptions JsonSerializerOptions { get; } = new();
    }
}