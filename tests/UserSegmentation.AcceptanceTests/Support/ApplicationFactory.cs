using System.Data.Common;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using UserSegmentation.Infrastructure.Data;
using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;
using UserSegmentation.Web;

namespace UserSegmentation.AcceptanceTests.Support;

public class ApplicationFactory : WebApplicationFactory<Program>
{
  protected override void ConfigureWebHost(IWebHostBuilder builder)
  {
    builder.ConfigureServices(services =>
    {
      var dbContextDescriptor = services.SingleOrDefault(d => d.ServiceType ==
                                                              typeof(DbContextOptions<AppDbContext>));

      if ( dbContextDescriptor != null )
      {
        services.Remove(dbContextDescriptor);
      }

      var dbConnectionDescriptor = services.SingleOrDefault(d => d.ServiceType ==
                                                                 typeof(DbConnection));

      if ( dbConnectionDescriptor != null )
      {
        services.Remove(dbConnectionDescriptor);
      }

      // Create open SqliteConnection so EF won't automatically close it.
      services.AddSingleton<DbConnection>(_ =>
      {
        var connection = new SqliteConnection("DataSource=:memory:");
        connection.Open();

        return connection;
      });
      
      services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<WebMarker>());

      services.AddScoped<IDomainEventDispatcher, DomainEventDispatcher>();
      
      var descriptorsToRemove = services
        .Where(d => d.ServiceType.FullName?.Contains("AppDbContext") == true ||
                    d.ServiceType.FullName?.Contains("DbContextOptions") == true)
        .ToList();

      foreach (var descriptor in descriptorsToRemove)
      {
        services.Remove(descriptor);
      }

      services.AddDbContext<AppDbContext>((container, options) =>
      {
        var connection = container.GetRequiredService<DbConnection>();
        options.UseSqlite(connection);
      });

      var sp = services.BuildServiceProvider();

      using (var scope = sp.CreateScope())
      {
          var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
          db.Database.EnsureCreated();
      }

      builder.UseEnvironment("Development");
    });
  }
}
