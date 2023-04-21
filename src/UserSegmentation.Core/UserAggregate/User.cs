﻿using UserSegmentation.Core.UserAggregate.Events;
using UserSegmentation.SharedKernel;
using UserSegmentation.SharedKernel.Interfaces;

namespace UserSegmentation.Core.UserAggregate;

public class User : EntityBase, IAggregateRoot
{
  public string Username { get; private set; }
  public string FirstName { get; private set; }
  public string LastName { get; private set; }
  public string Email { get; private set; }
  public string PhoneNumber { get; private set; }
  public SegmentReference? SegmentReference { get; private set; }

  public User(string username, string firstName, string lastName, string email, string phoneNumber)
  {
    Username = username;
    FirstName = firstName;
    LastName = lastName;
    Email = email;
    PhoneNumber = phoneNumber;
  }

  public void AssignToSegment(SegmentReference reference)
  {
    SegmentReference = reference;
    RegisterDomainEvent(new UsedAssignedToSegmentEvent(Id, reference));
  }
}
