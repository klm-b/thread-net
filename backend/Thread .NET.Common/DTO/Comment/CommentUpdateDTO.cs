using Newtonsoft.Json;

namespace Thread_.NET.Common.DTO.Comment
{
    public sealed class CommentUpdateDTO
    {
        [JsonIgnore]
        public int AuthorId { get; set; }

        public int Id { get; set; }
        public string Body { get; set; }
    }
}
