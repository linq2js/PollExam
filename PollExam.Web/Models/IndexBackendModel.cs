using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using PollExam.Dtos;

namespace PollExam.Web.Models
{
    public class IndexBackendModel
    {
        public PollDto Poll { get; set; }

        public IEnumerable<PollOptionDto> PollOptions { get; set; }
    }
}