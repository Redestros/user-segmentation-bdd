namespace UserSegmentation.Application.SegmentFeature;

public record SegmentDetailDto(int Id, string Name, List<SegmentParameterDto> Parameters);
