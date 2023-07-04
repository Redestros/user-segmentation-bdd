using Microsoft.IdentityModel.Tokens;
using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Application.SegmentFeature;

public class SegmentMapper
{
  public static SegmentDetailDto Map(Segment segment)
  {
    return new SegmentDetailDto(segment.Id, segment.Name, segment.SegmentParameters.Select(x =>
    {
      var value = x.Value.IsNullOrEmpty() ? x.Parameter.DefaultValue : x.Value;
      return new SegmentParameterDto(x.ParameterId, x.Parameter.Name, x.Parameter.Type.ToString(), value);
    }).ToList());
  }
}
