using CurenncyExchange.Core;
using CurenncyExchange.MVC.Models;
using CurenncyExchange.Transaction.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System.Diagnostics;

namespace CurenncyExchange.MVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IMemoryCache _memoryCache;
        private static List<CurrencyDetails> CurrencyDetailsList = new List<CurrencyDetails>();


        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
        {
            _logger = logger;
            _memoryCache = memoryCache;
        }

        public IActionResult Index()
        {
            if (!_memoryCache.TryGetValue("key_currency", out CurrencyViewModel currency))
            {
                // ToDo log here
                throw new Exception("Cannot read from_memoryCache ");
            }
            ViewBag.USD = currency.USD;
            ViewBag.EURO = currency.EURO;
            return View(currency);
        }
        [HttpPost]
        public async Task<IActionResult> BuyCurrencyAsync(TransactionCurrency transactionCurrency)
        {
            using (var client = new HttpClient())
            {
                transactionCurrency.CurrencyDetails = CurrencyDetailsList.LastOrDefault();
                HttpRequestMessage message = new HttpRequestMessage()
                {
                    // Вынести url в конфиг файл или в константу
                    RequestUri = new Uri("https://localhost:7011/api/transactioncurrencies"),
                    Method = HttpMethod.Post,
                    Content = JsonContent.Create(transactionCurrency)

                };

                HttpResponseMessage? res = await client.PostAsync(message.RequestUri,
                    message.Content);
                if (res.StatusCode == System.Net.HttpStatusCode.OK)
                {
                    return RedirectToAction("Index");

                }
            }
            return Ok("Succes");

        }
        [HttpGet]
        public IActionResult Transaction([FromQuery(Name = "type")] int type, [FromQuery(Name = "value")] decimal value)
        {

            ViewBag.CurrencyType = new { name = "Dollar", value = value, type = 1 };
            CurrencyDetails currencyDetails = new CurrencyDetails()
            {
                Rate = ViewBag.CurrencyType.value,
                Ammount = 1000,
                CurrencyType = CurrencyType.USD
            };
            switch (type)
            {

                case 2:
                    ViewBag.CurrencyType = new { name = "Euro", value = value, type = 2 };
                    currencyDetails.CurrencyType = CurrencyType.EURO;
                    break;
                default:

                    break;
            }

            CurrencyDetailsList.Add(currencyDetails);

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