using ErrorOr;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Repositories;

public interface IVideoRepository : IRepository<Video>
{
    Task<ErrorOr<Video>> Create(Video video);
    Task<ErrorOr<Video>> GetById(Guid id);
    Task<ErrorOr<Video>> GetByIdWithSections(Guid id);
    Task<ErrorOr<Video>> GetByIdWithCreator(Guid id);
    Task<IEnumerable<Video>> GetAllVideos();
    ErrorOr<Video> Update(Video video);
    Task<ErrorOr<Section>> AddSection(Section section);
    Task<ErrorOr<bool>> AddSections(IReadOnlyCollection<Section> sections);
    Task<ErrorOr<Section>> GetSectionById(Guid id);
    Task<ErrorOr<bool>> RemoveSection(Section section);
    Task<IEnumerable<Video>> GetVideosByCreatorId(Guid id);
    Task<ErrorOr<Note>> AddNote(Note note);
    Task<ErrorOr<Question>> AddQuestion(Question question);
    Task<ErrorOr<Video>> GetVideoWithAllContentById(Guid id);
    Task<IEnumerable<Video>> GetVideosByParameters(Guid? creatorId, DateTime? startDate, DateTime? endDate, int page,
        int pageLimit);

    Task<ErrorOr<Section>> UpdateSection(Section section);

    Task<int?> GetTotalVideos();
}