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

    builder.Metadata.FindNavigation(nameof(Segment.Parameters))
      ?.SetPropertyAccessMode(PropertyAccessMode.Field);

    builder.HasMany(s => s.Parameters)
      .WithMany()
      .UsingEntity<SegmentParameter>(
        l => l.HasOne(segmentParameter => segmentParameter.Parameter)
          .WithMany()
          .HasForeignKey(x => x.ParameterId),
        r => r.HasOne(segmentParameter => segmentParameter.Segment)
          .WithMany(s => s.SegmentParameters)
          .HasForeignKey(x => x.SegmentId)
   
      );
  }
}
