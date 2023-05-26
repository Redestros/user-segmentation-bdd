using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.Services;

namespace UserSegmentation.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    var services = new ServiceCollection();

    services.AddScoped<ICreateUserService, CreateUserService>();
    builder.Populate(services);
    
    base.Load(builder);
  }
}
