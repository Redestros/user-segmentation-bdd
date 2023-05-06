using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace UserSegmentation.Application;

public class ApplicationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    var services = new ServiceCollection();

    services.AddMediatR(config => config
      .RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));

    services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);
    
    builder.Populate(services);
    
    base.Load(builder);
  }
}
