using System;
using Metalama.Framework.Aspects;

namespace LoggableAttributeTest;

#pragma warning disable CA2200 // original stack trace is logged
#pragma warning disable CS8602

public class IntecerceptExceptionAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        try
        {
            return meta.Proceed();
        }
        catch (Exception e)
        {
            foreach (var param in meta.Target.Method.Parameters)
            {
                e.Data.Add(param.Name, param.Value.ToString());
            }

            throw e;
        }
    }
}

public class EnrichExceptionAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        throw new NotImplementedException();
    }
}

public static class EnrichExceptionHandler
{
    private const string _slotName = "Context";

    [ExcludeAspect(typeof(EnrichExceptionAttribute))]
    public static void AppendContextFrame(this Exception e, string frame)
    {

    }
}