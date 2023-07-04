#region

using Ardalis.Specification;
using LanguageExt.Common;
using MediatR;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.Services;
using UserSegmentation.SharedKernel.Interfaces;

#endregion

namespace UserSegmentation.Application.SegmentFeature;

public record GetSegmentByNameQuery(string Name) : IRequest<Result<SegmentDetailDto>>;

internal class GetSegmentByNameHandler : IRequestHandler<GetSegmentByNameQuery, Result<SegmentDetailDto>>
{
  private readonly IRepository<Segment> _repository;

  public GetSegmentByNameHandler(IRepository<Segment> repository)
  {
    _repository = repository;
  }

  public async Task<Result<SegmentDetailDto>> Handle(GetSegmentByNameQuery request, CancellationToken cancellationToken)
  {
    var segment = await _repository.FirstOrDefaultAsync(
      new GetSegmentByNameSpec(request), cancellationToken);

    return segment == null
      ? new Result<SegmentDetailDto>(new SegmentNotFoundException(request.Name))
      : SegmentMapper.Map(segment);
  }
}

public sealed class GetSegmentByNameSpec : Specification<Segment>
{
  public GetSegmentByNameSpec(GetSegmentByNameQuery byNameQuery)
  {
    Query.Where(s => s.Name.Equals(byNameQuery.Name));
    Query.Include(s => s.SegmentParameters);
    Query.Include(s => s.Parameters);
  }
}

public record GetSegmentByIdQuery(int Id) : IRequest<Result<SegmentDetailDto>>;

internal class GetSegmentByIdHandler : IRequestHandler<GetSegmentByIdQuery, Result<SegmentDetailDto>>
{
  private readonly IRepository<Segment> _repository;

  public GetSegmentByIdHandler(IRepository<Segment> repository)
  {
    _repository = repository;
  }

  public async Task<Result<SegmentDetailDto>> Handle(GetSegmentByIdQuery request, CancellationToken cancellationToken)
  {
    var segment =
      await _repository.SingleOrDefaultAsync(new GetSegmentWithParameterSpec(request.Id), cancellationToken);

    return segment == null
      ? new Result<SegmentDetailDto>(new SegmentNotFoundException(request.Id))
      : SegmentMapper.Map(segment);
  }
}
