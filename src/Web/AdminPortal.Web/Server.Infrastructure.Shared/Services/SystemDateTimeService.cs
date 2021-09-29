using AdminPortal.Web.Application.Interfaces.Services;
using System;

namespace AdminPortal.Web.Infrastructure.Shared.Services
{
    public class SystemDateTimeService : IDateTimeService
    {
        public DateTime NowUtc => DateTime.UtcNow;
    }
}