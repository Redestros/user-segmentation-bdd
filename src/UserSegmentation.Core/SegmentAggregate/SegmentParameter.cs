namespace UserSegmentation.Core.SegmentAggregate;

public sealed class SegmentParameter
{
  public int ParameterId { get; private set; }
  
  public int SegmentId { get; private set; }
  public string Value { get; private set; } = "";

  // for EF
  public SegmentParameter()
  {
  }
  public SegmentParameter(int parameterId, int segmentId, string value)
  {
    ParameterId = parameterId;
    SegmentId = segmentId;
    Value = value;
  }

  public void SetValue(string value)
  {
    //TODO: add Validation
    this.Value = value;
  }

  public Parameter Parameter { get; set; } = null!;
  public Segment Segment { get; set; } = null!;
}
