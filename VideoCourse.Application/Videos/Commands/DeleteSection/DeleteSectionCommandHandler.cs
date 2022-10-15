using ErrorOr;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;

namespace VideoCourse.Application.Videos.Commands.DeleteSection;

public class DeleteSectionCommandHandler : IRequestHandler<DeleteSectionCommand, ErrorOr<bool>>
{
    private readonly IVideoRepository _videoRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeleteSectionCommandHandler(IVideoRepository videoRepository, IUnitOfWork unitOfWork)
    {
        _videoRepository = videoRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ErrorOr<bool>> Handle(DeleteSectionCommand request, CancellationToken cancellationToken)
    {
        // Get the section
        var sectionResult = await _videoRepository.GetSectionById(request.Id);

        if (sectionResult.IsError)
        {
            return sectionResult.Errors;
        }

        var section = sectionResult.Value;

        var deleteOperation = await _videoRepository.RemoveSection(section);
        await _unitOfWork.Commit();

        return deleteOperation;
    }
}