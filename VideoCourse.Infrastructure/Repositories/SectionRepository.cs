using ErrorOr;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common;

namespace VideoCourse.Infrastructure.Repositories;

public class SectionRepository : GenericRepository<Section>, ISectionRepository
{
    public SectionRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ErrorOr<Section>> InsertSection(Section section)
    {
        _dbContext.Insert(section);
        return section;
    }
}