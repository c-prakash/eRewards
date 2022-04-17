using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using System;

namespace ezLoyalty.Services.Incentive.API.Extensions
{
    public class RoleNameInitializer : ITelemetryInitializer
    {
        private readonly string _roleName;

        /// <summary>Construct an initializer with a role name.</summary>
        /// <param name="roleName">Cloud role name to assign to telemetry's context.</param>
        public RoleNameInitializer(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                throw new ArgumentNullException(nameof(roleName));
            }

            _roleName = roleName;
        }

        void ITelemetryInitializer.Initialize(ITelemetry telemetry)
        {
            telemetry.Context.Cloud.RoleName = _roleName;
        }
    }
}
