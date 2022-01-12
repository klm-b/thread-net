using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Thread_.NET.BLL.Exceptions;
using Thread_.NET.BLL.Services.Abstract;
using Thread_.NET.Common.DTO.Reaction;
using Thread_.NET.DAL.Context;

namespace Thread_.NET.BLL.Services
{
    public sealed class ReactionService : BaseService
    {
        public ReactionService(ThreadContext context, IMapper mapper) : base(context, mapper) { }

        public async Task<ICollection<ReactionDTO>> GetPostReactions(int postId)
        {
            var reactions = await _context.PostReactions
                .Include(r => r.User)
                    .ThenInclude(u => u.Avatar)
                .Where(r => r.PostId == postId)
                .OrderByDescending(r => r.CreatedAt)
                .AsNoTracking()
                .ToListAsync();

            return _mapper.Map<ICollection<ReactionDTO>>(reactions);
        }
        
        public async Task ReactToPost(NewReactionDTO newReaction)
        {
            var reaction = await _context.PostReactions
                .Where(x => x.UserId == newReaction.UserId && x.PostId == newReaction.EntityId)
                .FirstOrDefaultAsync();

            if (reaction is not null)
            {
                _context.PostReactions.Remove(reaction);

                if (reaction.IsLike == newReaction.IsLike)
                {
                    await _context.SaveChangesAsync();
                    return;
                }
            }

            _context.PostReactions.Add(new DAL.Entities.PostReaction
            {
                PostId = newReaction.EntityId,
                IsLike = newReaction.IsLike,
                UserId = newReaction.UserId
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeletePostReaction(DeleteReactionDTO deleteReaction)
        {
            var reaction = await _context.PostReactions
                .Where(x => x.UserId == deleteReaction.UserId && x.PostId == deleteReaction.EntityId)
                .FirstOrDefaultAsync();

            if (reaction is null)
                throw new NotFoundException(nameof(DeleteReactionDTO));

            _context.PostReactions.Remove(reaction);

            await _context.SaveChangesAsync();
            return;
        }

        public async Task ReactToComment(NewReactionDTO newReaction)
        {
            var reaction = await _context.CommentReactions
                .Where(x => x.UserId == newReaction.UserId && x.CommentId == newReaction.EntityId)
                .FirstOrDefaultAsync();

            if (reaction is not null)
            {
                _context.CommentReactions.Remove(reaction);

                if (reaction.IsLike == newReaction.IsLike)
                {
                    await _context.SaveChangesAsync();
                    return;
                }
            }

            _context.CommentReactions.Add(new DAL.Entities.CommentReaction
            {
                CommentId = newReaction.EntityId,
                IsLike = newReaction.IsLike,
                UserId = newReaction.UserId
            });

            await _context.SaveChangesAsync();
        }

        public async Task DeleteCommentReaction(DeleteReactionDTO deleteReaction)
        {
            var reaction = await _context.CommentReactions
                .Where(x => x.UserId == deleteReaction.UserId && x.CommentId == deleteReaction.EntityId)
                .FirstOrDefaultAsync();

            if (reaction is null)
                throw new NotFoundException(nameof(DeleteReactionDTO));

            _context.CommentReactions.Remove(reaction);

            await _context.SaveChangesAsync();
            return;
        }
    }
}
