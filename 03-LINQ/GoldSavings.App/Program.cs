using GoldSavings.App.Model;
using GoldSavings.App.Client;
using GoldSavings.App.Services;
namespace GoldSavings.App;

class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Hello, Gold Investor!");

        // Step 1: Get gold prices
        GoldDataService dataService = new GoldDataService();
        GoldClient goldClient = new GoldClient();
        DateTime startDate = new DateTime(2024,09,18);
        DateTime endDate = DateTime.Now;
        List<GoldPrice> goldPrices = dataService.GetGoldPrices(startDate, endDate).GetAwaiter().GetResult();

        if (goldPrices.Count == 0)
        {
            Console.WriteLine("No data found. Exiting.");
            return;
        }

        Console.WriteLine($"Retrieved {goldPrices.Count} records. Ready for analysis.");

        // Step 2: Perform analysis
        GoldAnalysisService analysisService = new GoldAnalysisService(goldPrices);
        var avgPrice = analysisService.GetAveragePrice();

        // Step 3: Print results
        GoldResultPrinter.PrintSingleValue(Math.Round(avgPrice, 2), "Average Gold Price Last Half Year");

        Console.WriteLine("\nGold Analyis Queries with LINQ Completed.");

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

        //Task 2 - Satisfactory

        Func<int, bool> isLeapYear = year => (year % 4 == 0 && year % 100 != 0) || (year % 400 == 0);

        Console.WriteLine($"{startDate.Year} is a leap year: {isLeapYear(startDate.Year)}");
        Console.WriteLine($"{endDate.Year} is a leap year: {isLeapYear(endDate.Year)}");
    }
}
