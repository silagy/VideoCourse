using ErrorOr;
using MediatR;
using VideoCourse.Application.Videos.Commands.CreateVideo;
using VideoCourse.Application.Videos.Common;

namespace VideoCourse.Application.Core.Behaviors;

public class CreateVideoBehavior : IPipelineBehavior<CreateVideoCommand, ErrorOr<BasicVideoResponse>>
{
    public async Task<ErrorOr<BasicVideoResponse>> Handle(CreateVideoCommand request, RequestHandlerDelegate<ErrorOr<BasicVideoResponse>> next, CancellationToken cancellationToken)
    {

        var result = await next();

        return result;
    }
}