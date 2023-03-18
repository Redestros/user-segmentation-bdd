using Ardalis.Result;

namespace UserSegmentation.Core.Interfaces;

public interface IDeleteContributorService
{
  public Task<Result> DeleteContributor(int contributorId);
}
