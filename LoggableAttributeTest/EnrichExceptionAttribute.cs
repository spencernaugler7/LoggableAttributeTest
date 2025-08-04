using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Code;
using System.Text;

namespace LoggableAttributeTest;

/// <summary>
/// speical attribute to add function parameter values to a the stack trace if an exception is thrown in a method
/// only used to add information <see cref="GetContextInfo"/> to retrive info.
/// <seealso cref="EnrichExceptionHeler"/>
/// </summary>
public class EnrichExceptionAttribute : OverrideMethodAspect
{
    public override dynamic? OverrideMethod()
    {
        var builder = new InterpolatedStringBuilder();
        builder.AddText(meta.Target.Type.ToString());
        builder.AddText(".");
        builder.AddText(meta.Target.Method.Name);
        builder.AddText("(");

        foreach (var param in meta.Target.Method.Parameters)
        {
            if (param.Index > 0)
            {
                builder.AddText(",");
            }

            var kind = param.RefKind switch
            {
                RefKind.Out => $"{param.Name} = <out>",
                RefKind.Ref => $"{param.Name} = <ref>",
                _ => param.Name
            };

            builder.AddText(kind);
        }

        builder.AddText(")");

        try
        {
            return meta.Proceed();
        }
        catch (Exception exception)
        {
            exception.AppendContextFrame((string)builder.ToValue());
            throw exception;
        }
    }
}

/// <summary>
/// helper methods for attribute <see cref="EnrichExceptionAttribute"/>
/// </summary>
public static class EnrichExceptionHelper
{
    private const string _slotName = "Context";

    /// <summary>
    /// helper to add a append a single line from a stack trace to a <see cref="Exception"/>'s <see cref="Exception.Data"/> dictionary.
    /// uses a special key named <see cref="_slotName"/> which contains our special stack trance info.
    /// </summary>
    /// <param name="exception"></param>
    /// <param name="frame"></param>
    [ExcludeAspect(typeof(EnrichExceptionAttribute))] // we don't want to apply this attribute here. EnrichExceptionAttribute uses the funciton and if applied we would infinitely recurse.
    public static void AppendContextFrame(this Exception exception, string frame)
    {
        var builder = (StringBuilder?)exception.Data[_slotName];

        if (builder is null)
        {
            builder = new();
            exception.Data[_slotName] = builder;
        }

        builder.Append(frame);
        builder.AppendLine();
    }

    /// <summary>
    /// used by calling code to retrieve info created when a <see cref="EnrichExceptionAttribute"/> is applied to a method
    /// Usage
    /// <code>
    /// 
    /// [EnrichException] // this attribute can be explicitly added or use <see cref="Metalama.Framework.Fabrics"/> to automatically apply them.
    /// private float DivideBy(int a, int b) => a / b;
    /// 
    /// try 
    /// {
    ///     DivideBy(3, 0);
    /// } 
    /// catch (Exception exception) 
    /// {
    ///     var context = e.GetContextInfo();
    ///     if ( context != null )
    ///     {    
    ///         Console.WriteLine( "---with---" );
    ///         Console.Write( context );
    ///         Console.WriteLine( "----------" );
    ///     }
    /// }
    /// 
    /// </code
    /// </summary>
    /// <param name="exception"></param>
    /// <returns>string</returns>
    public static string? GetContextInfo(this Exception exception)
        => ((StringBuilder?)exception.Data[_slotName])?.ToString();
}
