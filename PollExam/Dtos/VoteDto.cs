using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PollExam.Entities;

namespace PollExam.Dtos
{
    public class VoteDto
    {
        public Guid Id { get; set; }

        public Guid? PollId { get; set; }

        public Guid OptionId { get; set; }

        public String UserName { get; set; }

        public String CustomOption { get; set; }

        public String UserAgent { get; set; }

        public String UserIp { get; set; }

        public static VoteDto FromEntity(VoteEntity entity)
        {
            return new VoteDto
                   {
                       Id = entity.Id,
                       CustomOption = entity.CustomOption,
                       OptionId = entity.OptionId,
                       PollId = entity.PollId,
                       UserAgent = entity.UserAgent,
                       UserIp = entity.UserIp,
                       UserName = entity.UserName
                   };
        }
    }
}
