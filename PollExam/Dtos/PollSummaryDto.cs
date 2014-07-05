using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollExam.Dtos
{
    public class PollSummaryDto
    {
        public PollDto Poll { get; set; }

        public IEnumerable<PollOptionSummaryDto> OptionSummaries { get; set; }

        public Int32 CustomOptionVotes { get; set; }

        public Int32 TotalVotes
        {
            get { return CustomOptionVotes + (OptionSummaries == null ? 0 : OptionSummaries.Sum(x => x.Votes)); }
        }
    }
}
