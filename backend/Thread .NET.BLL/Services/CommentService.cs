using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
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
