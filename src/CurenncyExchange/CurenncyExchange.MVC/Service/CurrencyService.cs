using System;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using CurenncyExchange.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting;

namespace CurenncyExchange.MVC.Service
{
    public class CurrencyService : BackgroundService, ICurrencyService
    {
        private IMemoryCache _memoryCash;
        private const string url= "https://www.cbr.ru/scripts/XML_daily.asp?";

        public CurrencyService(IMemoryCache memoryCache)
        {
            _memoryCash = memoryCache;
        }
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    await TryGetCurrencyDocument(stoppingToken);
                }
                catch (Exception)
                {

                    throw;
                }
                await Task.Delay(3600000);
            }
        }
        public async Task TryGetCurrencyDocument(CancellationToken cancellationToken)
        {
            try
            {
               
                using (var httpClient = new HttpClient ())
                {
                    Thread.CurrentThread.CurrentCulture = new CultureInfo("ru-RU");
                    Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
                    var response = await httpClient.GetAsync(url).ConfigureAwait(false);
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        var stream = await response.Content.ReadAsStreamAsync().ConfigureAwait(false);
                       
                        XDocument? feedXML = await XDocument.LoadAsync(stream, LoadOptions.None, cancellationToken);
                        Currency? currency =  await TryParseCurrencyDocument(feedXML);
                        if (currency != null)
                        {
                            Task.Run(async () =>
                            {
                                await Task.Delay(300);
                                return new JsonResult(currency);
                            });
                        }
                    }
                }
              
          
            }
            catch (Exception ex)
            {

                throw new ArgumentException(nameof (ex));
            }
        }
        public async Task<Currency?> TryParseCurrencyDocument(XDocument? xDocument)
        {
           
            var currency = new Currency();
            currency.USD = Convert.ToDecimal(xDocument?.Elements("ValCurs")
                                                     .Elements("Valute")
                                                     .FirstOrDefault(x => x.Element("NumCode").Value == "840")
                                                     .Elements("Value")
                                                     .FirstOrDefault().Value);
            currency.EURO = Convert.ToDecimal(xDocument?.Elements("ValCurs")
                                                      .Elements("Valute")
                                                      .FirstOrDefault(x => x.Element("NumCode").Value == "978")
                                                      .Elements("Value")
                                                      .FirstOrDefault().Value);
            _memoryCash.Set("key_currency", currency, TimeSpan.FromSeconds(1400));
            await Task.Delay(5000);
            return currency;
        } 

    

       
    }
}
