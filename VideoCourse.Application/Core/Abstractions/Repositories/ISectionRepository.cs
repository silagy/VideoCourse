using ErrorOr;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Application.Core.Abstractions.Repositories;

public interface ISectionRepository
{
    Task<ErrorOr<Section>> InsertSection(Section section);
}