using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserSegmentation.Core.UserAggregate;

namespace UserSegmentation.Infrastructure.Data.Config;

public class UserConfiguration : IEntityTypeConfiguration<User>
{
  public void Configure(EntityTypeBuilder<User> builder)
  {
    builder.HasKey(u => u.Id);

    builder.HasIndex(user => user.Username).IsUnique();

    builder.OwnsOne(user => user.SegmentReference);
  }
}
