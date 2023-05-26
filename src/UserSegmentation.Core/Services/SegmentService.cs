using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.SegmentAggregate.Specifications;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.Services;

public class SegmentService
{
  private readonly IRepository<Segment> _repository;

  public SegmentService(IRepository<Segment> repository)
  {
    _repository = repository;
  }

  public async Task CreateDefaultSegment()
  {
    var existingDefault = await _repository.FirstOrDefaultAsync(new DefaultSegmentSpecification());

    if (existingDefault == null)
    {
      var defaultSegment = new Segment("Default");
      await _repository.AddAsync(defaultSegment);
    }
  }

  public async Task<Segment> GetDefaultSegment()
  {
    var defaultSegment = await _repository.FirstOrDefaultAsync(new DefaultSegmentSpecification());

    if (defaultSegment == null)
    {
      throw new Exception("Default segment not found");
    }

    return defaultSegment;
  }
}
