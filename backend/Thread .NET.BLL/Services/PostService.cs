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

namespace Thread_.NET.BLL.Services
{
    public sealed class PostService : BaseService
    {
        private readonly IHubContext<PostHub> _postHub;

        public PostService(ThreadContext context, IMapper mapper, IHubContext<PostHub> postHub) : base(context, mapper)
        {
            _postHub = postHub;
        }

        public async Task<ICollection<PostDTO>> GetAllPosts()
        {
            var posts = await _context.Posts
                .Include(post => post.Author)
                    .ThenInclude(author => author.Avatar)
                .Include(post => post.Preview)
                .Include(post => post.Reactions)
                    .ThenInclude(reaction => reaction.User)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Reactions)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Author)
                .OrderByDescending(post => post.CreatedAt)
                .ToListAsync();

            return _mapper.Map<ICollection<Post>, ICollection<PostDTO>>(posts);
        }

        public async Task<ICollection<PostDTO>> GetAllPosts(int userId)
        {
            var posts = await _context.Posts
                .Include(post => post.Author)
                    .ThenInclude(author => author.Avatar)
                .Include(post => post.Preview)
                .Include(post => post.Comments)
                    .ThenInclude(comment => comment.Author)
                .Where(p => p.AuthorId == userId) // Filter here
                .ToListAsync();

            return _mapper.Map<ICollection<PostDTO>>(posts);
        }

        public async Task<PostDTO> CreatePost(PostCreateDTO postDto)
        {
            var postEntity = _mapper.Map<Post>(postDto);

            _context.Posts.Add(postEntity);
            await _context.SaveChangesAsync();

<<<<<<< HEAD
            var createdPost = await _context.Posts
                .Include(post => post.Author)
					.ThenInclude(author => author.Avatar)
                .FirstAsync(post => post.Id == postEntity.Id);
=======
            // Include Author info
			var author = await _context.Users.Include(x => x.Avatar).FirstAsync(x => x.Id == postEntity.AuthorId);
            postEntity.Author = author;
>>>>>>> f0f528d (Added logic)

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
        }
    }
}
