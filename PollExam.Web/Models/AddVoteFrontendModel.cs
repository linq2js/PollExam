using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollExam.Web.Models
{
    public class AddVoteFrontendModel
    {
        public Guid PollId { get; set; }

        public String UserName { get; set; }

        public Guid SelectedOptionId { get; set; }

        public String CustomOptionText { get; set; }
    }
}