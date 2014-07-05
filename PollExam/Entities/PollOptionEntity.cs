using System;
using System.Data.Linq.Mapping;

namespace PollExam.Entities
{
    [Table(Name = "PollOptions")]
    public class PollOptionEntity : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public Guid PollId { get; set; }

        [Column]
        public String Description { get; set; }
    }
}
