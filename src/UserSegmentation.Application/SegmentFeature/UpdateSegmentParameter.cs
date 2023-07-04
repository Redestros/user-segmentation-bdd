using LanguageExt.Common;
using MediatR;
using UserSegmentation.Core.SegmentAggregate;
using UserSegmentation.Core.Services;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.SegmentFeature;

public record UpdateSegmentParameterCommand(int SegmentId, int ParameterId, string Value) : IRequest<Result<bool>>;
public class UpdateSegmentParameterHandler : IRequestHandler<UpdateSegmentParameterCommand, Result<bool>>
{
  private readonly IRepository<Core.SegmentAggregate.Segment> _segmentRepository;

  public UpdateSegmentParameterHandler(IRepository<Segment> segmentRepository)
  {
    _segmentRepository = segmentRepository;
  }

  public async Task<Result<bool>> Handle(UpdateSegmentParameterCommand request, CancellationToken cancellationToken)
  {
    var segment =
      await _segmentRepository.SingleOrDefaultAsync(new GetSegmentWithParameterSpec(request.SegmentId),
        cancellationToken);

    if (segment is null)
      return new Result<bool>(new SegmentNotFoundException(request.SegmentId));
    
    segment.UpdateParameterValue(request.ParameterId, request.Value);

    await _segmentRepository.UpdateAsync(segment, cancellationToken);
    return true;
  }
}
