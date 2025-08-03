using System.ComponentModel;
using Metalama.Framework.Aspects;

namespace LoggableAttributeTest;

public class LoggableAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        Console.WriteLine($"Entering {meta.Target.Method}");
        try
        {
            return meta.Proceed();
        }
        finally
        {
            Console.WriteLine($"Leaving {meta.Target.Method}");
        }
    }
}