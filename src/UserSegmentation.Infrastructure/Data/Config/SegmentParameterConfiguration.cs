using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Infrastructure.Data.Config;

public class SegmentParameterConfiguration : IEntityTypeConfiguration<SegmentParameter>
{
  public void Configure(EntityTypeBuilder<SegmentParameter> builder)
  {
    // builder.HasKey(sp => new { sp.SegmentId, sp.ParameterId });
    
    builder.Metadata.FindNavigation(nameof(Parameter))
      ?.SetPropertyAccessMode(PropertyAccessMode.Field);
    
    builder.Metadata.FindNavigation(nameof(Segment))
      ?.SetPropertyAccessMode(PropertyAccessMode.Field);

  }
}

