using ErrorOr;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Repositories;

public interface IVideoRepository
{
    Task<ErrorOr<Video>> Create(Video video);
    Task<ErrorOr<Video>> GetById(Guid id);
    Task<ErrorOr<Video>> GetByIdWithSections(Guid id);
    Task<IEnumerable<Video>> GetAllVideos();
    ErrorOr<Video> Update(Video video);
    Task<ErrorOr<Section>> AddSection(Section section);
    Task<ErrorOr<bool>> AddSections(IReadOnlyCollection<Section> sections);
}