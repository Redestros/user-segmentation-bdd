// using Ardalis.ApiEndpoints;
// using Microsoft.AspNetCore.Mvc;
// using UserSegmentation.Core.UserAggregate;
// using UserSegmentation.SharedKernel.Interfaces;
//
// namespace UserSegmentation.Web.Endpoints.UserEndpoints;
//
// public class Create : EndpointBaseAsync.WithRequest<CreateUserRequest>.WithActionResult<CreateUserResponse>
// {
//   private readonly IRepository<User> _repository;
//
//
//   public Create(IRepository<User> repository)
//   {
//     _repository = repository;
//   }
//
//   public override async Task<ActionResult<CreateUserResponse>> HandleAsync(CreateUserRequest request, 
//     CancellationToken cancellationToken = new())
//   {
//     var user = new User(request.Username, request.FirstName, request.LastName, request.Email, request.PhoneNumber);
//     var createdUser = await _repository.AddAsync(user, cancellationToken);
//     var response = new CreateUserResponse(createdUser.Id, createdUser.Username);
//     return response;
//   }
// }



