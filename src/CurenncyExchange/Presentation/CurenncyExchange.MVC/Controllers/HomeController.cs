//using CurenncyExchange.Core;
//using CurenncyExchange.MVC.Models;
//using CurenncyExchange.Transaction.Core;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.Extensions.Caching.Memory;
//using System.Diagnostics;
//using System.Text.Json;

//namespace CurenncyExchange.MVC.Controllers
//{
//    public class HomeController : Controller
//    {
//        private readonly ILogger<HomeController> _logger;
//        private readonly IMemoryCache _memoryCache;


//        public HomeController(ILogger<HomeController> logger, IMemoryCache memoryCache)
//        {
//            _logger = logger;
//            _memoryCache = memoryCache;
//        }

//        public IActionResult Index()
//        {
//            if (!_memoryCache.TryGetValue("key_currency", out Currency currency))
//            {
//                // ToDo log here
//                throw new Exception("Cannot read from_memoryCache ");
//            }
//            ViewBag.USD = currency.USD;
//            ViewBag.EURO = currency.EURO;
//            return View(currency);
//        }
//        [HttpPost]
//        public async Task<IActionResult> Transaction(TransactionCurrency transactionCurrency)
//        {
//            using (var client = new HttpClient ())
//            {
//                transactionCurrency.AccountDetails = new AccountDetails()
//                {
//                    Ammount = 100,
//                    CurrencyType = "USD",
//                    Rate = 80
                    
//                };
//                transactionCurrency.Id = new Guid();
//                var ser = JsonSerializer.Serialize(transactionCurrency); 
//                HttpRequestMessage message = new HttpRequestMessage()
//                {
//                    RequestUri = new Uri("https://localhost:7011/api/transactioncurrencies"),
//                    Method = HttpMethod.Post,
//                    Content = JsonContent.Create(transactionCurrency)

//            };
               
//                HttpResponseMessage? res = await client.PostAsync(message.RequestUri,
//                    message.Content);
//                if (res.StatusCode == System.Net.HttpStatusCode.OK)
//                {
//                    return RedirectToAction("Index");
                    
//                }
//            }
//            return Ok("Succes");

//        }
//        [HttpGet]
//        public IActionResult Transaction([FromQuery(Name = "type")] int type, [FromQuery(Name = "value")] decimal value)
//        {


//            ViewBag.CurrencyType = new { name = "Dollar", value = value,type = 1 };
//            switch (type)
//            {

//                case 2:
//                    ViewBag.CurrencyType = new { name = "Euro", value = value , type = 2};
//                    break;
//                default:

//                    break;
//            }
//            return View();
//        }
//        public IActionResult Converter()
//        {

//            return View();
//        }
      

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}