using MediatR;
using UserSegmentation.Application.Segment.commands;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.Segment.Commands;

public class CreateSegmentCommandHandler : IRequestHandler<CreateSegmentCommand, int>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _repository;

  public CreateSegmentCommandHandler(IRepository<Core.SegmentAggregate.Segment> repository)
  {
    _repository = repository;
  }


  public async Task<int> Handle(CreateSegmentCommand request, CancellationToken cancellationToken)
  {
    var newSegment = new Core.SegmentAggregate.Segment(request.Name);
    var createdSegment = await _repository.AddAsync(newSegment, cancellationToken);
    return createdSegment.Id;
  }
}
