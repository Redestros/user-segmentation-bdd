using UserSegmentation.Infrastructure.Data;

namespace UserSegmentation.Application.Data;

public class DataLoader
{
  private readonly IEnumerable<IDatabaseSeeder> _seeders;
  private readonly AppDbContext _context;

  public DataLoader(IEnumerable<IDatabaseSeeder> seeders, AppDbContext context)
  {
    _seeders = seeders;
    _context = context;
  }

  public void LoadData()
  {
    foreach (var databaseSeeder in _seeders.OrderBy(x => x.GetOrder()))
    {
      databaseSeeder.Seed(_context);
    }

    _context.SaveChanges();
  }
  
}
