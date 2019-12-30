namespace Entities.Abstract {
    public interface ISoftDelete {
        bool IsDeleted { get; set; }
    }
}