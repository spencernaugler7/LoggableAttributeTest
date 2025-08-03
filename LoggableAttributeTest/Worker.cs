using System;
using Microsoft.Extensions.Hosting;

namespace LoggableAttributeTest;


internal class Blah(string SubBlah, int SubBlah2);

internal class CollectionBlah (List<string> SubBlah, Dictionary<string, string> SubBlah2);

public class Worker : BackgroundService
{
    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {

        Console.WriteLine("enter number to divide from 3:");
        var num = Console.ReadLine();

        DoStuff(num, DateTime.Now, ["one, two"]);

        return Task.CompletedTask;
    }

    public static void DoStuff(string num, DateTime time, List<string> strings)
    {
        var blah = new Blah("Test", 213);
        var blah2 = new CollectionBlah(["Test1", "Test2"], new Dictionary<string, string> { { "Test1", "Test3" } });

        if (string.IsNullOrEmpty(num) || !int.TryParse(num.Trim(), out int parsedNum))
        {
            Console.WriteLine("Bye !");
            return;
        }

        var a = 3 / parsedNum;
        Console.WriteLine(a);
    }
}
