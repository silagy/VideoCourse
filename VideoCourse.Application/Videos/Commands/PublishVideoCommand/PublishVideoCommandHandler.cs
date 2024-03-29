﻿using ErrorOr;
using Mapster;
using MediatR;
using VideoCourse.Application.Core.Abstractions.Common;
using VideoCourse.Application.Core.Abstractions.Data;
using VideoCourse.Application.Core.Abstractions.Repositories;
using VideoCourse.Application.Videos.Common;
using VideoCourse.Domain.DomainErrors;

namespace VideoCourse.Application.Videos.Commands.PublishVideoCommand;

public class PublishVideoCommandHandler : IRequestHandler<PublishVideoCommand, ErrorOr<BasicVideoResponse>>
{
    private readonly IVideoRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IDateTime _dateTimeProvider;

    public PublishVideoCommandHandler(IVideoRepository repository, IUnitOfWork unitOfWork, IDateTime dateTimeProvider)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
        _dateTimeProvider = dateTimeProvider;
    }

    public async Task<ErrorOr<BasicVideoResponse>> Handle(PublishVideoCommand request, CancellationToken cancellationToken)
    {
        // Get the video from the database
        var videoRequest = await _repository.GetById(request.Id);

        if (videoRequest.IsError)
        {
            return videoRequest.Errors;
        }
        
        var video = videoRequest.Value;
        if (video.IsPublished)
        {
            return CustomErrors.Video.VideoIsAlreadyPublished;
        }
        video.PublishVideo(_dateTimeProvider.UtcNow);

        await _unitOfWork.Commit();

        return video.Adapt<BasicVideoResponse>();
    }
}