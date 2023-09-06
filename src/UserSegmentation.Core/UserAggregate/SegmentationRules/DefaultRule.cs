using UserSegmentation.Core.Interfaces;

namespace UserSegmentation.Core.UserAggregate.SegmentationRules;

public class DefaultRule : ISegmentAssignmentRule
{
  private readonly ISegmentService _segmentService;

  public DefaultRule(ISegmentService segmentService)
  {
    _segmentService = segmentService;
  }

  public async Task<int> Evaluate(User user)
  {
    var defaultSegment = await _segmentService.GetDefaultSegment();

    return defaultSegment.Id;
  }
}
