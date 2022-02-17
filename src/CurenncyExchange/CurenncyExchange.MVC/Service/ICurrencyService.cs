using System.Xml.Linq;
using CurenncyExchange.Core;
namespace CurenncyExchange.MVC.Service
{
    public interface ICurrencyService
    {
        Task TryGetCurrencyDocument(CancellationToken cancellationToken);
        Task<Currency?> TryParseCurrencyDocument(XDocument? xDocument);
    }
}
