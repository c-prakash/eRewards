﻿using MediatR;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;

namespace ezLoyalty.Services.Incentive.API.Application.Behavior
{
    public class LoggingBehavior<TRequest, TResponse> 
        : IPipelineBehavior<TRequest, TResponse>
         where TRequest : MediatR.IRequest<TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;
        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger) => _logger = logger;

        public async Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken, RequestHandlerDelegate<TResponse> next)
        {
            _logger.LogInformation("----- Handling command {CommandName} ({@Command})", request.GetGenericTypeName(), request);
            var response = await next();
            _logger.LogInformation("----- Command {CommandName} handled - response: {@Response}", request.GetGenericTypeName(), response);

            return response;
        }
    }
}
