using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UserSegmentation.Core.SegmentAggregate;

namespace UserSegmentation.Infrastructure.Data.Config;

public class ParameterConfiguration : IEntityTypeConfiguration<Parameter>
{
  public void Configure(EntityTypeBuilder<Parameter> builder)
  {
    builder.HasKey(p => p.Id);
    
  }
}
