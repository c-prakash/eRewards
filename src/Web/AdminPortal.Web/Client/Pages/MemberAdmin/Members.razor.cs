using AdminPortal.Web.Application.Responses.Identity;
using MudBlazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPortal.Web.Shared.Constants.Application;
using AdminPortal.Web.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using Microsoft.JSInterop;
using AdminPortal.Web.Shared.Models;
using AdminPortal.Web.Client.Infrastructure.Managers.MemberAdmin;
using Microsoft.AspNetCore.Components;
using AdminPortal.Web.Client.Pages.Catalog;
using AdminPortal.Web.Application.Features.Products.Commands.AddEdit;

namespace AdminPortal.Web.Client.Pages.MemberAdmin
{
    public partial class Members
    {
        [Inject] private IMemberAdminManager MemberAdminManager { get; set; }

        private IEnumerable<Member> _pagedData;
        private MudTable<Member> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = string.Empty;
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private ClaimsPrincipal _currentUser;
        private bool _canCreateMembers;
        private bool _canSearchMembers;
        private bool _canExportMembers;
        private bool _canEditMembers;
        private bool _canDeleteMembers;
        private bool _canViewMembers;

        private bool _loaded;

        protected override async Task OnInitializedAsync()
        {
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateMembers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Members.Create)).Succeeded;
            _canSearchMembers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Members.Search)).Succeeded;
            _canExportMembers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Members.Export)).Succeeded;
            _canEditMembers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Members.Edit)).Succeeded;
            _canDeleteMembers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Members.Delete)).Succeeded;
            _canViewMembers = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Members.View)).Succeeded;

            //await GetUsersAsync();
            _loaded = true;
        }

        private async Task<TableData<Member>> ServerReload(TableState state)
        {
            //if (!string.IsNullOrWhiteSpace(_searchString))
            //{
            //    state.Page = 0;
            //}
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<Member> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            //if (!string.IsNullOrEmpty(state.SortLabel))
            //{
            //    orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            //}

            try
            {
                var response = await MemberAdminManager.GetAllMembers(pageNumber , pageSize, _searchString, orderings);
                if (response != null && response.Data!=null && response.Data.Any())
                {
                    _totalItems = response.Count;
                    _currentPage = response.PageIndex;
                    _pagedData = response.Data;
                }
            }
            catch(Exception ex)
            {
                _snackBar.Add(ex.Message, Severity.Error);
            }
        }

        private void OnSearch(string text)
        {
            _searchString = text;
            _table.ReloadServerData();
        }

        private async Task InvokeModal(int id = 0)
        {
            var parameters = new DialogParameters();
            if (id != 0)
            {
                var member = _pagedData.FirstOrDefault(c => c.Id == id);
                if (member != null)
                {
                    parameters.Add(nameof(AddEditMemberModal.AddEditMemberModel), new Member
                    {
                        Id = member.Id,
                        CustomerId = member.CustomerId,
                        CreatedAt = member.CreatedAt,
                        UpdatedAt = DateTime.Now
                    });
                }
            }
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Medium, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<AddEditMemberModal>(id == 0 ? _localizer["Create"] : _localizer["Edit"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                OnSearch("");
            }
        }

        private async Task Delete(int id)
        {
            string deleteContent = _localizer["Delete Content"];
            var parameters = new DialogParameters
            {
                {nameof(Shared.Dialogs.DeleteConfirmation.ContentText), string.Format(deleteContent, id)}
            };
            var options = new DialogOptions { CloseButton = true, MaxWidth = MaxWidth.Small, FullWidth = true, DisableBackdropClick = true };
            var dialog = _dialogService.Show<Shared.Dialogs.DeleteConfirmation>(_localizer["Delete"], parameters, options);
            var result = await dialog.Result;
            if (!result.Cancelled)
            {
                var response = await MemberAdminManager.DeleteAsync(id);
                if (response.Succeeded)
                {
                    OnSearch("");
                    _snackBar.Add(response.Messages[0], Severity.Success);
                }
                else
                {
                    OnSearch("");
                    foreach (var message in response.Messages)
                    {
                        _snackBar.Add(message, Severity.Error);
                    }
                }
            }
        }
    }
}
