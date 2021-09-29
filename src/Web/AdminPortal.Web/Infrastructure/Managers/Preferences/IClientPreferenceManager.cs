using AdminPortal.Web.Shared.Managers;
using MudBlazor;
using System.Threading.Tasks;

namespace AdminPortal.Web.Client.Infrastructure.Managers.Preferences
{
    public interface IClientPreferenceManager : IPreferenceManager
    {
        Task<MudTheme> GetCurrentThemeAsync();

        Task<bool> ToggleDarkModeAsync();
    }
}