using ErrorOr;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
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
        _dbContext.Insert(video);
        return video;
    }

    public async Task<ErrorOr<Video>> GetById(Guid id)
    {
        var result = await _dbContext.GetByIdAsync<Video>(id);
        if (result.Value is null)
        {
            return CustomErrors.Entity.EntityNotFound;
        }
        return result;
    }

    public async Task<ErrorOr<Video>> GetByIdWithSections(Guid id)
    {
        var result = await _dbContext.Set<Video>()
            .Include(v => v.Sections)
            .FirstOrDefaultAsync(v => v.Id == id);

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
        //_dbContext.Set<Video>().Attach(video);
        var result = _dbContext.Set<Video>().Update(video);
        
        return result.Entity;
    }

    public async Task<ErrorOr<Section>> AddSection(Section section)
    {
        _dbContext.Insert(section);
        return section;
    }

    public async Task<ErrorOr<bool>> AddSections(IReadOnlyCollection<Section> sections)
    {
        var result = await _dbContext.InsertRange(sections);
        return result;
    }

    public async Task<ErrorOr<bool>> Remove(Video video)
    {
        return await _dbContext.Remove(video);
    }

    public async Task<ErrorOr<Section>> GetSectionById(Guid id)
    {
        var result = await _dbContext.GetByIdAsync<Section>(id);

        if (result.IsError)
        {
            return result.Errors;
        }else if (result.Value is null)
        {
            return CustomErrors.Entity.EntityNotFound;
        }

        return result.Value;
    }

    public async Task<ErrorOr<bool>> RemoveSection(Section section)
    {
        return await _dbContext.Remove(section);
    }
}