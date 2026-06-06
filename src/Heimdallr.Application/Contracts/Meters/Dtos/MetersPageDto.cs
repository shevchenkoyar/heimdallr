namespace Heimdallr.Application.Contracts.Meters.Dtos;

public sealed class MetersPageDto(IReadOnlyCollection<MeterDto> meters, int pageCount, int totalItems, int itemsOnPage)
{
    public IReadOnlyCollection<MeterDto> Meters { get; } = meters;

    public int PageCount { get; } = pageCount;

    public int TotalItems { get; } = totalItems;

    public int ItemsOnPage { get; } = itemsOnPage;
}
