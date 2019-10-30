namespace Entities.Abstract
{
    public interface IBaseEntity<T>
    {
        T Id { get; set; }
    }
}