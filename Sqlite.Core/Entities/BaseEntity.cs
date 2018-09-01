using System;
using System.ComponentModel.DataAnnotations;

namespace Sqlite.Core.Entities
{
    public class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public DateTime? LastModifiedOn { get; set; }

        public DateTime? AddedOn { get; set; }
    }
}