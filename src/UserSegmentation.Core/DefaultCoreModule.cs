using Autofac;
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.Services;
using UserSegmentation.Core.UserAggregate.SegmentationRules;

namespace UserSegmentation.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    var services = new ServiceCollection();

    services.AddScoped<ISegmentService, SegmentService>();

    services.AddScoped<ISegmentAssignmentRule, DefaultRule>();
    services.AddScoped<ISegmentAssignmentRule, SalaryBasedRule>();

    services.AddScoped<SegmentAssignmentEngine>();
    
    builder.Populate(services);
    
    base.Load(builder);
  }
}
