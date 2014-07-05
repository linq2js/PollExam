using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PollExam.Web.Models
{
    public class AjaxResultModel
    {
        public Boolean HasError { get; set; }

        public String ErrorMessage { get; set; }
    }
}