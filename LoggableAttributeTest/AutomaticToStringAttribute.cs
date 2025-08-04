using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code.SyntaxBuilders;
using Metalama.Framework.Code;

namespace LoggableAttributeTest;

internal class AutomaticToStringAttribute : TypeAspect
{
    [Introduce(WhenExists = OverrideStrategy.Override, Name = "ToString")]
    public string IntroducedToString()
    {
        var stringBuilder = new InterpolatedStringBuilder();
        stringBuilder.AddText("{ ");
        stringBuilder.AddText(meta.Target.Type.Name);
        stringBuilder.AddText(" ");

        var properties = meta.Target.Type.AllFieldsAndProperties
            .Where(w => w is { IsStatic: false, IsImplicitlyDeclared: false, Accessibility: Accessibility.Public })
            .OrderBy(o => o.Name)
            .ToList();

        // compile time variable
        // if meta.Compliletime isn't used this line will be executed at runtime.
        var i = meta.CompileTime(0);

        foreach (var property in properties)
        {
            if (i > 0)
            {
                stringBuilder.AddText(", ");
            }

            stringBuilder.AddText(property.Name);
            stringBuilder.AddText(" = ");
            stringBuilder.AddExpression(property);

            i++;
        }

        stringBuilder.AddText(" }");

        return stringBuilder.ToValue();
    }    
}
