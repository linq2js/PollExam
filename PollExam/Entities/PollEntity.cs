using System;
using System.Collections.Generic;
using System.Data.Linq.Mapping;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollExam.Entities
{
    [Table(Name = "Polls")]
    public class PollEntity : IEntity
    {
        [Column(IsPrimaryKey = true)]
        public Guid Id { get; set; }

        [Column]
        public String Name { get; set; }

        [Column]
        public String Description { get; set; }
    }
}
