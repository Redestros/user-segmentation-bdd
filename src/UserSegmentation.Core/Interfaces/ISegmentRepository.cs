using UserSegmentation.Core.UserAggregate;

namespace UserSegmentation.Core.Interfaces;

public interface ISegmentRepository
{
  List<UserParameter> GetParameters(int segmentId); 
}
