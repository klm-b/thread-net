using Newtonsoft.Json;

namespace Thread_.NET.Common.DTO.Post
{
    public sealed class PostUpdateDTO
    {
        [JsonIgnore]
        public int AuthorId { get; set; }

        public int Id { get; set; }
        public string PreviewImage { get; set; }
        public string Body { get; set; }
    }
}
