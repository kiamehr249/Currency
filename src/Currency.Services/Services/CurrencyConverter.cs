using System;
using System.Collections.Generic;
using System.Linq;

namespace Currency.Services
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private IEnumerable<Tuple<string, string, double>> _conversionRates;
        private readonly IGraphProcess _iGraphProcess;

        public CurrencyConverter(IGraphProcess iGraphProcess)
        {
            _conversionRates = new List<Tuple<string, string, double>>();
            _iGraphProcess = iGraphProcess;
        }

        public void ClearConfiguration()
        {
            _conversionRates = new List<Tuple<string, string, double>>();
        }

        public void UpdateConfiguration(IEnumerable<Tuple<string, string, double>> conversionRates)
        {
            _conversionRates = conversionRates;
        }

        public double Convert(string fromCurrency, string toCurrency, double amount)
        {
            if(string.IsNullOrEmpty(fromCurrency) || string.IsNullOrEmpty(toCurrency))
            {
                return 0;
            }

            fromCurrency = fromCurrency.ToUpper();
            toCurrency = toCurrency.ToUpper();

            //Get Ready Vertices
            var vertices = new List<string>();
            var list1 = _conversionRates.Select(x => x.Item1).ToList();
            var list2 = _conversionRates.Select(x => x.Item2).ToList();
            vertices = list1.Concat(list2).Distinct().ToList();

            //Graph Generation
            var graph = new Graph(vertices, _conversionRates);

            //Find short path between currencies
            var shortestPath = _iGraphProcess.ShortestPathFunc(graph, fromCurrency);
            var path = shortestPath(toCurrency).ToList();

            //Final Exchange Rate Conversion
            double rate = 1;
            for(var i = 0; i < (path.Count -1); i++)
            {
                Tuple<string, string, double> node = null;
                node = _conversionRates.Where(x => x.Item1 == path[i] && x.Item2 == path[i + 1]).FirstOrDefault();
                if (node == null)
                {
                    node = _conversionRates.Where(x => x.Item1 == path[i + 1] && x.Item2 == path[i]).FirstOrDefault();
                    rate = rate / node.Item3;
                }
                else
                {
                    rate = node.Item3 * rate;
                }
                
            }

            return rate * amount;
        }

        public IEnumerable<Tuple<string, string, double>> GetReadyList()
        {
            return new List<Tuple<string, string, double>>
            {
                Tuple.Create("USD", "CAD", 1.34),
                Tuple.Create("CAD", "GBP", 0.58),
                Tuple.Create("USD", "EUR", 0.86),
                Tuple.Create("CAD", "RIR", 33561.29),
                Tuple.Create("EUR", "RUB", 65.46)
            };
        }
    }
}
