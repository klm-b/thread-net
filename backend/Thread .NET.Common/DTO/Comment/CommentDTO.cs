using System;
using System.Collections.Generic;
using Thread_.NET.Common.DTO.Reaction;
using Thread_.NET.Common.DTO.User;

namespace Thread_.NET.Common.DTO.Comment
{
    public sealed class CommentDTO
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public UserDTO Author { get; set; }
        public string Body { get; set; }

        public int LikesNumber { get; set; }
        public int DislikesNumber { get; set; }
        public bool? IsLikedByMe { get; set; }
    }
}
