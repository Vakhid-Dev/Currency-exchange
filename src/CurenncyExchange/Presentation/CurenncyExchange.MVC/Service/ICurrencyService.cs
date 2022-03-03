using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using CurenncyExchange.Core;
using CurenncyExchange.MVC.Models;

namespace CurenncyExchange.MVC.Service
{
    public interface ICurrencyService
    {
        Task TryGetCurrencyDocument(CancellationToken cancellationToken);
        Task<CurrencyViewModel?> TryParseCurrencyDocument(XDocument? xDocument);
    }
}
