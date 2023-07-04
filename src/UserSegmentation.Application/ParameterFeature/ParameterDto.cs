using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Application.ParameterFeature;

public class ParameterDto
{
  public int Id { get; private set; }
  public string Name { get; private set; }

  public String Type { get; private set; }

  public ParameterDto(int id, string name, String type)
  {
    Id = id;
    Name = name;
    Type = type;
  }

  public static implicit operator ParameterDto(Core.SegmentAggregate.Parameter parameter)
  {
    return new ParameterDto(parameter.Id, parameter.Name, parameter.Type.ToString());
  }
}
