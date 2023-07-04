
using UserSegmentation.Application.Exceptions;

namespace UserSegmentation.Application.ParameterFeature;

public class ParameterNotFoundException : NotFoundException
{
  public ParameterNotFoundException(string name) : base($"Parameter with name {name} not found")
  {
  }
  
  public ParameterNotFoundException(int id) : base($"Parameter with id {id} not found")
  {
  }
}
