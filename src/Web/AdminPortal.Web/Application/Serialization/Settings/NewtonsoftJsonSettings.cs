
using AdminPortal.Web.Application.Interfaces.Serialization.Settings;
using Newtonsoft.Json;

namespace AdminPortal.Web.Application.Serialization.Settings
{
    public class NewtonsoftJsonSettings : IJsonSerializerSettings
    {
        public JsonSerializerSettings JsonSerializerSettings { get; } = new();
    }
}