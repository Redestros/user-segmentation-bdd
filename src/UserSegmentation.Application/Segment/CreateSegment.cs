using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.Segment;

public record CreateSegmentCommand(string Name) : IRequest<Result<int>>;

public class CreateSegment : IRequestHandler<CreateSegmentCommand, Result<int>>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _repository;

  public CreateSegment(IRepository<Core.SegmentAggregate.Segment> repository)
  {
    _repository = repository;
  }


  public async Task<Result<int>> Handle(CreateSegmentCommand request, CancellationToken cancellationToken)
  {
    if (_repository.Exists(x => x.Name.ToLower().Equals(request.Name.ToLower())))
    {
      return new Result<int>(new SegmentAlreadyExistsException(request.Name));
    }

    var newSegment = new Core.SegmentAggregate.Segment(request.Name);
    var createdSegment = await _repository.AddAsync(newSegment, cancellationToken);
    return createdSegment.Id;
  }
}
