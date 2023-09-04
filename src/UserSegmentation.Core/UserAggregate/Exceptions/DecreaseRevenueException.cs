namespace UserSegmentation.Core.UserAggregate.Exceptions;

public class DecreaseRevenueException : Exception
{
  public DecreaseRevenueException() : base("Cannot decrease Gross annual revenue")
  {
    
  }
}
