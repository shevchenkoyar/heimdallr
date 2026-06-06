using Heimdallr.Application.Common.Interfaces.Contracts;
using Heimdallr.Application.Contracts.Meters.Dtos;

namespace Heimdallr.Application.Contracts.Meters.Queries;

public sealed record GetMetersPageQuery(
    int ItemsOnPage,
    int Page
    ) : IQuery<MetersPageDto>;
