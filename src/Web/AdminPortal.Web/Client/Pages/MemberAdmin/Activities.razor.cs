using System;
using Microsoft.AspNetCore.Components;
using MudBlazor;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using AdminPortal.Web.Shared.Constants.Permission;
using Microsoft.AspNetCore.Authorization;
using AdminPortal.Web.Shared.Models;

namespace AdminPortal.Web.Client.Pages.MemberAdmin
{
    public partial class Activities
    {
        [Parameter] public string Id { get; set; }

        private ClaimsPrincipal _currentUser;
        private bool _loaded;
        private IEnumerable<Activity> _pagedData;
        private MudTable<Activity> _table;
        private int _totalItems;
        private int _currentPage;
        private string _searchString = string.Empty;
        private bool _dense = false;
        private bool _striped = true;
        private bool _bordered = false;

        private bool _canCreateActivity;

        protected override async Task OnInitializedAsync()
        { 
            var accountNo = Id;
            _currentUser = await _authenticationManager.CurrentUser();
            _canCreateActivity = (await _authorizationService.AuthorizeAsync(_currentUser, Permissions.Activities.Create)).Succeeded;

            _loaded = true;


        }

        private async Task<TableData<Activity>> ServerReload(TableState state)
        {
            //if (!string.IsNullOrWhiteSpace(_searchString))
            //{
            //    state.Page = 0;
            //}
            await LoadData(state.Page, state.PageSize, state);
            return new TableData<Activity> { TotalItems = _totalItems, Items = _pagedData };
        }

        private async Task LoadData(int pageNumber, int pageSize, TableState state)
        {
            string[] orderings = null;
            //if (!string.IsNullOrEmpty(state.SortLabel))
            //{
            //    orderings = state.SortDirection != SortDirection.None ? new[] { $"{state.SortLabel} {state.SortDirection}" } : new[] { $"{state.SortLabel}" };
            //}

            
        }

    }
}
