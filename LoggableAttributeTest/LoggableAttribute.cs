using System.ComponentModel;
using Metalama.Framework.Aspects;

namespace LoggableAttributeTest;

public class LoggableAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var method = meta.Target.Method;

        Console.WriteLine($"Entering {method.Name}");
        foreach (var param in method.Parameters)
        {
            var value = string.Empty;
            Console.WriteLine($"Param: {param.Name}: {param.Value}");
        }

        var result = meta.Proceed();

        Console.WriteLine($"Result: {result}");
        return result;
    }
}