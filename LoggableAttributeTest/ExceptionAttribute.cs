using System;
using Metalama.Framework.Aspects;

namespace LoggableAttributeTest;

public class ExceptionAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        try
        {
            return meta.Proceed();
        }
        catch (Exception e) {
            foreach (var param in meta.Target.Method.Parameters)
            {
                e.Data.Add(param.Name, param.Value.ToString());
            }

            throw e;
        }
    }
}
