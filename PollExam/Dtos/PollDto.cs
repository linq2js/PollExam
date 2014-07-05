using System;
using PollExam.Entities;

namespace PollExam.Dtos
{
    public class PollDto
    {
        public Guid Id { get; set; }

        public String Name { get; set; }

        public String Description { get; set; }

        public static PollDto FromEntity(PollEntity entity)
        {
            return new PollDto
                   {
                       Id = entity.Id,
                       Name = entity.Name,
                       Description = entity.Description
                   };
        }
    }
}
