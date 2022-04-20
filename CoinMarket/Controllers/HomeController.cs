using System;
using System.Collections.Generic;
using CoinMarket.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using CoinMarket.Dal;
using CoinMarket.Dal.Interfaces;
using CoinMarket.Model;
using CoinMarket.Model.Entities;
using CoinMarket.Model.Models;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using RestSharp;
using JsonConverter = Newtonsoft.Json.JsonConverter;

namespace CoinMarket.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ICustomerRepository _customerRepository;

        private const string url = "https://pro-api.coinmarketcap.com/v1/cryptocurrency/quotes/latest?symbol=BNB";
        private const string apiKey = "0a26be34-a1bf-43c5-a89c-996c96a7cd2f";
        public HomeController(ILogger<HomeController> logger,
            MyContext context,
            ITransactionRepository transactionRepository,
            ICustomerRepository customerRepository)
        {
            _logger = logger;
            _context = context;
            _transactionRepository = transactionRepository;
            _customerRepository = customerRepository;
        }

        public IActionResult Index()
        {
            return View();
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

        [Route("/buy")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Buy([FromForm] BuyRequestModel model)
        {
            var responseModel = new ResponseModel();

            var customer = await _customerRepository.Get(model.CustomerId);
            if (customer is null)
            {
                responseModel.ErrorMessage = "Customer not found";
                return new JsonResult(responseModel);
            }
            if (model.Amount <= 0)
            {
                responseModel.ErrorMessage = "New Amount must be bigger than 0";
                return new JsonResult(responseModel);
            }
            var client = new RestClient(url);
            var request = new RestRequest() { Timeout = 10000 };
            request.AddHeader("X-CMC_PRO_API_KEY", apiKey);
            request.AddHeader("Accepts", "application/json");

            var response = await client.ExecuteAsync<CoinMarketResponse>(request);
            if (!response.IsSuccessful || !string.IsNullOrEmpty(response.ErrorMessage))
            {
                responseModel.ErrorMessage = "Coinmarket response fail.";
                return new JsonResult(responseModel);
            }

            model.Price = Convert.ToDecimal(response.Data.Data.BNB.Quote.USD.Price);
            if (customer.Balance < model.Price * model.Amount)
            {
                responseModel.ErrorMessage = "Customer not enough balance";
                return new JsonResult(responseModel);

            }
            await _transactionRepository.Buy(model.CustomerId, model.Price, model.Amount, model.CoinCode);
            customer.Balance -= model.Price * model.Amount;

            await _context.SaveChangesAsync();

            var transactions = await _transactionRepository.ShowPortfolio(model.CustomerId, model.CoinCode);
            

            responseModel.Data = ToResponseModel(customer, transactions, response.Data?.Data?.BNB?.Quote?.USD);
            responseModel.Done = true;

            return new JsonResult(responseModel);
        }

        public async Task<IActionResult> Account(long customerId, int coinCode = 1)
        {
            _logger.LogInformation(new EventId(1),null,"");
            var customer = await _customerRepository.Get(customerId);
            if (customer is null)
                throw new Exception("Customer not found");

            var transactions = await _transactionRepository.ShowPortfolio(customerId, coinCode);

            var client = new RestClient(url);
            var request = new RestRequest() { Timeout = 10000 };
            request.AddHeader("X-CMC_PRO_API_KEY", apiKey);
            request.AddHeader("Accepts", "application/json");

            var response = await client.ExecuteAsync<CoinMarketResponse>(request);
            if (response is null || !response.IsSuccessful)
                throw new Exception("Coinmarket response error.");


            return await Task.FromResult(View(ToResponseModel(customer, transactions, response.Data?.Data?.BNB?.Quote?.USD)));
        }

        private PortfolioModel ToResponseModel(Customer customer, List<Transaction> transactions, USD usd)
        {
            var quantity = transactions.Any() ? transactions.Sum(x => x.Quantity) : 0;
            var burnedMoney = transactions.Any() ? (transactions.Sum(x => x.Quantity * x.Price)) : 0;
            var newWorth = quantity * Convert.ToDecimal(usd?.Price);

            return new PortfolioModel
            {
                Balance = customer.Balance.ToString("0.00"),
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                NetWorth = newWorth.ToString("0.00"),
                Quantity = quantity.ToString("0.00"),
                AvgBuyPrice = (quantity != 0 ? burnedMoney / quantity : 0).ToString("0.00"),
                Last24HStatus = (usd?.percent_change_24h ?? 0).ToString("0.00"),
                Price = usd?.Price.ToString("0.00"),
                ProfitOrLoss = (newWorth - burnedMoney).ToString("0.00"),
                PercantageOfProfitOrLoss = ((newWorth - burnedMoney) / 100).ToString("0.00")

            };
        }
    }
}
