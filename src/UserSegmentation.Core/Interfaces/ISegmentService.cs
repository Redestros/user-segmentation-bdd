using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Core.Interfaces;

public interface ISegmentService
{
  Task CreateDefaultSegment();
  Task<Segment> GetDefaultSegment();
  Task<Segment> Get(string name);
  Task OverrideParameterValue(int segmentId, int parameterId, string value);
}
