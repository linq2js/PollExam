using System;

namespace PollExam.Services
{
    public class GetVotesParameters
    {
        public GetVotesParameters(Guid pollId, int pageIndex, int pageSize)
        {
            PageSize = pageSize;
            PageIndex = pageIndex;
            PollId = pollId;
        }

        /// <summary>
        /// Filter the vote list by poll id
        /// </summary>
        public Guid PollId { get; private set; }

        /// <summary>
        /// Specific zero based page index
        /// </summary>
        public Int32 PageIndex { get; private set; }

        /// <summary>
        /// Specific how many entries per page
        /// </summary>
        public Int32 PageSize { get; private set; }
    }
}
