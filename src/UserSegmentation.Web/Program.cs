using Ardalis.ListStartupServices;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using UserSegmentation.Core;
using UserSegmentation.Infrastructure;
using UserSegmentation.Infrastructure.Data;
using UserSegmentation.Web;
using Serilog;
using UserSegmentation.Application;
using UserSegmentation.Core.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

builder.Host.UseSerilog((_, config) => config.ReadFrom.Configuration(builder.Configuration));

// builder.Services.Configure<CookiePolicyOptions>(options =>
// {
//   options.CheckConsentNeeded = _ => true;
//   options.MinimumSameSitePolicy = SameSiteMode.None;
// });

var connectionString =
  builder.Configuration
    .GetConnectionString("SqliteConnection"); //Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext(connectionString!);

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// add list services for diagnostic purposes - see https://github.com/ardalis/AspNetCoreStartupServices
builder.Services.Configure<ServiceConfig>(config =>
{
  config.Services = new List<ServiceDescriptor>(builder.Services);

  // optional - default path to view services is /listallservices - recommended to choose your own path
  config.Path = "/listservices";
});


builder.Host.ConfigureContainer<ContainerBuilder>(containerBuilder =>
{
  containerBuilder.RegisterModule(new DefaultCoreModule());
  containerBuilder.RegisterModule(
    new DefaultInfrastructureModule(builder.Environment.EnvironmentName == "Development"));
  containerBuilder.RegisterModule(
    new ApplicationModule());
});

builder.Services.AddHealthChecks();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
  app.UseShowAllServicesMiddleware();
}

// app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseFluentValidationExceptionHandler();

app.MapHealthChecks("/healthz");

// Seed Database
using (var scope = app.Services.CreateScope())
{
  var services = scope.ServiceProvider;

  try
  {
    var context = services.GetRequiredService<AppDbContext>();
    context.Database.EnsureCreated();
    SeedData.Initialize(services);

    var segmentService = services.GetRequiredService<SegmentService>();
    await segmentService.CreateDefaultSegment();
  }
  catch (Exception ex)
  {
    var logger = services.GetRequiredService<ILogger<Program>>();
    logger.LogError(ex, "An error occurred seeding the DB. {exceptionMessage}", ex.Message);
  }
}

app.Run();
public partial class Program { }
