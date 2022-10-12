using ErrorOr;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common;

namespace VideoCourse.Infrastructure.Repositories;

public class VideoRepository : GenericRepository<Video>, IVideoRepository
{
    public VideoRepository(IDbContext dbContext) : base(dbContext)
    {
    }

    public async Task<ErrorOr<Video>> Create(Video video)
    {
        var result = await _dbContext.Insert(video);
        return result;
    }

    public async Task<ErrorOr<Video>> GetById(Guid id)
    {
        var result = await _dbContext.GetByIdAsync<Video>(id);
        return result;
    }

    public async Task<IEnumerable<Video>> GetAllVideos()
    {
        // I should implement Dapper here
        var results = await _dbContext.Set<Video>().ToListAsync();
        return results;
    }

    public ErrorOr<Video> Update(Video video)
    {
        var result = _dbContext.Set<Video>().Update(video);
        
        return result.Entity;
    }
}