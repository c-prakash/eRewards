using ezLoyalty.Services.Incentive.API.Application.Commands;
using ezLoyalty.Services.Incentive.Domain.PointsAggregate;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Net;

namespace ezLoyalty.Services.Incentive.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PointsController : ControllerBase
    {

        private readonly IMediator _mediator;
        private readonly ILogger<PointsController> _logger;

        public PointsController(IMediator mediator,
            ILogger<PointsController> logger)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [Route("{accountNo:int}")]
        [HttpGet]
        [SwaggerOperation(Summary = "Get points to the Account", Description = "Get points to the Account",
                OperationId = "Points.GetPoints", Tags = new[] { "Earn & Burn" })]
        [ProducesResponseType(typeof(IEnumerable<Points>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPoints(int accountNo)
        {
            var result = await _mediator.Send(new GetPointsCommand() { AccountNo= accountNo});
            return Ok(result);

        }

        [HttpGet]
        [SwaggerOperation(Summary = "Get points to the Account", Description = "Get points to the Account",
                OperationId = "Points.GetPoints", Tags = new[] { "Earn & Burn" })]
        [ProducesResponseType(typeof(IEnumerable<Points>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> GetPoints([FromQuery] GetPointsCommand getPointsCommand)
        {
            var result = await _mediator.Send(getPointsCommand);
            return Ok(result);

        }

        [Route("AddPoints")]
        [HttpPut]
        [SwaggerOperation(Summary = "Add points to the Account", Description = "Add points to the Account",
            OperationId = "Points.AddPoints", Tags = new[] { "Earn & Burn" })
        ]
        public async Task<IActionResult> AddPoints([FromBody] AddPointsCommand addPointsCommand)
        {
            var result = await _mediator.Send(addPointsCommand);
            return Ok(result);

        }

        [Route("RedeemPoints")]
        [HttpPut]
        [SwaggerOperation(Summary = "Redeem points to the Account", Description = "Redeem points to the Account",
            OperationId = "Points.RedeemPoints", Tags = new[] { "Earn & Burn" })
        ]
        public async Task<IActionResult> RedeemPoints([FromBody] RedeemPointsCommand redeemPointsCommand)
        {
            var result = await _mediator.Send(redeemPointsCommand);
            return Ok(result);

        }
    }

    
}
