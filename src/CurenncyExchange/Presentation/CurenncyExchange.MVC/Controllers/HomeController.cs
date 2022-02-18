using CurenncyExchange.Core;
using CurenncyExchange.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;
using System.Text.Json;

namespace CurenncyExchange.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;


        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue("key_currency", out Currency currency))
            {
                // ToDo log here
                throw new Exception("Cannot read from_memoryCache ");
            }
            ViewBag.USD = currency.USD;
            ViewBag.EURO = currency.EURO;
            return View(currency);
        }
        [HttpPost]
        public async Task<IActionResult> Transaction(AccountDetails accountDetails)
        {
            using (var client = new HttpClient ())
            {
                accountDetails.Ammount = 1000;
                var ser = JsonSerializer.Serialize(accountDetails); 
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    RequestUri = new Uri("https://localhost:7287/api/transaction"),
                    Method = HttpMethod.Post,
                    Content = JsonContent.Create(accountDetails)

            };
                HttpResponseMessage? res = await client.SendAsync(message);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {

                }
            }
            return View();
        }
        [HttpGet]
        public IActionResult Transaction([FromQuery(Name = "type")] int type, [FromQuery(Name = "value")] decimal value)
        {


            ViewBag.CurrencyType = new { name = "Dollar", value = value,type = 1 };
            switch (type)
            {

                case 2:
                    ViewBag.CurrencyType = new { name = "Euro", value = value , type = 2};
                    break;
                default:

                    break;
            }
            return View();
        }
        public IActionResult Converter()
        {

            return View();
        }
      

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}