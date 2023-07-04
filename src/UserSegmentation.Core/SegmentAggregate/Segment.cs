using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.SegmentAggregate;

public class Segment : EntityBase, IAggregateRoot
{
  private readonly List<Parameter> _parameters = new();
  public List<SegmentParameter> SegmentParameters { get; set; } = new();
  public string Name { private set; get; }
  
  public IReadOnlyCollection<Parameter> Parameters => _parameters.AsReadOnly();

  public Segment(string name)
  {
    Name = name;
  }

  public void AddParameter(Parameter parameter)
  {
    _parameters.Add(parameter);
  }

  public void RemoveParameter(Parameter parameter)
  {
    _parameters.Remove(parameter);
  }

  public void UpdateParameterValue(int parameterId, string value)
  {
    var parameter = SegmentParameters.First(x => x.ParameterId == parameterId);
    parameter.SetValue(value);
  }
}
