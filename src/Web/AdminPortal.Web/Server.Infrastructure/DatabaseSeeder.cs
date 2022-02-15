using AdminPortal.Web.Application.Interfaces.Services;
using AdminPortal.Web.Infrastructure.Contexts;
using AdminPortal.Web.Infrastructure.Helpers;
using AdminPortal.Web.Infrastructure.Models.Identity;
using AdminPortal.Web.Shared.Constants.Permission;
using AdminPortal.Web.Shared.Constants.Role;
using AdminPortal.Web.Shared.Constants.User;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Localization;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace AdminPortal.Web.Infrastructure
{
    public class DatabaseSeeder : IDatabaseSeeder
    {
        private readonly ILogger<DatabaseSeeder> _logger;
        private readonly IStringLocalizer<DatabaseSeeder> _localizer;
        private readonly BlazorHeroContext _db;
        private readonly UserManager<BlazorHeroUser> _userManager;
        private readonly RoleManager<BlazorHeroRole> _roleManager;

        public DatabaseSeeder(
            UserManager<BlazorHeroUser> userManager,
            RoleManager<BlazorHeroRole> roleManager,
            BlazorHeroContext db,
            ILogger<DatabaseSeeder> logger,
            IStringLocalizer<DatabaseSeeder> localizer)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
            _logger = logger;
            _localizer = localizer;
        }

        public async Task Initialize()
        {
            await AddAdministrator();
            await AddBasicUser();
            _db.SaveChanges();
        }

        private async Task AddAdministrator()
        {
            //Check if Role Exists
            var adminRole = new BlazorHeroRole(RoleConstants.AdministratorRole, _localizer["Administrator role with full permissions"]);
            var adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
            if (adminRoleInDb == null)
            {
                await _roleManager.CreateAsync(adminRole);
                adminRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.AdministratorRole);
                _logger.LogInformation(_localizer["Seeded Administrator Role."]);
            }
            //Check if User Exists
            var superUser = new BlazorHeroUser
            {
                FirstName = "Chandra",
                LastName = "Prakash",
                Email = "cprakash@ezloyalty.com",
                UserName = "cprakash",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            var superUserInDb = await _userManager.FindByEmailAsync(superUser.Email);
            if (superUserInDb == null)
            {
                await _userManager.CreateAsync(superUser, UserConstants.DefaultPassword);
                var result = await _userManager.AddToRoleAsync(superUser, RoleConstants.AdministratorRole);
                if (result.Succeeded)
                {
                    _logger.LogInformation(_localizer["Seeded Default SuperAdmin User."]);
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        _logger.LogError(error.Description);
                    }
                }
            }
            foreach (var permission in Permissions.GetRegisteredPermissions())
            {
                await _roleManager.AddPermissionClaim(adminRoleInDb, permission);
            }
        }

        private async Task AddBasicUser()
        {
            //Check if Role Exists
            var basicRole = new BlazorHeroRole(RoleConstants.BasicRole, _localizer["Basic role with default permissions"]);
            var basicRoleInDb = await _roleManager.FindByNameAsync(RoleConstants.BasicRole);
            if (basicRoleInDb == null)
            {
                await _roleManager.CreateAsync(basicRole);
                _logger.LogInformation(_localizer["Seeded Basic Role."]);
            }
            //Check if User Exists
            var basicUser = new BlazorHeroUser
            {
                FirstName = "John",
                LastName = "Doe",
                Email = "john@ezloyalty.com",
                UserName = "johndoe",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true,
                CreatedOn = DateTime.Now,
                IsActive = true
            };
            var basicUserInDb = await _userManager.FindByEmailAsync(basicUser.Email);
            if (basicUserInDb == null)
            {
                await _userManager.CreateAsync(basicUser, UserConstants.DefaultPassword);
                await _userManager.AddToRoleAsync(basicUser, RoleConstants.BasicRole);
                _logger.LogInformation(_localizer["Seeded User with Basic Role."]);
            }
        }
    }
}