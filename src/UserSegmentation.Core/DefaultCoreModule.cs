using Autofac;
using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.Services;

namespace UserSegmentation.Core;

public class DefaultCoreModule : Module
{
  protected override void Load(ContainerBuilder builder)
  {
    builder.RegisterType<AssignUserToSegmentService>()
      .As<IAssignUserToSegmentService>().InstancePerLifetimeScope();
  }
}
