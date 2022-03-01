#nullable disable
using Microsoft.AspNetCore.Mvc;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.App.Service;

namespace CurenncyExchange.Transactions.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCurrenciesController : ControllerBase
    {
        private ITransactionService _transactionService;

        public TransactionCurrenciesController(ITransactionService transactionService)
        {
            _transactionService = transactionService;
        }

        // GET: api/TransactionCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionCurrency>>> GetTransactionDetails()
        {
            return null;
        }

        // GET: api/TransactionCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionCurrency>> GetTransactionCurrency(Guid id)
        {
            return null;
        }


        [HttpPost]
        public async Task<IActionResult> PostTransactionCurrencyAsync(TransactionCurrency transactionCurrency)
        {
            if (!ModelState.IsValid)
            {
                // log need
                 throw new NullReferenceException(nameof(PostTransactionCurrencyAsync));

            }
            await _transactionService.BuyingCurrencyAsync(transactionCurrency).ConfigureAwait(false);

            return Ok("Сurrency bying was successful");
        }


    }
}
