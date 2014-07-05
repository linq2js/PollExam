using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollExam.Services
{
    public class GetPollSummaryParameters
    {
        public GetPollSummaryParameters(Guid pollId)
        {
            PollId = pollId;
        }

        public Guid PollId { get; private set; }
    }
}
