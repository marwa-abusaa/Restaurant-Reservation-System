using Microsoft.EntityFrameworkCore;
using RestaurantReservation.API.Models.Paging;

namespace RestaurantReservation.API.Services;

public class PaginationHelper<TEntity>
{
    public static async Task<PagedResult<TEntity>> GetPagedResult(IQueryable<TEntity> query, int pageNumber, int pageSize, int MaxPageSize)
    {
        if (pageSize > MaxPageSize) pageSize = MaxPageSize;

        var totalCount = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

        if (pageNumber > totalPages && totalPages != 0)
            pageNumber = totalPages;

        var skip = (pageNumber - 1) * pageSize;

        var items = await query
            .Skip(skip)
            .Take(pageSize)
            .ToListAsync();

        return new PagedResult<TEntity>
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalCount = totalCount,
            TotalPages = totalPages,
            HasNextPage = pageNumber < totalPages,
            HasPreviousPage = pageNumber > 1,
            Items = items
        };
    }
}
