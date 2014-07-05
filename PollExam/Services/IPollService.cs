using System;
using System.Collections.Generic;
using PollExam.Dtos;

namespace PollExam.Services
{
    public interface IPollService
    {
        /// <summary>
        /// Get votes by specific pollId with pagination support
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<VoteDto> GetVotes(GetVotesParameters parameters);

        /// <summary>
        /// Add new user vote
        /// </summary>
        /// <param name="parameters"></param>
        void AddVote(AddVoteParameters parameters);

        /// <summary>
        /// Get all options of specific poll
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<PollOptionDto> GetOptions(GetOptionsParameters parameters);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<PollOptionSummaryDto> GetPollOptionSummaries(GetPollOptionSummariesParameters parameters);

        /// <summary>
        /// Get poll info and voting summary, include all poll option summaries
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        PollSummaryDto GetPollSummary(GetPollSummaryParameters parameters);

        /// <summary>
        /// Get all polls
        /// </summary>
        /// <param name="parameters"></param>
        /// <returns></returns>
        IEnumerable<PollDto> GetPolls(GetPollsParameters parameters);
    }
}
