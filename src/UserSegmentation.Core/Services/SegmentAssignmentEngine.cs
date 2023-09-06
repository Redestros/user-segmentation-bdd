using UserSegmentation.Core.Interfaces;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.SegmentAggregate.Specifications;
using UserSegmentation.Core.UserAggregate;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.Services;

public class SegmentAssignmentEngine
{
  private readonly IEnumerable<ISegmentAssignmentRule> _rules;
  private readonly IRepository<Segment> _segmentRepository;
  
  public SegmentAssignmentEngine(IEnumerable<ISegmentAssignmentRule> rules, IRepository<Segment> segmentRepository)
  {
    _rules = rules;
    _segmentRepository = segmentRepository;
  }

  public async Task<int> GetSegmentToAssign(User user)
  {
    var defaultSegment = await _segmentRepository.FirstOrDefaultAsync(new DefaultSegmentSpecification());
    if (defaultSegment == null)
    {
      throw new Exception();
    }
    
    var segmentId = defaultSegment.Id;
    
    foreach (var rule in _rules.OrderBy(rule => rule.Order()))
    {
      segmentId = await rule.Evaluate(user);
    }

    return segmentId;
  }
}
