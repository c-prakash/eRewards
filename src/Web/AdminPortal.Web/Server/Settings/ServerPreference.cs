using System.Linq;
using AdminPortal.Web.Shared.Constants.Localization;
using AdminPortal.Web.Shared.Settings;

namespace AdminPortal.Web.Server.Settings
{
    public record ServerPreference : IPreference
    {
        public string LanguageCode { get; set; } = LocalizationConstants.SupportedLanguages.FirstOrDefault()?.Code ?? "en-US";

        //TODO - add server preferences
    }
}