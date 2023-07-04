using UserSegmentation.Application.Exceptions;

namespace UserSegmentation.Application.SegmentFeature;

public class SegmentAlreadyExistsException : ConflictException
{
  public SegmentAlreadyExistsException(string name) : 
    base($"Segment with name {name} already exists")
  {
  }
}
