using CurenncyExchange.App.Service;
using CurenncyExchange.Core;
using Microsoft.AspNetCore.Mvc;

namespace CurenncyExchange.Transactions.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        //ToDo need to implement
       // [HttpPost(Name = "execute")]
        public Task Get(AccountDetails accountDetails)
        {
            _transactionService.ExecuteAsync(accountDetails);
            return Task.CompletedTask;
        }
    }
}
