using Heimdallr.Application.Common.Entities;
using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Common.Interfaces.Persistent;
using Heimdallr.Application.Common.Monads;
using Heimdallr.Application.Contracts.Meters.Dtos;
using Heimdallr.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Heimdallr.Application.Contracts.Meters.Queries;

internal sealed class GetMetersPageQueryHandler(IApplicationDbContext dbContext)
    : IQueryHandler<GetMetersPageQuery, MetersPageDto>
{
    public async Task<Result<MetersPageDto>> Handle(GetMetersPageQuery query, CancellationToken cancellationToken)
    {
        Result validationResult = Validate(query);

        if (validationResult.IsFailure)
        {
            return Result.Failure<MetersPageDto>(validationResult.Error);
        }
        
        int totalMetersCount = await dbContext.Meters.CountAsync(cancellationToken);

        int totalPagesCount = Convert.ToInt32(Math.Ceiling((double)totalMetersCount / query.ItemsOnPage));

        int requestedPage = Math.Clamp(query.Page, 1, totalPagesCount);

        List<Meter> items = await dbContext.Meters
            .Skip((requestedPage - 1) * query.ItemsOnPage)
            .Take(query.ItemsOnPage)
            .ToListAsync(cancellationToken);

        return Result.Success(new MetersPageDto(items.ToDto(),
            totalPagesCount,
            totalMetersCount,
            requestedPage));
    }

    private Result Validate(GetMetersPageQuery query)
    {
        if (query.ItemsOnPage < 1)
        {
            return Result.Failure(Error.Failure("Meters.Validation", "ItemsOnPage must be greater than 0"));
        }
        
        return Result.Success();
    }
}

static file class Mapper
{
    extension(ICollection<Meter> meters)
    {
        public IReadOnlyCollection<MeterDto> ToDto() =>
            meters.Select(x => new MeterDto(
                    x.Id,
                    x.Name,
                    x.Model,
                    x.SerialNumber,
                    x.IsEnabled,
                    x.CreatedAt,
                    x.UpdatedAt))
                .ToList()
                .AsReadOnly();
    }
}
