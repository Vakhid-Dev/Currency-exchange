using CurenncyExchange.App.Service;
using CurenncyExchange.Core;
using Microsoft.AspNetCore.Mvc;

namespace CurenncyExchange.Transactions.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        public TransactionController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }
        //ToDo need to implement
       // [HttpPost(Name = "execute")]
       [HttpPost]
        public Task Init(AccountDetails accountDetails)
        {
            _transactionService.ExecuteAsync(accountDetails);
            return Task.CompletedTask;
        }
    }
}
