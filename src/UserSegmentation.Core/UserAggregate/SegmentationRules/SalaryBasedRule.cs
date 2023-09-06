using UserSegmentation.Core.Interfaces;

namespace UserSegmentation.Core.UserAggregate.SegmentationRules;

public class SalaryBasedRule : ISegmentAssignmentRule
{
  private readonly ISegmentService _segmentService;

  public SalaryBasedRule(ISegmentService segmentService)
  {
    _segmentService = segmentService;
  }

  public async Task<int> Evaluate(User user)
  {
    var defaultSegment = await _segmentService.GetDefaultSegment();
    
    if (user.GrossAnnualRevenue < 30000)
    {
      return defaultSegment.Id;
    }
    
    if (user.IsAboveEighteen() && user.SocialScore > 80)
    {
      switch (user.GrossAnnualRevenue)
      {
        case < 50000:
        {
          var silverSegment = await _segmentService.Get("Silver");
          return silverSegment.Id;
        }
        case < 80000:
        {
          var goldSegment = await _segmentService.Get("Gold");
          return goldSegment.Id;
        }
      }
    }
    
    return defaultSegment.Id;
  }
}
