using ErrorOr;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Repositories;

public interface IVideoRepository
{
    Task<ErrorOr<Video>> Create(Video video);
    Task<ErrorOr<Video>> GetById(Guid id);
    Task<IEnumerable<Video>> GetAllVideos();

    ErrorOr<Video> Update(Video video);
}