using Ardalis.Specification.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.SegmentAggregate.Specifications;
using UserSegmentation.Infrastructure.Data;

namespace UserSegmentation.Application.Data;

public class SegmentLoader : IDatabaseSeeder
{
  public void Seed(AppDbContext context)
  {
    var segments = new List<Segment>()
    {
      new("Default"),
      new("Silver"),
      new("Gold"),
      new("Platinum")
    };

    foreach (var segment in segments
               .Where(segment => !context.Set<Segment>().Exists(x => x.Name.Equals(segment.Name))))
    {
      context.Set<Segment>().Add(segment);
    }

    context.SaveChanges();

    var defaultSegment = context.Set<Segment>()
      .WithSpecification(new DefaultSegmentSpecification())
      .Include(x => x.Parameters)
      .First();


    if (defaultSegment.Parameters.Count != 0)
    {
      return;
    }
    
    var parameters = context.Set<Parameter>().ToList();

    foreach (var parameter in parameters)
    {
      defaultSegment.AddParameter(parameter);
    }

    context.SaveChanges();

  }

  public int GetOrder()
  {
    return 1;
  }
}
