using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AASTHA2.Entities
{
    public class BaseEntity
    {
        [Key]
        public long Id { get; set; }
        public long? CreatedBy { get; set; }
        public long? ModifiedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool? IsDeleted { get; set; }

        [ForeignKey("CreatedBy")]
        public User CreaterInfo { get; set; }
        [ForeignKey("ModifiedBy")]
        public User ModifierInfo { get; set; }
    }
}
