using AutoMapper;
using Thread_.NET.Common.DTO.Post;
using Thread_.NET.DAL.Entities;

namespace Thread_.NET.BLL.MappingProfiles
{
    public sealed class PostProfile : Profile
    {
        public PostProfile()
        {
            CreateMap<Post, PostDTO>()
                .ForMember(dest => dest.PreviewImage, src => src.MapFrom(x => x.Preview != null ? x.Preview.URL : string.Empty))
                .ForMember(dest => dest.IsUpdated, src => src.MapFrom(x => x.CreatedAt != x.UpdatedAt));

            CreateMap<PostCreateDTO, Post>()
                .ForMember(dest => dest.Preview, src => src.MapFrom(s => string.IsNullOrEmpty(s.PreviewImage) ? null : new Image { URL = s.PreviewImage }));
        }
    }
}
