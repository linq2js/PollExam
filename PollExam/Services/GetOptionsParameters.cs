using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PollExam.Services
{
    public class GetOptionsParameters
    {
        public GetOptionsParameters(Guid pollId)
        {
            PollId = pollId;
        }

        /// <summary>
        /// Specific pollId
        /// </summary>
        public Guid PollId { get; private set; }
    }
}
