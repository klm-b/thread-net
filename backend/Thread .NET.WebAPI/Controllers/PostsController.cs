using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Thread_.NET.BLL.Services;
using Thread_.NET.Common.DTO.Reaction;
using Thread_.NET.Common.DTO.Post;
using Thread_.NET.Extensions;

namespace Thread_.NET.WebAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly PostService _postService;
        private readonly ReactionService _likeService;

        public PostsController(PostService postService, ReactionService likeService)
        {
            _postService = postService;
            _likeService = likeService;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<ICollection<PostDTO>>> Get()
        {

            return Ok(await (User.Identity.IsAuthenticated
                ? _postService.GetAllPosts(this.GetUserIdFromToken())
                : _postService.GetAllPosts()));
        }

        [HttpPost]
        public async Task<ActionResult<PostDTO>> CreatePost([FromBody] PostCreateDTO dto)
        {
            dto.AuthorId = this.GetUserIdFromToken();

            return Ok(await _postService.CreatePost(dto));
        }

        [HttpPut]
        public async Task<ActionResult<PostDTO>> UpdatePost([FromBody] PostUpdateDTO dto)
        {
            dto.AuthorId = this.GetUserIdFromToken();
            await _postService.UpdatePost(dto);

            return NoContent();
        }

        [HttpGet("reactions/{postId}")]
        public async Task<IActionResult> GetPostReactions(int postId)
        {
            return Ok(await _likeService.GetPostReactions(postId));
        }

        [HttpPost("reactions")]
        public async Task<IActionResult> ReactToPost(NewReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();

            await _likeService.ReactToPost(reaction);
            return Ok();
        }

        [HttpDelete("reactions")]
        public async Task<IActionResult> DeleteReaction([FromQuery]DeleteReactionDTO reaction)
        {
            reaction.UserId = this.GetUserIdFromToken();

            await _likeService.DeleteReaction(reaction);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _postService.DeletePost(id);
            return NoContent();
        }
    }
}