using AdminPortal.Web.Client.Infrastructure.Managers.MemberAdmin;
using AdminPortal.Web.Shared.Models;
using Blazored.FluentValidation;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System;
using System.Threading.Tasks;

namespace AdminPortal.Web.Client.Pages.MemberAdmin
{
    public partial class AddEditMemberModal
    {
        [Inject] private IMemberAdminManager MemberAdminManager { get; set; }

        [Parameter] public Member AddEditMemberModel { get; set; } = new();
        [CascadingParameter] private MudDialogInstance MudDialog { get; set; }

        private FluentValidationValidator _fluentValidationValidator;
        private bool Validated => _fluentValidationValidator.Validate(options => { options.IncludeAllRuleSets(); });

        public void Cancel()
        {
            MudDialog.Cancel();
        }

        private async Task SaveAsync()
        {
            try
            {
                if (AddEditMemberModel.Id == 0)
                {
                    AddEditMemberModel.CreatedAt = DateTime.Now;
                    await MemberAdminManager.AddMember(AddEditMemberModel);
                }
                else
                {
                    await MemberAdminManager.UpdateMember(AddEditMemberModel);
                }
                _snackBar.Add("Member details updated successfully.", Severity.Success);
                MudDialog.Close();
            }
            catch (Exception ex)
            {
                _snackBar.Add(ex.Message, Severity.Error);
            }
        }
    }
}