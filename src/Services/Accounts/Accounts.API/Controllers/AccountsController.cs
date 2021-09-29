using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using eRewards.Services.Accounts.Domain.AccountsAggregate;
using eRewards.Services.Accounts.API.ViewModel;

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
            
            var item = await _accountRepository.Add(account);

            return CreatedAtAction(nameof(GetAccount), new { id = item.Id }, null);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [HttpPut]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> UpdateAccount([FromBody] Account account)
        {

            await _accountRepository.Update(account);

            return Ok(account);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [Route("")]
        [HttpGet]
        [ProducesResponseType(typeof(PaginatedItemsViewModel<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType(typeof(IEnumerable<Account>), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<IEnumerable<Account>>> GetAccounts([FromQuery] string searchString, [FromQuery] string orderBy, [FromQuery] int pageNumber = 0, [FromQuery] int pageSize = 10)
        {
            try
            {
                var totalAccounts = await _accountRepository.Count(searchString);
                var accounts = await _accountRepository.GetAllAsync(searchString, orderBy, pageNumber, pageSize);

                return Ok(new PaginatedItemsViewModel<Account>(pageNumber, pageSize, totalAccounts, accounts));
            }
            catch
            {
                return NotFound();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        [Route("{accountNo:int}")]
        [HttpGet]
        [ProducesResponseType(typeof(Account), (int)HttpStatusCode.OK)]
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

        /// <summary>
        /// 
        /// </summary>
        /// <param name="accountNo"></param>
        /// <returns></returns>
        [Route("{accountNo:int}")]
        [HttpDelete]
        [ProducesResponseType(typeof(int), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<ActionResult<int>> DeleteAccount(int accountNo)
        {
            try
            {
                var result = await _accountRepository.DeleteAsync(accountNo);

                return Ok(result);
            }
            catch
            {
                return NotFound();
            }
        }
    }
}
