using UserSegmentation.Infrastructure.Data;

namespace UserSegmentation.Application.Data;

public interface IDatabaseSeeder
{
  void Seed(AppDbContext context);

  int GetOrder() => 0;
}
