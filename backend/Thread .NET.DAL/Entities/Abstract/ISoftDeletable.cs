namespace Thread_.NET.DAL.Entities.Abstract
{
    internal interface ISoftDeletable
    {
        bool IsDeleted { get; set; }
    }
}
