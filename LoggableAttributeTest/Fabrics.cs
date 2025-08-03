using System;
using Metalama.Framework.Aspects;
using Metalama.Framework.Code;
using Metalama.Framework.Fabrics;

namespace LoggableAttributeTest;

public class Fabrics : ProjectFabric
{
    public override void AmendProject(IProjectAmender amender)
    {
        amender
            .SelectMany(complilation => complilation.AllTypes)
            .Where(namedType => namedType.Accessibility == Accessibility.Public
                    && !namedType.GetNamespace().ContainingNamespace.Name.Contains(nameof(LoggableAttributeTest.PrettyPrint)))
            .SelectMany(type => type.Methods)
            .Where(method => method.Accessibility == Accessibility.Public)
            .AddAspectIfEligible<LoggableAttribute>();
    }
}
