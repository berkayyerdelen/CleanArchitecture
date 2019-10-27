using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Entities.Abstract
{
    public class BaseEntity<T>: IBaseEntity<T>
    {
        [Key]
        [Column(Order = 1)]
        public T Id { get; set; }
    }
}