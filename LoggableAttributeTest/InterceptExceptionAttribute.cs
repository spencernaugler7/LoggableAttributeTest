using System.Text;
using Metalama.Framework.Aspects;

namespace LoggableAttributeTest;

#pragma warning disable CA2200 // original stack trace is logged
#pragma warning disable CS8602

/// <summary>
/// intercepts method call and adds function params to <see cref="Exception.Data"/> dictionary if the call throws an exception.
/// </summary>
public class InterceptExceptionAttribute : OverrideMethodAspect
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