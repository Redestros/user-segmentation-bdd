using System.Linq;
using System.Threading.Tasks;
using Ardalis.Specification;
using UserSegmentation.Core.ContributorAggregate;
using UserSegmentation.Core.Services;
using UserSegmentation.SharedKernel.Interfaces;
using MediatR;
using Moq;
using Xunit;

namespace UserSegmentation.UnitTests.Core.Services;

public class DeleteContributorService_DeleteContributor
{
  private readonly Mock<IRepository<Contributor>> _mockRepo = new();
  private readonly Mock<IMediator> _mockMediator = new();
  private readonly DeleteContributorService _service;

  public DeleteContributorService_DeleteContributor()
  {
    _service = new DeleteContributorService(_mockRepo.Object, _mockMediator.Object);
  }

  [Fact]
  public async Task ReturnsNotFoundGivenCantFindContributor()
  {
    var result = await _service.DeleteContributor(0);

    Assert.Equal(Ardalis.Result.ResultStatus.NotFound, result.Status);
  }
}
