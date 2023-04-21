using MediatR;

namespace UserSegmentation.Application.Segment.commands;

public class CreateSegmentCommand : IRequest<int>
{
  public CreateSegmentCommand(string name)
  {
    Name = name;
  }

  public string Name { get; private set; }
}
