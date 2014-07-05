using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PollExam.Data;
using PollExam.Dtos;
using PollExam.Entities;
using PollExam.Services;

namespace PollExam.UnitTests
{
    [TestFixture]
    internal class PollServiceTests
    {
        private IQuery CreateQuery<TEntity>(params TEntity[] entities)
        {
            return new MockQuery(new Dictionary<Type, IQueryable>
                                 {
                                     {typeof (TEntity), entities.AsQueryable()}
                                 });
        }

        private void ExpectedException<TException>(String message, Action<IPollService> action, IPollService pollService = null) where TException : Exception
        {
            try
            {
                action(pollService ?? new DefaultPollService());
            }
            catch (TException ex)
            {
                StringAssert.Contains(message, ex.Message);
            }
        }

        private void ExpectedResult<TResult>(Func<IPollService, TResult> action, TResult result, Boolean notEqual = false, IPollService pollService = null)
        {
            var actual = action(pollService ?? new DefaultPollService());
            if (notEqual)
            {
                Assert.AreNotEqual(result, actual);
            }
            else
            {
                Assert.AreEqual(result, actual);
            }
        }


        #region GetVotes tests

        [Test]
        public void GetVotes_Parameters_ShouldBeNotNull()
        {
            ExpectedException<ArgumentNullException>("parameters value cannot be null",
                x => x.GetVotes(null));
        }

        [Test]
        public void GetVotes_PollId_ShouldBeNotEmpty()
        {
            ExpectedException<ArgumentException>("parameters.PollId value cannot be empty",
                x => x.GetVotes(new GetVotesParameters(Guid.Empty, 0, 0)));
        }

        [Test]
        public void GetVotes_PageIndex_ShouldBeGreaterThanOrEqualToZero()
        {
            ExpectedException<ArgumentException>("parameters.PageIndex is not valid",
                x => x.GetVotes(new GetVotesParameters(Guid.NewGuid(), -1, 1)));
        }

        [Test]
        public void GetVotes_PageSize_ShouldBeGreaterThanZero()
        {
            ExpectedException<ArgumentException>("parameters.PageSize is not valid",
                x => x.GetVotes(new GetVotesParameters(Guid.NewGuid(), 0, 0)));
        }

        [Test]
        public void GetVotes_ShouldReturnNotNullObject()
        {
            ExpectedResult(
                x => x.GetVotes(new GetVotesParameters(Guid.NewGuid(), 0, 1)),
                null,
                true,
                new DefaultPollService
                {
                    Query = CreateQuery<VoteEntity>()
                });
        }

        [Test]
        public void GetVotes_ShouldReturnEnumerableObjectThatHasItemCountLessThanOrEqualToPageSize()
        {
            // arrange
            var pollService = new DefaultPollService
                              {
                                  Query = CreateQuery<VoteEntity>()
                              };
            var parameters = new GetVotesParameters(Guid.NewGuid(), 0, 1);

            // action
            var result = pollService.GetVotes(parameters);

            // assert
            Assert.That(result.Count(), Is.LessThanOrEqualTo(parameters.PageSize));
        }

        [Test]
        public void GetVotes_ShouldReturnVoteDtoObjectsThatHavePollIdMatchSpecificPollId()
        {
            // arrange
            var pollId = Guid.NewGuid();
            var voteEntities = new[]
                               {
                                   new VoteEntity {PollId = pollId},
                                   new VoteEntity {PollId = Guid.NewGuid()}
                               };
            var pollService = new DefaultPollService
            {
                Query = CreateQuery(voteEntities)
            };
            var parameters = new GetVotesParameters(pollId, 0, 10);

            // action
            var result = pollService.GetVotes(parameters);

            // assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(1));
            Assert.That(result.First().PollId, Is.EqualTo(pollId));
        }

        #endregion

        #region GetOptions tests



        #endregion

        #region GetPollOptionSummaries tests

        #endregion

        #region AddVote tests



        #endregion
    }

    internal class MockQuery : IQuery
    {
        private readonly IDictionary<Type, IQueryable> _queries;

        public MockQuery(IDictionary<Type, IQueryable> queries)
        {
            _queries = queries;
        }

        public IQueryable<TEntity> Create<TEntity>() where TEntity : class, IEntity
        {
            return (IQueryable<TEntity>) _queries[typeof (TEntity)];
        }
    }
}
