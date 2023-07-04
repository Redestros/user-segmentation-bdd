using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Infrastructure.Data;

namespace UserSegmentation.Application.Data;

public class ParameterLoader : IDatabaseSeeder
{
  public void Seed(AppDbContext context)
  {
    var defaultParameters = new List<Parameter>()
    {
      new("Max transaction amount per day", "700", DataType.Integer),
      new("Maximum Number of transactions per day ", "5", DataType.Integer),
      new("Maximum Total Amount of Transaction per day", "5000", DataType.Integer),
      new("Maximum Amount Per Transaction", "3000", DataType.Integer)
    };
    
    foreach (var defaultParameter in defaultParameters
               .Where(defaultParameter => !context.Set<Parameter>().Exists(x => x.Name.Equals(defaultParameter.Name)))
             )
    {
      context.Set<Parameter>().Add(defaultParameter);
    }

    context.SaveChanges();
  }
}
