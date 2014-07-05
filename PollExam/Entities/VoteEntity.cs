using System;
using System.Data.Linq.Mapping;

namespace PollExam.Entities
{
    [Table(Name = "Votes")]
    public class VoteEntity : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public Guid? PollId { get; set; }

        [Column]
        public Guid OptionId { get; set; }

        [Column]
        public String UserName { get; set; }

        [Column]
        public String CustomOption { get; set; }

        [Column]
        public String UserAgent { get; set; }

        [Column]
        public String UserIp { get; set; }
    }
}
