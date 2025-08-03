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
            .Where(namedType => namedType.Accessibility == Accessibility.Public)
            .BlackListNamespace(nameof(LoggableAttributeTest.PrettyPrint))
            .SelectMany(type => type.Methods)
            .Where(method => method.Accessibility == Accessibility.Public)
            .AddAspectIfEligible<LoggableAttribute>();
    }
}

[CompileTime]
public static class ProjectAmenderExtensions
{
    public static IQuery<INamedType> BlackListNamespace(this IQuery<INamedType> query, string namespaceName)
        => query.Where(w => !w.GetNamespace().ContainingNamespace.Name.Contains(namespaceName));
}
