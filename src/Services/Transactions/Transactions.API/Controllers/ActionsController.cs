using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.eShopOnContainers.BuildingBlocks.EventBus.Extensions;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ezLoyalty.Services.Actions.API.Application.Commands;
using ezLoyalty.Services.Actions.API.Application.Queries;
using ezLoyalty.Services.Actions.Domain.ActionsAggregate;

namespace ezLoyalty.Services.Actions.API.Controllers
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
            _mediator = mediator ?? throw new System.ArgumentNullException(nameof(mediator));
            _actionsQueries = actionsQueries ?? throw new System.ArgumentNullException(nameof(actionsQueries));
            _logger = logger ?? throw new System.ArgumentNullException(nameof(logger));
        }

        //actions

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

        [Route("account/{accountNo:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Action>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Action>>> GetActionsForAccountAsync(int accountNo)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var actions = await _actionsQueries.GetActionsForAccountAsync(accountNo);

                return Ok(actions);
            }
            catch
            {
                return NotFound();
            }
        }


        [Route("{actionId:int}/account/{accountNo:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Action>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Action>>> GetActionsForAccountAsync(int accountNo, int actionId)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var actions = await _actionsQueries.GetActionsForAccountAsync(accountNo, actionId);

                return Ok(actions);
            }
            catch
            {
                return NotFound();
            }
        }

        [Route("{actionId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(ActionMetadata), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ActionMetadata>>> GetActionMetadata(int actionId)
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var actionsMetadata = await _actionsQueries.GetActionMetadatAsync(actionId);

                return Ok(actionsMetadata);
            }
            catch
            {
                return NotFound();
            }
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ActionMetadata>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<ActionMetadata>>> GetActions()
        {
            try
            {
                //Todo: It's good idea to take advantage of GetOrderByIdQuery and handle by GetCustomerByIdQueryHandler
                //var order customer = await _mediator.Send(new GetOrderByIdQuery(orderId));
                var actions = await _actionsQueries.GetAllActionMetadataAsync();

                return Ok(actions);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
