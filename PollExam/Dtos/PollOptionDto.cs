using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollExam.Entities;

namespace PollExam.Dtos
{
    public class PollOptionDto
    {
        public Guid Id { get; set; }

        public Guid PollId { get; set; }

        public String Description { get; set; }

        public static PollOptionDto FromEntity(PollOptionEntity entity)
        {
            return new PollOptionDto
                   {
                       Id = entity.Id,
                       PollId = entity.PollId,
                       Description = entity.Description
                   };
        }
    }
}
