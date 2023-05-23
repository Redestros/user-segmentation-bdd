using Ardalis.Specification;
using LanguageExt.Common;
using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.Segment;

public record GetSegmentByNameQuery(string Name) : IRequest<Result<SegmentDto>>;

internal class GetSegmentByNameHandler : IRequestHandler<GetSegmentByNameQuery, Result<SegmentDto>>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _repository;

  public GetSegmentByNameHandler(IRepository<Core.SegmentAggregate.Segment> repository)
  {
    _repository = repository;
  }

  public async Task<Result<SegmentDto>> Handle(GetSegmentByNameQuery request, CancellationToken cancellationToken)
  {
    var segment = await _repository.FirstOrDefaultAsync(
      new SegmentSpec(request), cancellationToken);

    return segment == null
      ? new Result<SegmentDto>(new SegmentNotFoundException(request.Name))
      : new SegmentDto(segment.Id, segment.Name);
  }
}

public sealed class SegmentSpec : Specification<Core.SegmentAggregate.Segment>
{
  public SegmentSpec(GetSegmentByNameQuery byNameQuery)
  {
    Query.Where(s => s.Name.Equals(byNameQuery.Name));
  }
}

public record GetSegmentByIdQuery(int Id) : IRequest<Result<SegmentDto>>;

internal class GetSegmentByIdHandler : IRequestHandler<GetSegmentByIdQuery, Result<SegmentDto>>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _repository;

  public GetSegmentByIdHandler(IRepository<Core.SegmentAggregate.Segment> repository)
  {
    _repository = repository;
  }

  public async Task<Result<SegmentDto>> Handle(GetSegmentByIdQuery request, CancellationToken cancellationToken)
  {
    var segment = await _repository.GetByIdAsync(request.Id, cancellationToken);

    return segment == null
      ? new Result<SegmentDto>(new SegmentNotFoundException(request.Id))
      : new SegmentDto(segment.Id, segment.Name);
  }
}
