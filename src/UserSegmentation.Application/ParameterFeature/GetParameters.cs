using MediatR;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Application.ParameterFeature;

public record GetParametersRequest() : IRequest<List<ParameterDto>>;

public class GetParametersHandler : IRequestHandler<GetParametersRequest, List<ParameterDto>>
{

  private readonly IRepository<Core.SegmentAggregate.Parameter> _repository;

  public GetParametersHandler(IRepository<Core.SegmentAggregate.Parameter> repository)
  {
    _repository = repository;
  }

  public async Task<List<ParameterDto>> Handle(GetParametersRequest request, CancellationToken cancellationToken)
  {
    var parameters = await _repository.ListAsync(cancellationToken);
    return parameters.Select(x =>
    {
      ParameterDto dto = x;
      return dto;
    }).ToList();
  }
}
