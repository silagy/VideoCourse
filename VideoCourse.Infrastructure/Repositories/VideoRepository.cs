using Dapper;
using ErrorOr;
using Microsoft.EntityFrameworkCore;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Domain.DomainErrors;
using VideoCourse.Domain.Entities;
using VideoCourse.Infrastructure.Common;
using VideoCourse.Infrastructure.Common.Queries;
using VideoCourse.Infrastructure.Specifications;

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
            .AddSpecification(new GetVideoByIdWithSectionsSpecifications(id))
            .FirstOrDefaultAsync(v => v.Id == id);

        return result;
    }

    public async Task<ErrorOr<Video>> GetByIdWithCreator(Guid id)
    {
        Video? result = await _dbContext.Set<Video>()
            .AddSpecification(new GetVideoByIdWithCreator(id))
            .FirstOrDefaultAsync(v => v.Id == id);

        if (result is null)
        {
            return CustomErrors.Entity.EntityNotFound;
        }

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

    // public async Task<ErrorOr<bool>> Remove(Video video)
    // {
    //     return await _dbContext.Remove(video);
    // }

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

    public async Task<IEnumerable<Video>> GetVideosByCreatorId(Guid id)
    {
        var results = await _dbContext.GetRecordsUsingRawSqlAsync<Video>(
            query: QueriesRepository.Videos.GetVideosByCreatorId,
            parameters: new { CreatorId = id });

        return results;
    }

    public async Task<ErrorOr<Note>> AddNote(Note note)
    {
        return await _dbContext.Insert(note);
    }

    public async Task<ErrorOr<Question>> AddQuestion(Question question)
    {
        return await _dbContext.Insert(question);
    }

    public async Task<ErrorOr<Video>> GetVideoWithAllContentById(Guid id)
    {
        SqlMapper.GridReader results = await _dbContext.GetRecordUsingMultipleQueries(
            QueriesRepository.Videos.GetVideoWithAllItemsMultipleQuery,
            new { VideoId = id });

        Video? video = await results.ReadFirstOrDefaultAsync<Video>();
        if (video is null) return CustomErrors.Entity.EntityNotFound;
        video.SetSections(await results.ReadAsync<Section>());
        video.SetNotes(await results.ReadAsync<Note>());
        video.SetQuestions(await results.ReadAsync<Question>());

        return video;
    }

    public async Task<IEnumerable<Video>> GetVideosByParameters(Guid? creatorId, DateTime? startDate, DateTime? endDate, int page, int pageLimit)
    {
        IEnumerable<Video> results = await _dbContext.GetRecordsUsingRawSqlAsync<Video>(
                QueriesRepository.Videos.GetVideosWithParameters,
                new
                {
                    CreatorId = creatorId,
                    StartDate = startDate,
                    EndDate = endDate,
                    PageSize = pageLimit,
                    Page = (page - 1) * pageLimit
                });

            return results;
    }

    public async Task<ErrorOr<Section>> UpdateSection(Section section)
    {
        var entityEntry = _dbContext.Set<Section>().Update(section);
        return await Task.FromResult(entityEntry.Entity);
    }

    public async Task<int?> GetTotalVideos()
    {
        return await _dbContext.Set<Video>().CountAsync();
    }
}