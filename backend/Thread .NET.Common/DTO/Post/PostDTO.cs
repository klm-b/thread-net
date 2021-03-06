using System;
using System.Collections.Generic;
using Thread_.NET.Common.DTO.Comment;
using Thread_.NET.Common.DTO.Reaction;
using Thread_.NET.Common.DTO.User;

namespace Thread_.NET.Common.DTO.Post
{
    public sealed class PostDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsUpdated { get; set; }
        public UserDTO Author { get; set; }
        public string PreviewImage { get; set; }
        public string Body { get; set; }

        public ICollection<CommentDTO> Comments { get; set; }
        public int LikesNumber { get; set; }
        public int DislikesNumber { get; set; }
        public int CommentsNumber { get; set; }
        public bool? IsLikedByMe { get; set; }
    }
}
