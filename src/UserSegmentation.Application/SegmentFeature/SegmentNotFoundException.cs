using UserSegmentation.Application.Exceptions;

namespace UserSegmentation.Application.SegmentFeature;

public class SegmentNotFoundException : NotFoundException
{
  public SegmentNotFoundException(string name) : base($"Segment with name {name} not found")
  {
  }

  public SegmentNotFoundException(int id) : base($"Segment with Id {id} not found")
  {
    
  }
}
