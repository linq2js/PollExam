using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollExam.Dtos
{
    public class PollOptionSummaryDto
    {
        public PollOptionDto Option { get; set; }

        public Int32 Votes { get; set; }
    }
}
