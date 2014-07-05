using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Text.RegularExpressions;
using PollExam.Data;
using PollExam.Dtos;
using PollExam.Entities;

namespace PollExam.Services
{
    [Export(typeof (IPollService))]
    public class DefaultPollService : IPollService
    {
        [Import]
        public IQuery Query { get; set; }

        [Import]
        public IRepository<VoteEntity> VoteRepository { get; set; }

        public IEnumerable<VoteDto> GetVotes(GetVotesParameters parameters)
        {
            Guard.CanNotBeNull(parameters, "parameters");
            Guard.CanNotBeEmpty(parameters.PollId, "parameters.PollId");
            Guard.Against<ArgumentException>(parameters.PageIndex < 0, "parameters.PageIndex is not valid");
            Guard.Against<ArgumentException>(parameters.PageSize <= 0, "parameters.PageSize is not valid");

            var voteQuery = Query.Create<VoteEntity>();
            var result = voteQuery
                .Where(x => x.PollId == parameters.PollId)
                .Skip(parameters.PageIndex*parameters.PageSize)
                .Take(parameters.PageSize);

            return result
                .Select(x => new VoteDto
                             {
                                 Id = x.Id,
                                 CustomOption = x.CustomOption,
                                 OptionId = x.OptionId,
                                 PollId = x.PollId,
                                 UserAgent = x.UserAgent,
                                 UserIp = x.UserIp,
                                 UserName = x.UserName
                             })
                .ToArray();
        }

        public void AddVote(AddVoteParameters parameters)
        {
            Guard.CanNotBeNull(parameters, "parameters");
            Guard.CanNotBeEmpty(parameters.PollId, "parameters.PollId");
            Guard.CanNotBeNullOrEmpty(parameters.UserName, "parameters.UserName");
            Guard.CanNotBeNullOrEmpty(parameters.UserIp, "parameters.UserIp");
            Guard.CanNotBeNullOrEmpty(parameters.UserAgent, "parameters.UserAgent");
            Guard.Against<ArgumentException>(!Regex.IsMatch(parameters.UserIp, @"^(?:\d{1,3}\.\d{1,3}\.\d{1,3}\.\d{1,3}|::1)$"), "parameters.UserIp is not valid format");

            var voteQuery = Query.Create<VoteEntity>();
            var pollQuery = Query.Create<PollEntity>();

            Guard.Against<ArgumentException>(!pollQuery.Any(x => x.Id == parameters.PollId), string.Format("Cannot find any poll which has id {0}", parameters.PollId));
            Guard.InvalidOperation(voteQuery.Any(x => x.PollId == parameters.PollId && x.UserName == parameters.UserName), "You have already voted");

            VoteRepository.Save(new VoteEntity
                                {
                                    PollId = parameters.PollId,
                                    OptionId = parameters.OptionId,
                                    CustomOption = parameters.CustomOption,
                                    UserAgent = parameters.UserAgent,
                                    UserIp = parameters.UserIp,
                                    UserName = parameters.UserName
                                });
        }

        public IEnumerable<PollOptionDto> GetOptions(GetOptionsParameters parameters)
        {
            Guard.CanNotBeNull(parameters, "parameters");

            var pollOptionQuery = Query.Create<PollOptionEntity>();
            var result = pollOptionQuery.Where(x => x.PollId == parameters.PollId);
            return result.Select(PollOptionDto.FromEntity);
        }

        public IEnumerable<PollOptionSummaryDto> GetPollOptionSummaries(GetPollOptionSummariesParameters parameters)
        {
            Guard.CanNotBeNull(parameters, "parameters");
            Guard.CanNotBeEmpty(parameters.PollId, "parameters.PollId");

            var pollOptionQuery = Query.Create<PollOptionEntity>();
            var voteQuery = Query.Create<VoteEntity>();
            var pollOptions = pollOptionQuery.Where(x => x.PollId == parameters.PollId);
            var result =
                from v in voteQuery
                where v.PollId == parameters.PollId && v.OptionId != Guid.Empty
                group v by v.OptionId
                into g
                select new
                       {
                           OptionId = g.Key,
                           Count = g.Count()
                       };
            return result
                .Select(x => new PollOptionSummaryDto
                             {
                                 Option = PollOptionDto.FromEntity(pollOptions.First(o => o.Id == x.OptionId)),
                                 Votes = x.Count
                             })
                .ToArray();
        }

        public PollSummaryDto GetPollSummary(GetPollSummaryParameters parameters)
        {
            Guard.CanNotBeNull(parameters, "parameters");
            Guard.CanNotBeEmpty(parameters.PollId, "parameters.PollId");

            var pollQuery = Query.Create<PollEntity>();
            var poll = pollQuery.FirstOrDefault(x => x.Id == parameters.PollId);
            Guard.Against<ArgumentException>(poll == null, string.Format("Cannot find any poll which has id {0}", parameters.PollId));

            var voteQuery = Query.Create<VoteEntity>();

            var pollOptionSummaries = GetPollOptionSummaries(new GetPollOptionSummariesParameters(parameters.PollId));

            return new PollSummaryDto
                   {
                       Poll = PollDto.FromEntity(poll),
                       OptionSummaries = pollOptionSummaries,
                       CustomOptionVotes = voteQuery.Count(x => x.PollId == parameters.PollId && x.OptionId == Guid.Empty)
                   };
        }

        public IEnumerable<PollDto> GetPolls(GetPollsParameters parameters)
        {
            Guard.CanNotBeNull(parameters, "parameters");

            var pollQuery = Query.Create<PollEntity>();
            return pollQuery.Select(PollDto.FromEntity).ToArray();
        }
    }
}
