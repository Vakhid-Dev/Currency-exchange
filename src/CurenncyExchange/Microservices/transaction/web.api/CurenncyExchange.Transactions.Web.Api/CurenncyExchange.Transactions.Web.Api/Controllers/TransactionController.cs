using CurenncyExchange.App;
using CurenncyExchange.App.Service;
using CurenncyExchange.Transaction.Core;
using Microsoft.AspNetCore.Http;
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
        [HttpPost]
        public async Task<IActionResult> ByCurrencyAsync(ByCurrencyRequest byCurrencyRequest)
        {
            if (!ModelState.IsValid)
            {
                // log need
                throw new NullReferenceException(nameof(ByCurrencyAsync));

            }
           await _transactionService.BuyingCurrencyAsync(byCurrencyRequest);

            return Ok();
        }
    }
}
