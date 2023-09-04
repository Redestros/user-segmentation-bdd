using UserSegmentation.Core.UserAggregate;

namespace UserSegmentation.Core.Interfaces;

public interface ISegmentAssignmentRule
{
  /// <summary>
  /// Perform business checks for user attributes to assign the appropriate segment 
  /// </summary>
  /// <param name="user"></param>
  /// <returns>Segment id</returns>
  Task<int> Evaluate(User user);

  int Order() => 1;
}
