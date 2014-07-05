using System;
using System.ComponentModel.Composition;
using System.Linq;
using System.Web.Mvc;
using PollExam.Services;
using PollExam.Web.Models;

namespace PollExam.Web.Controllers
{
    public class FrontendController : ControllerBase
    {
        [Import]
        public IPollService PollService { get; set; }

        [HttpPost]
        public ActionResult AddVote(AddVoteFrontendModel model)
        {
            AjaxResultModel ajaxResultModel;
            try
            {
                PollService.AddVote(new AddVoteParameters(model.PollId, model.UserName, Request.UserHostAddress, Request.UserAgent, model.SelectedOptionId, model.CustomOptionText));

                ajaxResultModel = new AjaxResultModel
                                  {
                                      HasError = false
                                  };
            }
            catch (Exception ex)
            {
                ajaxResultModel = new AjaxResultModel
                                  {
                                      HasError = true,
                                      ErrorMessage = ex.Message
                                  };
            }
            return Json(ajaxResultModel);
        }

        [HttpGet]
        public ActionResult GetPollSummary(Guid pollId)
        {
            var summary = PollService.GetPollSummary(new GetPollSummaryParameters(pollId));
            return Json(summary, JsonRequestBehavior.AllowGet);
        }

        //
        // GET: /Frontend/

        public ActionResult Index()
        {
            var model = new IndexFrontendModel();
            model.Poll = PollService.GetPolls(new GetPollsParameters()).First();
            model.PollOptions = PollService.GetOptions(new GetOptionsParameters(model.Poll.Id));
            return View(model);
        }
    }
}