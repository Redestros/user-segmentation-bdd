using Autofac;
using Autofac.Extensions.DependencyInjection;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using UserSegmentation.Core.Services;

namespace UserSegmentation.Application;

public class ApplicationModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    var services = new ServiceCollection();

    services.AddMediatR(config => config
      .RegisterServicesFromAssembly(typeof(ApplicationModule).Assembly));

    services.AddValidatorsFromAssembly(typeof(ApplicationModule).Assembly);

    services.AddSingleton<SegmentService>();
    
    builder.Populate(services);
    
    base.Load(builder);
  }
}
