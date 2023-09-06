using System.Reflection;
using Microsoft.EntityFrameworkCore;
using UserSegmentation.Infrastructure.Converters;
using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Infrastructure.Data;

public class AppDbContext : DbContext
{
  private readonly IDomainEventDispatcher? _dispatcher;

  public AppDbContext(DbContextOptions<AppDbContext> options,
    IDomainEventDispatcher? dispatcher)
    : base(options)
  {
    _dispatcher = dispatcher;
  }

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    base.OnModelCreating(modelBuilder);
    modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
  }

  protected override void ConfigureConventions(ModelConfigurationBuilder builder)
  {
    base.ConfigureConventions(builder);

    builder.Properties<DateOnly>()
      .HaveConversion<DateOnlyConverter>();
  }

  public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new())
  {
    var result = await base.SaveChangesAsync(cancellationToken).ConfigureAwait(false);

    // ignore events if no dispatcher provided
    if (_dispatcher == null)
    {
      return result;
    }

    // dispatch events only if save was successful
    var entitiesWithEvents = ChangeTracker.Entries<EntityBase>()
      .Select(e => e.Entity)
      .Where(e => e.DomainEvents.Any())
      .ToArray();

    await _dispatcher.DispatchAndClearEvents(entitiesWithEvents);

    return result;
  }

  public override int SaveChanges()
  {
    return SaveChangesAsync().GetAwaiter().GetResult();
  }
}
