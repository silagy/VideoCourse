using System.Linq.Expressions;
using Org.BouncyCastle.Asn1.X509.SigI;
using VideoCourse.Domain.Entities;

namespace VideoCourse.Infrastructure.Specifications;

public class GetVideoByIdWithSectionsSpecifications : Specification<Video>
{
    public GetVideoByIdWithSectionsSpecifications(Guid id) 
        : base(video => video.Id == id)
    {
        AddInclude(v => v.Sections);
    }
}