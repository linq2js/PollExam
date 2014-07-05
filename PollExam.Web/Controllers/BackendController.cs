using System;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PollExam.Services;
using PollExam.Web.Models;

namespace PollExam.Web.Controllers
{
    public class BackendController : ControllerBase
    {
        [Import]
        public IPollService PollService { get; set; }

        //
        // GET: /Backend/

        public ActionResult Index()
        {
            var model = new IndexBackendModel();
            model.Poll = PollService.GetPolls(new GetPollsParameters()).First();
            model.PollOptions = PollService.GetOptions(new GetOptionsParameters(model.Poll.Id));
            return View(model);
        }

        [HttpGet]
        public ActionResult GetPollSummary(Guid pollId)
        {
            var summary = PollService.GetPollSummary(new GetPollSummaryParameters(pollId));
            return Json(summary, JsonRequestBehavior.AllowGet);
        }
    }
}
