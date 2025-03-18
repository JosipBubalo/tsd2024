using GoldSavings.App.Model;
using GoldSavings.App.Client;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Saver!");

        GoldClient goldClient = new GoldClient();

        GoldPrice currentPrice = goldClient.GetCurrentGoldPrice().GetAwaiter().GetResult();
        Console.WriteLine($"The price for today is {currentPrice.Price}");

        List<GoldPrice> thisMonthPrices = goldClient.GetGoldPrices(new DateTime(2024, 03, 01), new DateTime(2024, 03, 11)).GetAwaiter().GetResult();
        foreach(var goldPrice in thisMonthPrices)
        {
            Console.WriteLine($"The price for {goldPrice.Date} is {goldPrice.Price}");
        }

        List<GoldPrice> lowestPrices = goldClient.GetLowestGoldPrices().GetAwaiter().GetResult();
        Console.WriteLine("Top 3 lowest prices:");
        foreach(var goldPrice in lowestPrices)
        {
            Console.WriteLine($"Price for {goldPrice.Date} is {goldPrice.Price}");
        }

        List<GoldPrice> highestPrices = goldClient.GetHighestGoldPrices().GetAwaiter().GetResult();
        Console.WriteLine("Top 3 highest prices:");
        foreach(var goldPrice in highestPrices)
        {
            Console.WriteLine($"Price for {goldPrice.Date} is {goldPrice.Price}");
        }

        _ = goldClient.AnalyzeGoldInvestment();
        _ = goldClient.FindSecondTenOpeningDates();
        _ = goldClient.CalculateAverageGoldPrices();
        _ = goldClient.BestRoiOnGold();
        //_ = goldClient.SavingListToXML();
        _ = goldClient.ReadingXML();
    }
}
