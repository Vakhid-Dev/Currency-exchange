using CurenncyExchange.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace CurenncyExchange.Transaction.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransactionController : ControllerBase
    {
        
        public bool Execute(AccountDetails accountDetails)
        {
          
            return false;
        }
    }
}
