using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eRewards.Services.Accounts.Domain.AccountsAggregate;

namespace eRewards.Services.Accounts.API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<AccountsController> _logger;

       /// <summary>
       /// 
       /// </summary>
       /// <param name="accountRepository"></param>
       /// <param name="logger"></param>
        public AccountsController(
            IAccountRepository accountRepository,
            ILogger<AccountsController> logger)
        {
            _accountRepository = accountRepository ?? throw new ArgumentNullException(nameof(accountRepository));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> CreateAccount([FromBody] Account account)
        {
            var innerAccount = new Account
            {
                Mappings = account.Mappings
            };

            var item = await _accountRepository.Add(innerAccount);

            return CreatedAtAction(nameof(GetAccount), new { id = item.Id }, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        [Route("{accountNo:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<Account>> GetAccount(int accountNo)
        {
            try
            {
                var account = await _accountRepository.GetAsync(accountNo);

                return Ok(account);
            }
            catch
            {
                return NotFound();
            }
        }

    }
}
