using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.SegmentAggregate;

public class Parameter : EntityBase, IAggregateRoot
{
  public Parameter(string name, DataType type)
  {
    Name = name;
    Type = type;
    DefaultValue = "";
  }


  public Parameter(string name, string defaultDefaultValue, DataType type = DataType.String)
  {
    Name = name;
    DefaultValue = defaultDefaultValue;
    Type = type;
  }

  public string Name { get; private set; }
  public string DefaultValue { get; private set; }
  public DataType Type { get; private set; }
  // public List<Segment> Segments { get; set; } = new();
}
