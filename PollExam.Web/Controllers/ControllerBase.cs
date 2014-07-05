using System;
using System.ComponentModel.Composition;
using System.Text;
using System.Web.Mvc;

namespace PollExam.Web.Controllers
{
    public class ControllerBase : Controller
    {
        public ControllerBase()
        {
            this.Bind();
        }
    }
}