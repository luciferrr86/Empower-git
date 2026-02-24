using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.DAL.Entites;
using A4.Empower.Helpers;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Route("api/[controller]")]
    public class MassSchedulingController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public MassSchedulingController(ILogger<MassSchedulingController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(MassSchedulingListViewModel))]
        public IActionResult GetAllMassinterviewList(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new MassSchedulingListViewModel();
            var viewModel = new List<massSchedulingListModel>();
            var model = _unitOfWork.MassScheduling.GetAllMassinterview(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));

            if (model.Count() > 0)
            {
                foreach (var item in model)
                {
                    viewModel.Add(new massSchedulingListModel { Id = item.Id.ToString(), fromDate = item.StartDate.ToShortDateString(),toDate=item.EndDate.ToShortDateString(),venue=item.Venue });
                }
            }
            result.MassSchedulingList = viewModel;
            result.TotalCount = model.TotalCount;
            return Ok(result);

        }

        [HttpPost("saveInterviewScheduling")]
        public IActionResult SaveInterviewScheduling([FromBody]InterviewSchedulingModel model)
        {
            if (ModelState.IsValid)
            {
                var interview = new MassInterview();
                interview.StartDate = model.StartDate;
                interview.EndDate = model.EndDate;
                interview.Venue = model.Venue;
                interview.Address = model.Address;
                _unitOfWork.MassScheduling.Add(interview);
                if (model.ListTime.Count() > 0)
                {
                    foreach (var item in model.ListTime)
                    {
                        var time = new MassInterviewDetail();
                        time.Date = item.Date;
                        time.StartTime = item.StartTime;
                        time.EndTime = item.EndTime;
                        time.MassInterviewId = interview.Id;
                        _unitOfWork.MassScheduling.CreateInterviewDetail(time);

                    }
                    return Ok(interview.Id);
                }
                else
                {
                    return BadRequest(" model cannot be null");
                }
            }
            return BadRequest(ModelState);
        }

        [HttpPost("saveRoom")]
        public IActionResult SaveRoom([FromBody]RoomModel model)
        {
            try
            {

                if (ModelState.IsValid)
                {
                    var room = new MassInterviewRoom();
                    room.RoomName = model.RoomName;
                    room.StartTime = model.StartTime;
                    room.EndTime = model.EndTime;
                    _unitOfWork.MassScheduling.CreateRooms(room);
                    return NoContent();
                }
                return BadRequest(ModelState);

            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("roomlist")]
        public IActionResult GetRoom()
        {
            try
            {
                var model = new List<RoomModel>();
                var job = _unitOfWork.MassScheduling.GetRooms();
                if (job.Count() > 0)
                {
                    foreach (var item in job)
                    {
                        model.Add(new RoomModel { Id = item.Id.ToString(), RoomName = item.RoomName, EndTime = Utilities.ConvertTime(item.EndTime), StartTime = Utilities.ConvertTime(item.StartTime) });
                    }
                }
                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("saveInterviewPanel")]
        public IActionResult SaveInterviewPanel([FromBody]Panel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var panel = new MassInterviewPanel();
                    panel.Name = model.Name;
                    panel.StartTime = model.StartTime;
                    panel.EndTime = model.EndTime;
                    panel.BreakStartTime = model.BreakStartTime;
                    panel.BreakEndTime = model.BreakEndTime;
                    panel.VacancyId = new Guid(model.VacancyId);
                    panel.InterviewDateId = new Guid(model.InterViewDateId);
                    _unitOfWork.MassScheduling.CreateInterviewPanel(panel);
                    if (model.ManagerIdList.Count() > 0)
                    {
                        foreach (var item in model.ManagerIdList)
                        {
                            var panelVacancy = new MassInterviewPanelVacancy();
                            panelVacancy.MangerId = new Guid(item);
                            panelVacancy.PanelId = panel.Id;
                            _unitOfWork.MassScheduling.CreateInterviewPanelVacancy(panelVacancy);
                        }
                        return NoContent();
                    }
                    return BadRequest(" ManagerIdList cannot be null");
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpGet("interviewPanel/{id}")]
        public IActionResult GetInterviewerPanel(string id)
        {
            var model = new InterviewerModel();
            model.PanleModel = GetPanleList(Guid.Parse(id));
            model.MangerList = GetManagerList();
            model.jobVacancyList = GetOpenJobs();
            model.dateList = GetInterviewDate(Guid.Parse(id));
            return Ok(model);
        }

        [HttpGet("schedule/{id}")]
        [Produces(typeof(InterviewerModel))]
        public IActionResult Schedule(string id)
        {
            var model = new InterviewerModel();
            var panels = GetPanleList(Guid.Parse(id));
            var allpanelTimeSlot = new List<AllCandidateInterviewSlot>();


            var list = PanelTimeSlot(panels, new TimeSpan(0, 0, 30, 0, 0));

            return NoContent();
        }

        [HttpDelete("delete/{id}")]
        public IActionResult DeleteMassInterviewList(string id)
        {
            try
            {
                if (!string.IsNullOrEmpty(id))
                {
                    var checkexist = _unitOfWork.MassScheduling.GetSheduleList(Guid.Parse(id));
                    if (checkexist != null)
                    {
                        _unitOfWork.MassScheduling.Remove(checkexist);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    else
                    {
                        return NotFound(id);
                    }
                }
                return BadRequest(" Id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private List<Panel> GetPanleList(Guid id)
        {

            var panelList = new List<Panel>();
            var result = _unitOfWork.MassScheduling.GetPanelList(id);
            foreach (var item in result)
            {
                panelList.Add(new Panel { Id = item.Id.ToString(), VacancyId = item.VacancyId.ToString(), Name = item.Name, BreakEndTime = Utilities.ConvertTime(item.BreakEndTime), BreakStartTime = Utilities.ConvertTime(item.BreakStartTime), InterViewDate = item.MassInterviewDetail.Date.ToShortDateString(), Vacancy = item.JobVacancy.JobTitle, StartTime = Utilities.ConvertTime(item.StartTime), EndTime = Utilities.ConvertTime(item.EndTime), InterViewDateId = item.MassInterviewDetail.Id.ToString() });
            }
            return panelList;
        }

        private List<DropDownList> GetManagerList()
        {
            var managerList = new List<DropDownList>();
            var employeeList = _unitOfWork.Employee.GetManagerList();
            foreach (var item in employeeList)
            {
                managerList.Add(new DropDownList { Label = item.FullName, Value = item.Id.ToString() });
            }
            return managerList;
        }

        private List<DropDownList> GetOpenJobs()
        {
            var interviewType = new List<DropDownList>();
            var job = _unitOfWork.JobVacancy.GetPublishedVacancyList();
            foreach (var item in job)
            {
                interviewType.Add(new DropDownList { Label = item.JobTitle, Value = item.Id.ToString() });
            }
            return interviewType;
        }

        private List<DropDownList> GetInterviewDate(Guid massId)
        {
            var interviewType = new List<DropDownList>();
            var result = _unitOfWork.MassScheduling.GetInterviewDate(massId);
            foreach (var item in result)
            {
                interviewType.Add(new DropDownList { Label = item.Date.ToShortDateString(), Value = item.Id.ToString() });
            }
            return interviewType;
        }

        private List<MassCandidateInterviewSchedule> PanelTimeSlot(List<Panel> panelList, TimeSpan interval)
        {
            var listScheduleCandidate = new List<MassCandidateInterviewSchedule>();
            List<CandidateInterviewSlot> candidate = new List<CandidateInterviewSlot>();

            foreach (var panel in panelList)
            {
                var date = _unitOfWork.MassScheduling.GetInterviewDetal(Guid.Parse(panel.InterViewDateId));

                if (date != null)
                {
                    DateTime startDate;
                    DateTime endDate;

                    if (date.StartTime > Convert.ToDateTime(panel.StartTime))
                    {
                        startDate = Convert.ToDateTime(panel.StartTime);
                    }
                    else
                    {
                        startDate = date.StartTime;
                    }
                    if (date.EndTime > Convert.ToDateTime(panel.EndTime))
                    {
                        endDate = Convert.ToDateTime(panel.EndTime);
                    }
                    else
                    {
                        endDate = date.EndTime;
                    }

                    while (startDate <= endDate)
                    {
                        candidate.Add(new CandidateInterviewSlot { PanelId = Guid.Parse(panel.Id), StartTime = startDate, EndTime = startDate.Add(interval), InterviewDate = date.Date, VaccancyId = Guid.Parse(panel.VacancyId) });
                        startDate = startDate.Add(interval);
                    }
                }
            }

            var candidateList = _unitOfWork.MassScheduling.GetCandidateList();
            foreach (var item in candidateList)
            {
                var listPanel = candidate.Where(m => m.VaccancyId == item.JobVacancyId).FirstOrDefault();
           

                listScheduleCandidate.Add(new MassCandidateInterviewSchedule { });
                candidate.Remove(listPanel);
            }
            return listScheduleCandidate;
        }

        private void GetCandidateList()
        {
            var list = new List<MassCandidateList>();

            var candidateList = _unitOfWork.MassScheduling.GetCandidateList();
            if (candidateList != null)
            {
                foreach (var item in candidateList)
                {
                    list.Add(new MassCandidateList { CandidateId = item.JobCandidateProfileId, VaccancyId = item.JobVacancyId });
                }
            }
        }

    }
}