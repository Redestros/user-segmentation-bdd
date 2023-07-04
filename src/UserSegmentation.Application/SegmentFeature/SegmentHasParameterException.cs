using UserSegmentation.Application.Exceptions;

namespace UserSegmentation.Application.SegmentFeature;

public class SegmentHasParameterException: ConflictException
{
  public SegmentHasParameterException(string name) : 
    base($"Segment has parameter {name} already")
  {
  }
}
