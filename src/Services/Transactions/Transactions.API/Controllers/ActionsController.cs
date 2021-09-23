using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eRewards.Services.Transactions.Domain.ActionsAggregate;
using eRewards.Services.Transactions.API.Application.Queries;
using eRewards.Services.Transactions.API.Application.Commands;

namespace eRewards.Services.Transactions.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ActionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IActionsQueries _actionsQueries;
        private readonly ILogger<ActionsController> _logger;

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="mediator"></param>
        /// <param name="logger"></param>
        public ActionsController(
            IMediator mediator,
            IActionsQueries actionsQueries,
            ILogger<ActionsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _actionsQueries = actionsQueries ?? throw new ArgumentNullException(nameof(actionsQueries));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// RecordAction
        /// Receive, record, validate the action
        /// </summary>
        /// <param name="actionsCommand"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.BadRequest)]
        public async Task<IActionResult> RecordAction([FromBody] NewActionCommand actionsCommand)
        {
            bool commandResult = false;

            _logger.LogInformation(
               "----- Sending command: {CommandName} - {sender}: {UniqueToken} ({@Command})",
               actionsCommand.GetGenericTypeName(),
               nameof(actionsCommand.Sender),
               actionsCommand.UniqueToken,
               actionsCommand);

            commandResult = await _mediator.Send(actionsCommand);

            if (!commandResult)
            {
                return BadRequest();
            }

            return Ok();
        }

        [Route("{accountNo:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Actions>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Actions>>> GetActionsAsync(int accountNo)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var actions = await _actionsQueries.GetActionsAsync(accountNo);

                return Ok(actions);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
