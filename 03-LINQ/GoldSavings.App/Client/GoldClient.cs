using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using GoldSavings.App.Model;
using System.Xml.Linq;

namespace GoldSavings.App.Client;

public class GoldClient
{
    private HttpClient _client;
    public GoldClient()
    {
        _client = new HttpClient();
        _client.BaseAddress = new Uri("http://api.nbp.pl/api/");
        _client.DefaultRequestHeaders.Accept.Clear();

    }
    public async Task<GoldPrice> GetCurrentGoldPrice()
    {
        try
        {
            HttpResponseMessage responseMsg = _client.GetAsync("cenyzlota/").GetAwaiter().GetResult();
            if (responseMsg.IsSuccessStatusCode)
            {
                string content = await responseMsg.Content.ReadAsStringAsync();
                List<GoldPrice>? prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
                if (prices != null && prices.Count == 1)
                {
                    return prices[0];
                }
            }
            return null;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"API Request Error: {e.Message}");
            return null;
        }
   
        
        }

    public async Task<List<GoldPrice>> GetGoldPrices(DateTime startDate, DateTime endDate)
    {
        string dateFormat = "yyyy-MM-dd";
        string requestUri = $"cenyzlota/{startDate.ToString(dateFormat)}/{endDate.ToString(dateFormat)}";
        HttpResponseMessage responseMsg = _client.GetAsync(requestUri).GetAwaiter().GetResult();
        if (responseMsg.IsSuccessStatusCode)
        {
            string content = await responseMsg.Content.ReadAsStringAsync();
            List<GoldPrice> prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
            return prices;
        }
        else
        {
            return null;
        }

    }

    public async Task<List<GoldPrice>> GetLowestGoldPrices()
    {
        DateTime startDate = DateTime.Today.AddYears(-1);
        DateTime endDate = DateTime.Today;
        string dateFormat = "yyyy-MM-dd";
        string requestUri = $"cenyzlota/{startDate.ToString(dateFormat)}/{endDate.ToString(dateFormat)}";
        HttpResponseMessage responseMsg = _client.GetAsync(requestUri).GetAwaiter().GetResult();
        if (responseMsg.IsSuccessStatusCode)
        {
            string content = await responseMsg.Content.ReadAsStringAsync();
            List<GoldPrice> prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
            var top3Lowest = prices.OrderBy(p => p.Price).Take(3).ToList();
            return top3Lowest;
        }
        else
        {
            return null;
        }
    }

    public async Task<List<GoldPrice>> GetHighestGoldPrices()
    {
        DateTime startDate = DateTime.Today.AddYears(-1);
        DateTime endDate = DateTime.Today;
        string dateFormat = "yyyy-MM-dd";
        string requestUri = $"cenyzlota/{startDate.ToString(dateFormat)}/{endDate.ToString(dateFormat)}";
        HttpResponseMessage responseMsg = _client.GetAsync(requestUri).GetAwaiter().GetResult();
        if (responseMsg.IsSuccessStatusCode)
        {
            string content = await responseMsg.Content.ReadAsStringAsync();
            List<GoldPrice> prices = JsonConvert.DeserializeObject<List<GoldPrice>>(content);
            var top3Highest = prices.OrderByDescending(p => p.Price).Take(3).ToList();
            return top3Highest;
        }
        else
        {
            return null;
        }
    }

    public async Task AnalyzeGoldInvestment()
    {
        DateTime startDate = new DateTime(2020, 1, 1);
        DateTime endDate = new DateTime(2020, 1, 31);
        List<GoldPrice> jan2020Prices = await GetGoldPrices(startDate, endDate);


        DateTime startDateAllPrices = new DateTime(2025, 3, 10);
        DateTime endDateAllPrices  = new DateTime(2025, 3, 17);;
        List<GoldPrice> recentPrices = await GetGoldPrices(startDateAllPrices, endDateAllPrices);

        if (jan2020Prices != null && jan2020Prices.Count > 0 && recentPrices != null && recentPrices.Count > 0)
        {
            var profitableDays = (from janPrice in jan2020Prices
                                from recentPrice in recentPrices
                                where recentPrice.Price > (janPrice.Price * 1.05)
                                select new
                                {
                                    SaleDate = recentPrice.Date,
                                    SalePrice = recentPrice.Price
                                }).ToList();

            Console.WriteLine("Days where price increased more than 5%:");
            foreach (var day in profitableDays)
            {
                Console.WriteLine($"Day where price is more then 5%: {day.SaleDate}");
            }
        }
        else
        {
            Console.WriteLine("No sufficient data available.");
        }
    }

    public async Task FindSecondTenOpeningDates()
    {
        DateTime startDate = new DateTime(2019, 1, 1);
        DateTime endDate = new DateTime(2019, 12, 31);
        List<GoldPrice> prices = await GetGoldPrices(startDate, endDate);

        DateTime startDate2020 = new DateTime(2020, 1, 1);
        DateTime endDate2020 = new DateTime(2020, 12, 31);
        List<GoldPrice> prices2020 = await GetGoldPrices(startDate2020, endDate2020);

        DateTime startDate2021 = new DateTime(2021, 1, 1);
        DateTime endDate2021 = new DateTime(2021, 12, 31);
        List<GoldPrice> prices2021 = await GetGoldPrices(startDate2021, endDate2021);

        DateTime startDate2022 = new DateTime(2022, 1, 1);
        DateTime endDate2022 = new DateTime(2022, 12, 31);
        List<GoldPrice> prices2022 = await GetGoldPrices(startDate2022, endDate2022);

        prices.AddRange(prices2020);
        prices.AddRange(prices2021);
        prices.AddRange(prices2022);

        if (prices != null && prices.Count >= 13)
        {
            var secondTenOpening = prices
                .OrderByDescending(p => p.Price)
                .Skip(10)
                .Take(3)
                .ToList();

            Console.WriteLine("Dates opening the second ten in price ranking:");
            foreach (var price in secondTenOpening)
            {
                Console.WriteLine($"{price.Date}: {price.Price}");
            }
        }
        else
        {
            Console.WriteLine("Not enough data available.");
        }
    }

    public async Task CalculateAverageGoldPrices()
    {
        List<GoldPrice> prices2020 = await GetGoldPrices(new DateTime(2020, 1, 1), new DateTime(2020, 12, 31));
        List<GoldPrice> prices2023 = await GetGoldPrices(new DateTime(2023, 1, 1), new DateTime(2023, 12, 31));
        List<GoldPrice> prices2024 = await GetGoldPrices(new DateTime(2024, 1, 1),  new DateTime(2024, 12, 31));

        var avg2020 = prices2020.Any() ? prices2020.Average(p => p.Price) : 0;
        var avg2023 = prices2023.Any() ? prices2023.Average(p => p.Price) : 0;
        var avg2024 = prices2024.Any() ? prices2024.Average(p => p.Price) : 0;

        Console.WriteLine($"Average Gold Price in 2020: {avg2020:F2}");
        Console.WriteLine($"Average Gold Price in 2023: {avg2023:F2}");
        Console.WriteLine($"Average Gold Price in 2024: {avg2024:F2}");
    }
    
    public async Task BestRoiOnGold()
    {
        List<GoldPrice> prices2020 = await GetGoldPrices(new DateTime(2020, 1, 1), new DateTime(2020, 12, 31));
        List<GoldPrice> prices2021 = await GetGoldPrices(new DateTime(2021, 1, 1), new DateTime(2021, 12, 31));
        List<GoldPrice> prices2022 = await GetGoldPrices(new DateTime(2022, 1, 1), new DateTime(2022, 12, 31));
        List<GoldPrice> prices2023 = await GetGoldPrices(new DateTime(2023, 1, 1), new DateTime(2023, 12, 31));
        List<GoldPrice> prices2024 = await GetGoldPrices(new DateTime(2024, 1, 1),  new DateTime(2024, 12, 31));

        List<GoldPrice> prices = prices2020;
        prices.AddRange(prices2021);
        prices.AddRange(prices2022);
        prices.AddRange(prices2023);
        prices.AddRange(prices2024);

        var highest = prices.OrderByDescending(p => p.Price).Take(1).ToList();
        var lowest = prices.OrderBy(p => p.Price).Take(1).ToList();

        foreach(var goldPrice in lowest)
        {
            Console.WriteLine($"Best day for buying gold is {goldPrice.Date} with price {goldPrice.Price}");
        }

        foreach(var goldPrice in highest)
        {
            Console.WriteLine($"Best day for selling gold is {goldPrice.Date} with price {goldPrice.Price}");
        }
    }

    public async Task SavingListToXML()
    {
        List<GoldPrice> prices = await GetGoldPrices(new DateTime(2025, 3, 1),  new DateTime(2025, 3, 17));

        var xml = new XElement("Prices", prices.Select(x => new XElement("price", 
                                                new XAttribute("Date", x.Date), 
                                                new XAttribute("Price", x.Price))));

        XDocument document = new XDocument(new XComment("Price document"), xml);

        document.Declaration = new XDeclaration("1.0", "utf-8", "true");

        document.Save(@"C:\Users\josip\Desktop\prices.xml");

        Console.WriteLine("Saved XML document");
    }

    public async Task ReadingXML()
    {
        XDocument xmlDoc = XDocument.Load(@"C:\Users\josip\Desktop\prices.xml");
        Console.WriteLine("Reading XML document");

        List<GoldPrice> goldPrices = xmlDoc.Descendants("price")
            .Select(p => new GoldPrice
            {
                Date = DateTime.Parse(p.Attribute("Date")?.Value),
                Price = double.Parse(p.Attribute("Price")?.Value)
            })
            .ToList();

       foreach (var price in goldPrices)
        {
            Console.WriteLine($"Date {price.Date:yyyy-MM-dd} - price {price.Price}");
        }
    }
}