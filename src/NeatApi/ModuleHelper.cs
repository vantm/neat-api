using NeatApi.DependencyInjection;
using NeatApi.Routing;
using System.Collections.Immutable;
using System.Reflection;

#pragma warning disable IDE0130, S1200
namespace Microsoft.AspNetCore.Builder;

internal static class ModuleHelper
{
    public static IEnumerable<Type> FindServiceModuleTypes(params Assembly[] assemblies)
    {
        return (
            from a in assemblies
            from t in a.GetTypes()
            where IsClass(t) && IsServiceModule(t)
            select t
       ).ToImmutableList();
    }

    public static IEnumerable<Type> FindRoutingModuleTypes(params Assembly[] assemblies)
    {
        return (
            from a in assemblies
            from t in a.GetTypes()
            where IsClass(t) && IsRoutingModule(t)
            select t
       ).ToImmutableList();
    }

    private static bool IsClass(Type t) => t.IsClass && !t.IsAbstract;
    private static bool IsServiceModule(Type t) => t.IsAssignableTo(typeof(IServiceModule));
    private static bool IsRoutingModule(Type t) => t.IsAssignableTo(typeof(IRoutingModule));
}
