using Currency.Services;
using Currency.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Currency.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ICurrencyConverter _iCurrencyConverter;
        public HomeController(ILogger<HomeController> logger, ICurrencyConverter iCurrencyConverter)
        {
            _logger = logger;
            _iCurrencyConverter = iCurrencyConverter;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.Content = 0;
            var request = new ConversionRequest();
            request.HasError = false;
            return View(request);
        }

        [HttpPost]
        public IActionResult Index(ConversionRequest request)
        {
            var list = _iCurrencyConverter.GetReadyList();
            _iCurrencyConverter.UpdateConfiguration(list);
            var amount = _iCurrencyConverter.Convert(request.From, request.To, request.Amount);
            if(amount == 0)
            {
                request.HasError = true;
            }
            ViewBag.Content = amount;
            return View(request);
        }

        public IActionResult Privacy()
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
