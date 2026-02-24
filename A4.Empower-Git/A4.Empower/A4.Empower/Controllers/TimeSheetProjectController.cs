using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using A4.BAL;
using A4.DAL.Entites;
using DAL;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{

    [Route("api/[controller]")]
    public class TimeSheetProjectController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;

        public TimeSheetProjectController(ILogger<TimeSheetProjectController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        #region Project 
        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(ProjectViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new ProjectViewModel();
            var viewModel = new List<ProjectModel>();
            var model = _unitOfWork.Project.GetAllProject(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (model.Count() > 0)
            {
                foreach (var item in model)
                {
                    viewModel.Add(new ProjectModel { Id = item.Id.ToString(), ProjectName = item.ProjectName, StartDate = item.StartDate, EndDate = item.EndDate, Description = item.Description, ClientId = item.ClientId.ToString(), ManagerId = item.ManagerId.ToString(), Status = item.Status });
                }
            }
            result.ProjectList = viewModel;
            result.ClientList = GetClientList();
            result.ManagerList = GetManagerList();
            result.TotalCount = model.TotalCount;
            return Ok(result);
        }

        [HttpPost("create")]
        public IActionResult Create([FromBody] ProjectModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (model.StartDate > model.EndDate)
                        return BadRequest("End date cannot be before start date");
                    if (string.IsNullOrEmpty(model.ProjectName) && string.IsNullOrEmpty(model.ClientId) && string.IsNullOrEmpty(model.ManagerId))
                        return BadRequest("model property cannot be null");
                    var project = new TimesheetProject();
                    project.ProjectName = model.ProjectName;
                    project.ClientId = Guid.Parse(model.ClientId);
                    project.StartDate = model.StartDate;
                    project.EndDate = model.EndDate;
                    project.Description = model.Description;
                    project.ManagerId = Guid.Parse(model.ManagerId);
                    project.Status = model.Status;
                    _unitOfWork.Project.Add(project);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.ProjectName + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody]ProjectModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        if (string.IsNullOrEmpty(model.ProjectName) && string.IsNullOrEmpty(model.ClientId) && string.IsNullOrEmpty(model.ManagerId))
                            return BadRequest("model property cannot be null");
                        var project = _unitOfWork.Project.Get(Guid.Parse(id));
                        if (project != null)
                        {
                            project.ProjectName = model.ProjectName;
                            project.ClientId = Guid.Parse(model.ClientId);
                            project.StartDate = model.StartDate;
                            project.EndDate = model.EndDate;
                            project.Description = model.Description;
                            project.Status = model.Status;
                            project.ManagerId = Guid.Parse(model.ManagerId);
                            _unitOfWork.Project.Update(project);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("projec cannot be null");
                    }
                    return BadRequest(ModelState);
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpDelete("delete/{id}")]
        public IActionResult Delete(string id)
        {
            try
            {
                if (id != null)
                {
                    var checkProject = _unitOfWork.Project.Get(Guid.Parse(id));
                    if (checkProject != null)
                    {
                        _unitOfWork.Project.Remove(checkProject);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("project cannot be null");
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private List<DropDownList> GetManagerList()
        {
            var viewModel = new List<DropDownList>();
            var manager = _unitOfWork.Project.GetManagerList();
            if (manager.Count() > 0)
            {
                foreach (var item in manager)
                {
                    viewModel.Add(new DropDownList { Label = item.FullName, Value = item.Id.ToString() });
                }
            }
            return viewModel;
        }

        private List<DropDownList> GetClientList()
        {
            var viewModel = new List<DropDownList>();
            var client = _unitOfWork.Project.GetClientList();
            if (client.Count() > 0)
            {
                foreach (var item in client)
                {
                    viewModel.Add(new DropDownList { Label = item.Name, Value = item.Id.ToString() });
                }
            }
            return viewModel;
        }

        #endregion

        #region Assign Project   

        [HttpGet("getAssignProject/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(AssignProjectViewModel))]
        public IActionResult GetAssignProject(int? page = null, int? pageSize = null, string name = null)
        {
            var result = new AssignProjectViewModel();
            var viewModel = new List<EmployeeListModel>();
            var employeeList = _unitOfWork.TimesheetTemplate.GetEmployeeList(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (employeeList.Count() > 0)
            {
                foreach (var item in employeeList)
                {

                    viewModel.Add(new EmployeeListModel { EmployeeId = item.Id.ToString(), FullName = item.ApplicationUser.FullName, Designation = item.FunctionalDesignation.Name, ProjectId = "0" });
                }
            }
            result.EmployeeList = viewModel;
            result.ProjectList = GetProjectList();
            result.TotalCount = employeeList.TotalCount;
            return Ok(result);
        }

        [HttpGet("getEmployee/{id}/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(EmployeeListViewModel))]
        public IActionResult GetEmployeeByProjectId(string id, int? page = null, int? pageSize = null, string name = null)
        {
            var viewModel = new EmployeeListViewModel();
            var model = new List<EmployeeListModel>();
            var result = _unitOfWork.TimesheetEmployeeProject.GetEmployeeList(Guid.Parse(id), name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (result.Count() > 0)
            {
                foreach (var item in result)
                {
                    string TimeSheetProjectId = "";
                    if (item.TimeSheetProjectId.ToString() == "00000000-0000-0000-0000-000000000000")
                    {
                        TimeSheetProjectId = "0";
                    }
                    else
                    {
                        TimeSheetProjectId = item.TimeSheetProjectId.ToString();
                    }
                    model.Add(new EmployeeListModel
                    {
                        EmployeeId = item.Employee.Id.ToString(),
                        FullName = item.Employee.ApplicationUser.FullName,
                        Designation = item.Employee.FunctionalDesignation.Name,
                        ProjectId = TimeSheetProjectId
                    });
                }
            }
            viewModel.EmployeeList = model;
            viewModel.TotalCount = result.TotalCount;
            return Ok(viewModel);
        }

        [HttpPost("assignProject")]
        public IActionResult AssignProject([FromBody]AssignProjectModel model)
        {
            
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    if (string.IsNullOrEmpty(model.ProjectId))
                        return BadRequest("Project  cannot be null");

                    var check = _unitOfWork.TimesheetEmployeeProject.Find(m => m.IsActive == true && m.TimeSheetProjectId == Guid.Parse(model.ProjectId)).ToList();
                    if (check.Count > 0)
                    {
                        _unitOfWork.TimesheetEmployeeProject.RemoveRange(check);
                        _unitOfWork.SaveChanges();

                    }


                    if (model.Employeelist.Count != 0)
                    {
                        foreach (var item in model.Employeelist)
                        {
                            var empId = Guid.Parse(item.EmployeeId);
                            var projectId = Guid.Parse(model.ProjectId);
                            var employeeProjects = _unitOfWork.TimesheetEmployeeProject.GetProjectByEmployeeId(empId);
                            var count = employeeProjects.Where(q => q.TimeSheetProjectId == projectId).Count();
                            if (count == 0)
                            {
                                var project = new TimesheetEmployeeProject();
                                project.TimeSheetProjectId = projectId;
                                project.EmployeeId = empId;
                                _unitOfWork.TimesheetEmployeeProject.Add(project);
                                _unitOfWork.SaveChanges();
                            }
                        }


                    }
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        private List<DropDownList> GetProjectList()
        {
            var viewModel = new List<DropDownList>();
            var projectList = _unitOfWork.Project.GetAll().ToList();
            if (projectList.Count() > 0)
            {
                foreach (var item in projectList)
                {
                    viewModel.Add(new DropDownList { Label = item.ProjectName, Value = item.Id.ToString() });
                }
            }
            return viewModel;
        }

        #endregion

    }
}