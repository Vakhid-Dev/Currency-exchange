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
        private readonly ITransactionService _transactionService;

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
            return null ;
        }

        
        [HttpPost]
        public async Task<ActionResult<TransactionCurrency>> PostTransactionCurrency(TransactionCurrency transactionCurrency)
        {
          await _transactionService.ExecuteAsync(transactionCurrency);

            return CreatedAtAction("GetTransactionCurrency", new { id = transactionCurrency.Id }, transactionCurrency);
        }


    }
}
