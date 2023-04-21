using MediatR;

namespace UserSegmentation.Application.Segment.Queries;

public class GetSegmentsQuery : IRequest<List<SegmentDto>>
{
}
