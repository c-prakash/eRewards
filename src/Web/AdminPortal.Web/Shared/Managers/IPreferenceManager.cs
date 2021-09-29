using AdminPortal.Web.Shared.Settings;
using System.Threading.Tasks;
using AdminPortal.Web.Shared.Wrapper;

namespace AdminPortal.Web.Shared.Managers
{
    public interface IPreferenceManager
    {
        Task SetPreference(IPreference preference);

        Task<IPreference> GetPreference();

        Task<IResult> ChangeLanguageAsync(string languageCode);
    }
}