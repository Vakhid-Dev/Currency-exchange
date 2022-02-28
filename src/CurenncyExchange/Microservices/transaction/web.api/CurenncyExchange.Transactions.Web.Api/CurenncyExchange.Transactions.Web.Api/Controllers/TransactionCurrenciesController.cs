#nullable disable
using Microsoft.AspNetCore.Mvc;
using CurenncyExchange.Transaction.Core;
using CurenncyExchange.App.Service;
using CurenncyExchange.Transaction.Core.Repository;

namespace CurenncyExchange.Transactions.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCurrenciesController : ControllerBase
    {
        private ITransactionRepository _transactionRepository;

        public TransactionCurrenciesController(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
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
        public IActionResult PostTransactionCurrency(TransactionCurrency transactionCurrency)
        {
           _transactionRepository.SendMessage(transactionCurrency);

            return Ok($"SENDET {transactionCurrency}");
        }


    }
}
