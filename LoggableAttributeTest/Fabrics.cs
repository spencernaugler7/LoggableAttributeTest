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
            .Where(w => w.Accessibility == Accessibility.Public)
            .SelectMany(type => type.Methods)
            .Where(w => w.Accessibility == Accessibility.Public)
            .AddAspectIfEligible<LoggableAttribute>();
    }
}
