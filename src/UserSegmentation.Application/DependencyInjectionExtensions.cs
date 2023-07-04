using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace UserSegmentation.Application;

public static class DependencyInjectionExtensions
{
  public static void RegisterImplementationsOfInterface(this IServiceCollection services, Assembly assembly,
    Type interfaceType)
  {
    var implementationTypes = assembly.GetTypes()
      .Where(t => interfaceType.IsAssignableFrom(t) && t is { IsInterface: false, IsAbstract: false });

    foreach (var implementationType in implementationTypes)
    {
      services.AddSingleton(interfaceType, implementationType);
    }
  }
}
