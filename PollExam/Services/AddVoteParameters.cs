using System;

namespace PollExam.Services
{
    public class AddVoteParameters
    {
        public AddVoteParameters(Guid pollId, string userName, string userIp, string userAgent, Guid optionId, string customOption = null)
        {
            CustomOption = customOption;
            OptionId = optionId;
            UserAgent = userAgent;
            UserIp = userIp;
            UserName = userName;
            PollId = pollId;
        }

        public Guid PollId { get; private set; }

        public Guid OptionId { get; private set; }

        public String CustomOption { get; private set; }

        public String UserName { get; private set; }

        public String UserIp { get; private set; }

        public String UserAgent { get; private set; }
    }
}