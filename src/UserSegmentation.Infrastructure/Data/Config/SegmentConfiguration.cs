using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Infrastructure.Data.Config;

public class SegmentConfiguration : IEntityTypeConfiguration<Segment>
{
  public void Configure(EntityTypeBuilder<Segment> builder)
  {
    builder.HasKey(s => s.Id);

    builder.HasIndex(s => s.Name).IsUnique();

    builder.HasMany(s => s.Parameters)
      .WithOne(parameter => parameter.Segment);

    builder.Metadata.FindNavigation(nameof(Segment.Parameters))
      ?.SetPropertyAccessMode(PropertyAccessMode.Field);
  }
}
