using LanguageExt.Common;
using MediatR;
using UserSegmentation.Application.ParameterFeature;
using UserSegmentation.Core.Services;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.SegmentFeature;

public record CreateSegmentParameterCommand(int SegmentId, int ParameterId, string Value) : IRequest<Result<bool>>;

public class CreateSegmentParameterHandler : IRequestHandler<CreateSegmentParameterCommand, Result<bool>>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _segmentRepository;
  private readonly IRepository<Core.SegmentAggregate.Parameter> _parameterRepository;

  public CreateSegmentParameterHandler(IRepository<Core.SegmentAggregate.Segment> segmentRepository,
    IRepository<Core.SegmentAggregate.Parameter> parameterRepository)
  {
    _segmentRepository = segmentRepository;
    _parameterRepository = parameterRepository;
  }

  public async Task<Result<bool>> Handle(CreateSegmentParameterCommand request, CancellationToken cancellationToken)
  {
    var segment =
      await _segmentRepository.SingleOrDefaultAsync(new GetSegmentWithParameterSpec(request.SegmentId),
        cancellationToken);
    if (segment is null)
      return new Result<bool>(new SegmentNotFoundException(request.SegmentId));

    var parameter = await _parameterRepository.GetByIdAsync(request.ParameterId, cancellationToken);

    if (parameter is null)
    {
      return new Result<bool>(new ParameterNotFoundException(request.ParameterId));
    }

    if (segment.Parameters.Any(x => x.Id == parameter.Id))
    {
      return new Result<bool>(new SegmentHasParameterException(parameter.Name));
    }

    segment.AddParameter(parameter);
    await _segmentRepository.UpdateAsync(segment, cancellationToken);

    segment.UpdateParameterValue(request.ParameterId, request.Value);

    await _segmentRepository.UpdateAsync(segment, cancellationToken);
    return true;
  }
}
