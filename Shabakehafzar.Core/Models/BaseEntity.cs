using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace Shabakehafzar.Core.Models
{
    public class BaseEntity
    {
        public BaseEntity()
        {
            Id = Guid.NewGuid();
            CreatedDate = DateTime.Now;
            IsActive = true;
        }
        /// <summary>
        /// Gets or sets the entity identifier
        /// </summary>
        [Key]
        public virtual Guid Id { get; set; }
        /// <summary>
        /// for soft delete
        /// </summary>
        public virtual bool IsActive { get; set; }
        public virtual DateTime CreatedDate { get; set; }
    }
}
