using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;
using Thread_.NET.Common.DTO.Comment;
using Thread_.NET.Common.DTO.Reaction;
using Thread_.NET.Extensions;

namespace Thread_.NET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class CommentsController : ControllerBase
    {
        private readonly CommentService _commentService;
        private readonly ReactionService _likeService;

        public CommentsController(CommentService commentService, ReactionService likeService)
        {
            _commentService = commentService;
            _likeService = likeService;
        }

        [HttpGet("{postId}")]
        public async Task<ActionResult<CommentDTO>> GetPostComments(int postId)
        {
            return Ok(await _commentService.GetPostComments(postId, this.GetUserIdFromToken()));
        }

        [HttpPost]
        public async Task<ActionResult<CommentDTO>> CreateComment([FromBody] NewCommentDTO comment)
        {
            comment.AuthorId = this.GetUserIdFromToken();
            return Ok(await _commentService.CreateComment(comment));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateComment([FromBody] CommentUpdateDTO dto)
        {
            dto.AuthorId = this.GetUserIdFromToken();
            await _commentService.UpdateComment(dto);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteComment(int id)
        {
            await _commentService.DeleteComment(id);
            return NoContent();
        }

        [HttpPost("reactions")]
        public async Task<IActionResult> ReactToComment(NewReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();

            await _likeService.ReactToComment(reaction);
            return Ok();
        }

        [HttpDelete("reactions")]
        public async Task<IActionResult> DeleteReaction([FromQuery] DeleteReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();

            await _likeService.DeleteCommentReaction(reaction);
            return Ok();
        }
    }
}