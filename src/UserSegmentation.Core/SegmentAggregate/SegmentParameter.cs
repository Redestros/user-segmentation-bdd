using UserSegmentation.SharedKernel;

namespace UserSegmentation.Core.SegmentAggregate;

public sealed class SegmentParameter : EntityBase
{
  public string Name { get; private set; }
  public string Value { get; private set; }
  public int SegmentId { get; private set; }

  public SegmentParameter(string name, string value, int segmentId)
  {
    Name = name;
    Value = value;
    SegmentId = segmentId;
  }

  public Segment? Segment { get; set; }
}
