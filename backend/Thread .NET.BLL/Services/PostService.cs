using System;
using AutoMapper;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thread_.NET.BLL.Exceptions;
using Thread_.NET.BLL.Hubs;
using Thread_.NET.BLL.Services.Abstract;
using Thread_.NET.Common.DTO.Post;
using Thread_.NET.DAL.Context;
using Thread_.NET.DAL.Entities;
using Thread_.NET.Common.DTO.User;

namespace Thread_.NET.BLL.Services
{
    public sealed class PostService : BaseService
    {
        private readonly IHubContext<PostHub> _postHub;

        public PostService(ThreadContext context, IMapper mapper, IHubContext<PostHub> postHub) : base(context, mapper)
        {
            _postHub = postHub;
        }

        private IQueryable<Post> GetPostsQuery()
        {
            return _context.Posts
                .Include(post => post.Author)
                    .ThenInclude(author => author.Avatar)
                .Include(post => post.Preview)
                .OrderByDescending(post => post.CreatedAt)
                .AsNoTracking();
        }

        public async Task<ICollection<PostDTO>> GetAllPosts(int currentUserId)
        {
            var posts = await GetPostsQuery().ToListAsync();

            ICollection<PostDTO> dtos = _mapper.Map<ICollection<PostDTO>>(posts);

            // get number of likes and dislikes in a single query
            var postsReactions = _context.PostReactions
                .IgnoreQueryFilters()
                .GroupBy(p => p.PostId).Select(g => new
                {
                    PostId = g.Key,
                    LikesNumber = g.Count(r => r.IsLike == true),
                    DislikesNumber = g.Count(r => r.IsLike == false),
                    IsLikedByMe = g.Count(r => r.IsLike == true && r.UserId == currentUserId) > 0,
                    IsDislikedByMe = g.Count(r => r.IsLike == false && r.UserId == currentUserId) > 0,
                });

            // left outer join of dtos and reactions
            dtos = dtos.GroupJoin(postsReactions, d => d.Id, a => a.PostId,
                    (dto, reactions) => new { Dto = dto, Reactions = reactions })
                .SelectMany(x => x.Reactions.DefaultIfEmpty(), (dto, reactions) =>
                {
                    dto.Dto.LikesNumber = reactions?.LikesNumber ?? 0;
                    dto.Dto.DislikesNumber = reactions?.DislikesNumber ?? 0;
                    dto.Dto.IsLikedByMe = reactions is not null
                        ? (reactions.IsLikedByMe ? true : reactions.IsDislikedByMe ? false : null)
                        : null;
                    return dto.Dto;
                }).ToList();

            // get number of comments in a single query
            var commentsNumber = _context.Comments
                .IgnoreQueryFilters()
                .GroupBy(p => p.PostId).Select(g => new
                {
                    PostId = g.Key,
                    CommentsNumber = g.Count()
                });

            // left outer join of dtos and comments numbers
            dtos = dtos.GroupJoin(commentsNumber, d => d.Id, a => a.PostId,
                    (dto, comments) => new { Dto = dto, Comments = comments })
                .SelectMany(x => x.Comments.DefaultIfEmpty(), (dto, comments) =>
                {
                    dto.Dto.CommentsNumber = comments?.CommentsNumber ?? 0;
                    return dto.Dto;
                }).ToList();

            return dtos;
        }

        public async Task<ICollection<PostDTO>> GetAllPosts()
        {
            var posts = await GetPostsQuery().ToListAsync();

            ICollection<PostDTO> dtos = _mapper.Map<ICollection<PostDTO>>(posts);

            // get number of likes and dislikes in a single query
            var postsReactions = _context.PostReactions
                .IgnoreQueryFilters()
                .GroupBy(p => p.PostId).Select(g => new
                {
                    PostId = g.Key,
                    LikesNumber = g.Count(r => r.IsLike == true),
                    DislikesNumber = g.Count(r => r.IsLike == false),
                });

            // left outer join of dtos and reactions
            dtos = dtos.GroupJoin(postsReactions, d => d.Id, a => a.PostId,
                    (dto, reactions) => new { Dto = dto, Reactions = reactions })
                .SelectMany(x => x.Reactions.DefaultIfEmpty(), (dto, reactions) =>
                {
                    dto.Dto.LikesNumber = reactions?.LikesNumber ?? 0;
                    dto.Dto.DislikesNumber = reactions?.DislikesNumber ?? 0;
                    return dto.Dto;
                }).ToList();

            return dtos;
        }

        public async Task<PostDTO> CreatePost(PostCreateDTO postDto)
        {
            var postEntity = _mapper.Map<Post>(postDto);

            _context.Posts.Add(postEntity);
            await _context.SaveChangesAsync();

            var createdPost = await _context.Posts
                .Include(post => post.Author)
                    .ThenInclude(author => author.Avatar)
                .FirstAsync(post => post.Id == postEntity.Id);

            // Include Author info
			var author = await _context.Users.Include(x => x.Avatar).FirstAsync(x => x.Id == postEntity.AuthorId);
            postEntity.Author = author;

            var createdPostDTO = _mapper.Map<PostDTO>(createdPost);
            await _postHub.Clients.All.SendAsync("NewPost", createdPostDTO);

            return createdPostDTO;
        }

        public async Task UpdatePost(PostUpdateDTO postDto)
        {
            var postEntity = await _context.Posts
                .Include(u => u.Preview)
                .FirstOrDefaultAsync(p => p.Id == postDto.Id); ;

            if (postEntity == null)
                throw new NotFoundException(nameof(Post), postDto.Id);

            var timeNow = DateTime.Now;
            postEntity.Body = postDto.Body;
            postEntity.UpdatedAt = timeNow;

            if (!string.IsNullOrEmpty(postDto.PreviewImage))
            {
                if (postEntity.Preview == null)
                {
                    postEntity.Preview = new Image
                    {
                        URL = postDto.PreviewImage
                    };
                }
                else
                {
                    postEntity.Preview.URL = postDto.PreviewImage;
                    postEntity.Preview.UpdatedAt = timeNow;
                }
            }
            else
            {
                if (postEntity.Preview != null)
                {
                    _context.Images.Remove(postEntity.Preview);
                }
            }

            _context.Posts.Update(postEntity);
            await _context.SaveChangesAsync();

            var updatedPostDTO = _mapper.Map<PostDTO>(postEntity);
            await _postHub.Clients.All.SendAsync("UpdatePost", updatedPostDTO);
        }

        public async Task DeletePost(int postId)
        {
            var postEntity = await _context.Posts.FirstOrDefaultAsync(u => u.Id == postId);

            if (postEntity == null)
            {
                throw new NotFoundException(nameof(User), postId);
            }

            _context.Posts.Remove(postEntity);
            await _context.SaveChangesAsync();

            await _postHub.Clients.All.SendAsync("DeletePost", postId);
        }
    }
}
