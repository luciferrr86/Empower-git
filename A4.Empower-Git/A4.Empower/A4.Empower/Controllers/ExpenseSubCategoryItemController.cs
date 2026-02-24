using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using A4.BAL;
using A4.BAL.ExpenseBooking;
using A4.DAL.Entites;
using A4.Empower.ViewModels;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace A4.Empower.Controllers
{
    [Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    public class ExpenseSubCategoryItemController : Controller
    {
        readonly ILogger _logger;
        readonly IUnitOfWork _unitOfWork;
        public ExpenseSubCategoryItemController(ILogger<ExpenseBookingController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        #region Catgory

        [HttpGet("categorylist/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(CategoryViewModel))]
        public IActionResult GetAllCatgory(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new CategoryViewModel();
                var request = new List<ExpenseCategoryModel>();
                var model = _unitOfWork.SubCategoryItem.GetAllCategory(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        request.Add(new ExpenseCategoryModel { Id = item.Id.ToString(), Name = item.Name });
                    }
                }
                result.CategoryList = request;
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("createcategory")]
        [Produces(typeof(CategoryModel))]
        public IActionResult CreateCategory([FromBody]ExpenseCategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var Model = new ExpenseBookingCategory()
                    {
                        Name = model.Name,
                    };
                    _unitOfWork.Category.Add(Model);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.Name + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("updatecategory/{id}")]
        public IActionResult UpdateCategory(string id, [FromBody]ExpenseCategoryModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        var item = _unitOfWork.Category.Get(Guid.Parse(id));
                        if (item != null)
                        {
                            item.Name = model.Name;
                            _unitOfWork.Category.Update(item);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("item cannot be null");
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

        [HttpDelete("deletecategory/{id}")]
        public IActionResult DeleteCategory(string id)
        {
            try
            {
                if (id != null)
                {
                    var check = _unitOfWork.Category.Get(Guid.Parse(id));
                    if (check != null)
                    {
                        _unitOfWork.Category.Remove(check);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("check cannot be null");
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
        #endregion

        #region Sub Category

        [HttpGet("subcategorylist/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(SubCategoryViewModel))]
        public IActionResult GetAllSubCatgory(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new SubCategoryViewModel();
                var request = new List<ExpenseSubCategoryModel>();
                var model = _unitOfWork.SubCategoryItem.GetAllSubCategory(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        request.Add(new ExpenseSubCategoryModel { Id = item.Id.ToString(), Name = item.Name, Category = item.Category, CategoryId = item.CategoryId });
                    }
                }
                result.SubCategoryList = request;
                result.CategoryList = GetCategoryList();
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("createsubcategory")]
        [Produces(typeof(CategoryModel))]
        public IActionResult CreateSubCategory([FromBody]ExpenseSubCategoryModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var Model = new ExpenseBookingSubCategory()
                    {
                        Name = model.Name,
                        ExpenseBookingCategoryId = Guid.Parse(model.CategoryId)
                    };
                    _unitOfWork.SubCategory.Add(Model);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.Name + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("updatesubcategory/{id}")]
        public IActionResult UpdateSubCategory(string id, [FromBody]ExpenseSubCategoryModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        var item = _unitOfWork.SubCategory.Get(Guid.Parse(id));
                        if (item != null)
                        {
                            item.Name = model.Name;
                            item.ExpenseBookingCategoryId = Guid.Parse(model.CategoryId);
                            _unitOfWork.SubCategory.Update(item);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("item cannot be null");
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

        [HttpDelete("deletesubcategory/{id}")]
        public IActionResult DeleteSubCategory(string id)
        {
            try
            {
                if (id != null)
                {
                    var check = _unitOfWork.SubCategory.Get(Guid.Parse(id));
                    if (check != null)
                    {
                        _unitOfWork.SubCategory.Remove(check);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("check cannot be null");
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        #endregion

        #region Expense Item

        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(SubCategoryItemViewModel))]
        public IActionResult GetAll(int? page = null, int? pageSize = null, string name = null)
        {
            try
            {
                var result = new SubCategoryItemViewModel();
                var request = new List<SubCategoryItemModel>();
                var model = _unitOfWork.SubCategoryItem.GetAll(name, Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (model.Count > 0)
                {
                    foreach (var item in model)
                    {
                        request.Add(new SubCategoryItemModel { Id = item.Id.ToString(), Name = item.Name, SubCategoryId = item.SubCategoryId.ToString(), SubCategory = item.SubCategory, CategoryId = item.CategoryId.ToString(), Category = item.Category });
                    }
                }
                result.SubCategoryItemList = request;
                result.CategoryList = GetCategoryList();
                //result.SubCategoryList = GetSubCategoryList();
                result.TotalCount = model.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPost("create")]
        [Produces(typeof(SubCategoryItemModel))]
        public IActionResult Create([FromBody]SubCategoryItemModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var Model = new ExpenseBookingSubCategoryItem()
                    {
                        Name = model.Name,
                        ExpenseBookingSubCategoryId = Guid.Parse(model.SubCategoryId)
                    };
                    _unitOfWork.SubCategoryItem.Add(Model);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest(model.Name + " already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }


        [HttpGet("getSubCategory/{id}")]
        public IActionResult GetSubCategoryByCategoryId(string id)
        {
            try
            {
                if (id != null)
                {
                    var subCategoryList = new List<DropDownList>();
                    subCategoryList = GetSubCategoryList(new Guid(id));
                    return Ok(subCategoryList);
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("update/{id}")]
        public IActionResult Update(string id, [FromBody]SubCategoryItemModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        var item = _unitOfWork.SubCategoryItem.Get(Guid.Parse(id));
                        if (item != null)
                        {
                            item.Name = model.Name;
                            item.ExpenseBookingSubCategoryId = Guid.Parse(model.SubCategoryId);
                            _unitOfWork.SubCategoryItem.Update(item);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("item cannot be null");
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
        public IActionResult DeleteExpenseItem(string id)
        {
            try
            {
                if (id != null)
                {
                    var check = _unitOfWork.SubCategoryItem.Get(new Guid(id));
                    if (check != null)
                    {
                        _unitOfWork.SubCategoryItem.Remove(check);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("check cannot be null");
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }

        #endregion

        #region Expense Title Value
        [HttpGet("expanseList/{page?}/{pageSize?}")]
        [Produces(typeof(ExpenseBookingTitleViewModel))]
        public ActionResult GetExpanseTitle(int? page = null, int? pageSize = null, string name = null)
        {
            var viewModal = new ExpenseBookingTitleViewModel();
            var result = _unitOfWork.ExpenseBookingTitleAmount.GetAllTitle(Convert.ToInt32(page), Convert.ToInt32(pageSize));
            if (result.Count > 0)
            {
                var titleModels = new List<ExpenseBookingTitleModel>();
                foreach (var item in result)
                {
                    titleModels.Add(new ExpenseBookingTitleModel { Id = item.Id, Amount = item.Amount, TitleId = item.TitleId, TitleName = item.TitleName });
                }
                viewModal.TotalCount = result.TotalCount;
                viewModal.ExpenseBookingTitleModel = titleModels;
            }
            viewModal.TitleList = GetTitleList();
            return Ok(viewModal);
        }

        [HttpPost("createexpensetitle")]
        [Produces(typeof(ExpenseBookingTitleModel))]
        public IActionResult CreateExpenseTitle([FromBody]ExpenseBookingTitleModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (model == null)
                        return BadRequest($"{nameof(model)} cannot be null");
                    var objExpTitleAmount = new ExpenseBookingTitleAmount()
                    {
                        Amount = model.Amount,
                        TitleID = Guid.Parse(model.TitleId)
                    };
                    _unitOfWork.ExpenseBookingTitleAmount.Add(objExpTitleAmount);
                    _unitOfWork.SaveChanges();
                    return NoContent();
                }
                return BadRequest(ModelState);
            }
            catch (DbUpdateException ex) when (ex.InnerException is SqlException sqlException && (sqlException.Number == 2627 || sqlException.Number == 2601))
            {
                return BadRequest("Title already exists");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.GetBaseException().Message);
            }
        }

        [HttpPut("updateexpensetitle/{id}")]
        public IActionResult UpdateExpenseTitle(string id, [FromBody]ExpenseBookingTitleModel model)
        {
            try
            {
                if (id != null)
                {
                    if (ModelState.IsValid)
                    {
                        if (model == null)
                            return BadRequest($"{nameof(model)} cannot be null");
                        var item = _unitOfWork.ExpenseBookingTitleAmount.Get(Guid.Parse(id));
                        if (item != null)
                        {
                            item.Amount = model.Amount;
                            item.TitleID = Guid.Parse(model.TitleId);
                            _unitOfWork.ExpenseBookingTitleAmount.Update(item);
                            _unitOfWork.SaveChanges();
                            return NoContent();
                        }
                        return BadRequest("item cannot be null");
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

        [HttpDelete("deleteexpensetitle/{id}")]
        public IActionResult DeleteExpenseTitle(string id)
        {
            try
            {
                if (id != null)
                {
                    var check = _unitOfWork.ExpenseBookingTitleAmount.Get(Guid.Parse(id));
                    if (check != null)
                    {
                        _unitOfWork.ExpenseBookingTitleAmount.Remove(check);
                        _unitOfWork.SaveChanges();
                        return NoContent();
                    }
                    return BadRequest("check cannot be null");
                }
                return BadRequest("id cannot be null");
            }
            catch (Exception ex)
            {

                return BadRequest(ex.GetBaseException().Message);
            }
        }
        #endregion

        private List<DropDownList> GetSubCategoryList(Guid id)
        {
            var viewModel = new List<DropDownList>();
            var subCategory = _unitOfWork.SubCategoryItem.GetSubCategory(id);
            if (subCategory.Count > 0)
            {
                foreach (var item in subCategory)
                {
                    viewModel.Add(new DropDownList { Value = item.Id.ToString(), Label = item.Name });
                }
            }
            return viewModel;
        }

        private List<DropDownList> GetCategoryList()
        {
            var viewModel = new List<DropDownList>();
            var category = _unitOfWork.SubCategoryItem.GetCategory();
            if (category.Count > 0)
            {
                foreach (var item in category)
                {
                    viewModel.Add(new DropDownList { Value = item.Id.ToString(), Label = item.Name });
                }
            }
            return viewModel;
        }

        private List<DropDownList> GetTitleList()
        {
            var viewModel = new List<DropDownList>();
            var title = _unitOfWork.Title.GetAll();
            if (title.Count() > 0)
            {
                foreach (var item in title)
                {
                    viewModel.Add(new DropDownList { Value = item.Id.ToString(), Label = item.Name });
                }
            }
            return viewModel;
        }
    }
}