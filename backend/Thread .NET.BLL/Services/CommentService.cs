using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thread_.NET.BLL.Exceptions;
using Thread_.NET.BLL.Services.Abstract;
using Thread_.NET.Common.DTO.Comment;
using Thread_.NET.DAL.Context;
using Thread_.NET.DAL.Entities;

namespace Thread_.NET.BLL.Services
{
    public sealed class CommentService : BaseService
    {
        public CommentService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ICollection<CommentDTO>> GetPostComments(int postId, int currentUserId)
        {
            var comments = await _context.Comments
                .Where(c => c.PostId == postId)
                .Include(post => post.Author)
                    .ThenInclude(a => a.Avatar)
                .OrderByDescending(post => post.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

            ICollection<CommentDTO> dtos = _mapper.Map<ICollection<CommentDTO>>(comments);
            var commentsIds = comments.Select(c => c.Id);

            // get number of likes and dislikes in a single query
            var commentReactions = _context.CommentReactions
                .Where(r => commentsIds.Contains(r.CommentId))
                .IgnoreQueryFilters()
                .GroupBy(p => p.CommentId).Select(g => new
                {
                    CommentId = g.Key,
                    LikesNumber = g.Count(r => r.IsLike == true),
                    DislikesNumber = g.Count(r => r.IsLike == false),
                    IsLikedByMe = g.Count(r => r.IsLike == true && r.UserId == currentUserId) > 0,
                    IsDislikedByMe = g.Count(r => r.IsLike == false && r.UserId == currentUserId) > 0,
                });

            // left outer join of dtos and reactions
            dtos = dtos.GroupJoin(commentReactions, d => d.Id, a => a.CommentId,
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

            return dtos;
        }

        public async Task<CommentDTO> CreateComment(NewCommentDTO newComment)
        {
            var commentEntity = _mapper.Map<Comment>(newComment);

            _context.Comments.Add(commentEntity);
            await _context.SaveChangesAsync();

            // Include Author info
            var author = await _context.Users.Include(x => x.Avatar).FirstAsync(x => x.Id == commentEntity.AuthorId);
            commentEntity.Author = author;

            var createdComment = await _context.Comments
                .Include(comment => comment.Author)
                    .ThenInclude(user => user.Avatar)
                .FirstAsync(comment => comment.Id == commentEntity.Id);

            return _mapper.Map<CommentDTO>(createdComment);
        }

        public async Task UpdateComment(CommentUpdateDTO commentDto)
        {
            var commentEntity = await _context.Comments
                .FirstOrDefaultAsync(p => p.Id == commentDto.Id);

            if (commentEntity == null)
                throw new NotFoundException(nameof(Comment), commentDto.Id);

            commentEntity.Body = commentDto.Body;
            commentEntity.UpdatedAt = DateTime.Now;

            _context.Comments.Update(commentEntity);
            await _context.SaveChangesAsync();

            // var updatedCommentDTO = _mapper.Map<CommentDTO>(commentEntity);
            // await _commentHub.Clients.All.SendAsync("UpdateComment", updatedCommentDTO);
        }
    }
}
