using MediatR;

namespace UserSegmentation.Application.Segment.Queries;

public record GetSegmentQuery(string Name) : IRequest<SegmentDto>
{
}
