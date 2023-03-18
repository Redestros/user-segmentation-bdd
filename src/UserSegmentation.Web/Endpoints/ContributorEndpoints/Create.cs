using UserSegmentation.Core.ContributorAggregate;
using UserSegmentation.SharedKernel.Interfaces;
using FastEndpoints;

namespace UserSegmentation.Web.Endpoints.ContributorEndpoints;

public class Create : Endpoint<CreateContributorRequest, CreateContributorResponse>
{
  private readonly IRepository<Contributor> _repository;

  public Create(IRepository<Contributor> repository)
  {
    _repository = repository;
  }

  public override void Configure()
  {
    Post(CreateContributorRequest.Route);
    AllowAnonymous();
    Options(x => x
      .WithTags("ContributorEndpoints"));
  }

  public override async Task HandleAsync(
    CreateContributorRequest request,
    CancellationToken cancellationToken)
  {
    if (request.Name == null)
    {
      ThrowError("Name is required");
    }

    var newContributor = new Contributor(request.Name);
    var createdItem = await _repository.AddAsync(newContributor, cancellationToken);
    var response = new CreateContributorResponse
    (
      createdItem.Id,
      createdItem.Name
    );

    await SendAsync(response);
  }
}
