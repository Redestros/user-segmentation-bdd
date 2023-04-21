using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.SegmentAggregate;

public class Segment : EntityBase, IAggregateRoot
{
  private readonly List<SegmentParameter> _parameters = new();
  public string Name { private set; get; }

  public IReadOnlyCollection<SegmentParameter> Parameters => _parameters.AsReadOnly();

  public Segment(string name)
  {
    Name = name;
  }

  public void AddParameter(SegmentParameter parameter)
  {
    _parameters.Add(parameter);
  }

  public void RemoveParameter(SegmentParameter parameter)
  {
    _parameters.Remove(parameter);
  }
}
