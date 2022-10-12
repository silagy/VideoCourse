namespace VideoCourse.Application.Core.Abstractions.Data;

public interface IUnitOfWork
{
    Task<int> Commit();
    Task<bool> Dispose();
}