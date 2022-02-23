#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CurenncyExchange.Data.Context;
using CurenncyExchange.Transaction.Core;

namespace CurenncyExchange.Transactions.Web.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionCurrenciesController : ControllerBase
    {
        private readonly TransactionContext _context;

        public TransactionCurrenciesController(TransactionContext context)
        {
            _context = context;
        }

        // GET: api/TransactionCurrencies
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TransactionCurrency>>> GetTransactionDetails()
        {
            return await _context.TransactionDetails.ToListAsync();
        }

        // GET: api/TransactionCurrencies/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TransactionCurrency>> GetTransactionCurrency(Guid id)
        {
            var transactionCurrency = await _context.TransactionDetails.FindAsync(id);

            if (transactionCurrency == null)
            {
                return NotFound();
            }

            return transactionCurrency;
        }

        // PUT: api/TransactionCurrencies/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTransactionCurrency(Guid id, TransactionCurrency transactionCurrency)
        {
            if (id != transactionCurrency.Id)
            {
                return BadRequest();
            }

            _context.Entry(transactionCurrency).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TransactionCurrencyExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<TransactionCurrency>> PostTransactionCurrency(TransactionCurrency transactionCurrency)
        {
            _context.TransactionDetails.Add(transactionCurrency);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTransactionCurrency", new { id = transactionCurrency.Id }, transactionCurrency);
        }

        // DELETE: api/TransactionCurrencies/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTransactionCurrency(Guid id)
        {
            var transactionCurrency = await _context.TransactionDetails.FindAsync(id);
            if (transactionCurrency == null)
            {
                return NotFound();
            }

            _context.TransactionDetails.Remove(transactionCurrency);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool TransactionCurrencyExists(Guid id)
        {
            return _context.TransactionDetails.Any(e => e.Id == id);
        }
    }
}
