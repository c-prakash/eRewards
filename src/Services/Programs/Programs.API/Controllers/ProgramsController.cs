using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using ezloyalty.Services.Programs.Domain.ProgramsAggregate;

namespace ezloyalty.Services.Programs.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ProgramsController : ControllerBase
    {
        private readonly IProgramRepository _programRepository;
        private readonly ILogger<ProgramsController> _logger;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="programRepository"></param>
       /// <param name="logger"></param>
        public ProgramsController(
            IProgramRepository programRepository,
            ILogger<ProgramsController> logger)
        {
            _programRepository = programRepository ?? throw new ArgumentNullException(nameof(programRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="program"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateProgram([FromBody] Domain.ProgramsAggregate.Program program)
        {
            var innerProgram = new Domain.ProgramsAggregate.Program
            {
            };

            var item = await _programRepository.Add(innerProgram);

            return CreatedAtAction(nameof(GetProgram), new { id = item.Id }, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="programId"></param>
        /// <returns></returns>
        [Route("{programId:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Domain.ProgramsAggregate.Program>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Domain.ProgramsAggregate.Program>> GetProgram(int programId)
        {
            try
            {
                var program = await _programRepository.GetAsync(programId);

                return Ok(program);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
