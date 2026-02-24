using A4.BAL.Blog;
using A4.DAL.Entites.Blog;
using DAL;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System;

namespace A4.Empower.Controllers
{
    //[Authorize(AuthenticationSchemes = OpenIddict.Validation.AspNetCore.OpenIddictValidationAspNetCoreDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _hostEnvironment;
        public BlogsController(IUnitOfWork unitOfWork, IWebHostEnvironment hostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _hostEnvironment = hostEnvironment;
        }

        #region Blog Operations
        [HttpGet("list/{page?}/{pageSize?}/{name?}")]
        [Produces(typeof(BlogListVM))]
        public ActionResult GetAll(int? page = null, int? pageSize = null, string name = null)

        {
            try
            {
                var result = new BlogListVM();
                var model = new List<BlogVM>();

                var blogList = _unitOfWork.Blog.GetAllBlogs(Convert.ToInt32(page), Convert.ToInt32(pageSize), name);
                if (blogList.Count() > 0)
                {
                    foreach (var blog in blogList)
                    {
                        model.Add(new BlogVM
                        {
                            Id = blog.Id.ToString(),
                            Title = blog.Title,
                            Content = blog.Content,
                            IsPublished = blog.IsPublished,
                            PublishedDate = blog.PublishedDate,
                            ThumbnailImagePath = blog.ThumbnailImagePath,
                            HeaderImagePath = blog.HeaderImagePath
                        });
                    }
                }
                result.BlogVM = model;
                result.TotalCount = blogList.TotalCount;
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred:{ex.GetBaseException().Message}");
            }
        }

        [HttpGet("blog/{id}")]
        [Produces(typeof(BlogVM))]
        public ActionResult GetById(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var blogId))
                {
                    return BadRequest("Invalid blog ID format");
                }

                var blog = _unitOfWork.Blog.Get(blogId);

                if (blog == null)
                {
                    return NotFound(id);
                }

                var viewModel = new BlogVM
                {
                    Id = blog.Id.ToString(),
                    Title = blog.Title,
                    Content = blog.Content,
                    IsPublished = blog.IsPublished,
                    PublishedDate = blog.PublishedDate,
                    UrlSlug = blog.UrlSlug,
                    ThumbnailImagePath = blog.ThumbnailImagePath,
                    HeaderImagePath = blog.HeaderImagePath
                };
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred:{ex.GetBaseException().Message}");
            }
        }

        [HttpPost("manageBlog")]
        public async Task<IActionResult> ManageBlog([FromForm] BlogCreateVM model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Content))
                        return BadRequest("Title and Content cannot be empty");

                    if (model.ThumbnailImage != null && !IsValidImageFile(model.ThumbnailImage))
                    {
                        return BadRequest("Invalid thumbnail image file format or size.");
                    }

                    if (model.HeaderImage != null && !IsValidImageFile(model.HeaderImage))
                    {
                        return BadRequest("Invalid header image file format or size.");
                    }

                    await UpdateImagePathIfNeeded("ThumbnailImagePath", model.ThumbnailImage, model, "Blogs");
                    await UpdateImagePathIfNeeded("HeaderImagePath", model.HeaderImage, model, "Blogs");

                    Blog blog;
                    if (!string.IsNullOrWhiteSpace(model.Id) && model.Id != "undefined")
                    {
                        blog = _unitOfWork.Blog.Get(Guid.Parse(model.Id));
                        if (blog == null)
                        {
                            return NotFound($"Blog with ID '{model.Id}' not found");
                        }
                        
                        model.ThumbnailImagePath = model.ThumbnailImage != null
                            ? await UploadImage(model.ThumbnailImage, "Blogs")
                            : blog.ThumbnailImagePath;

                        model.HeaderImagePath = model.HeaderImage != null
                            ? await UploadImage(model.HeaderImage, "Blogs")
                            : blog.HeaderImagePath;
                        Console.WriteLine(model.HeaderImagePath);
                        MapBlogProperties(model, blog);
                        _unitOfWork.Blog.Update(blog);
                    }
                    else
                    {
                        blog = new Blog();
                        model.ThumbnailImagePath = model.ThumbnailImage != null
                   ? await UploadImage(model.ThumbnailImage, "Blogs")
                   : null;

                        model.HeaderImagePath = model.HeaderImage != null
                            ? await UploadImage(model.HeaderImage, "Blogs")
                            : null;
                        MapBlogProperties(model, blog);
                        blog.IsPublished = false;
                        _unitOfWork.Blog.Add(blog);
                    }
                    Console.WriteLine($"Request Scheme: {Request.Scheme}, Request Host: {Request.Host}");

                    _unitOfWork.SaveChanges();
                    return Ok(blog);
                }
                return BadRequest(ModelState);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.GetBaseException().Message}");
            }
        }

        [HttpDelete("delete/{id}")]
        public ActionResult Delete(string id)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(id) || !Guid.TryParse(id, out var blogId))
                {
                    return BadRequest("Invalid blog ID format");
                }

                var blogToDelete = _unitOfWork.Blog.Get(blogId);

                if (blogToDelete == null)
                {
                    return NotFound($"Blog with ID '{id}' not found");
                }

                _unitOfWork.Blog.Remove(blogToDelete);
                _unitOfWork.SaveChanges();
                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred:{ex.GetBaseException().Message}");
            }
        }

        [HttpPost("publish/{id}")]
        public ActionResult Publish(string id)
        {
            try
            {
                if (!Guid.TryParse(id, out var blogId))
                {
                    return BadRequest("Invalid blog ID format");
                }
                var blog = _unitOfWork.Blog.Get(blogId);

                if (blog == null)
                {
                    return BadRequest($"Blog with ID '{id}' not found");
                }

                blog.IsPublished = !blog.IsPublished;
                _unitOfWork.Blog.Update(blog);
                _unitOfWork.SaveChanges();

                return NoContent();
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred:{ex.GetBaseException().Message}");
            }
        }
        #endregion

        #region BlogCategory Operations
        #endregion

        #region BlogTag Operations
        #endregion

        #region Private Methods
        private async Task UpdateImagePathIfNeeded(string propertyName, IFormFile file, BlogCreateVM model, string folderName)
        {
            if (file != null)
            {
                model.GetType().GetProperty(propertyName).SetValue(model, await UploadImage(file, folderName));
            }
        }

        private async Task<string> UploadImage(IFormFile file, string folderName)
        {
            var uploadsFolder = Path.Combine(_hostEnvironment.WebRootPath, folderName);
            if (!Directory.Exists(uploadsFolder))
            {
                Directory.CreateDirectory(uploadsFolder);
            }

            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
            var filePath = Path.Combine(uploadsFolder, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            //var baseUrl = $"{Request.Scheme}://{Request.Host.Value.TrimEnd('/')}";
            return $"{folderName}/{uniqueFileName}";
        }

        private void MapBlogProperties(BlogCreateVM model, Blog blog)
        {
            blog.Title = model.Title;
            blog.Content = model.Content;
            blog.PublishedDate = model.PublishedDate;
            blog.UrlSlug = GenerateSeoFriendlyUrl(model.Title);
            blog.ThumbnailImagePath = model.ThumbnailImagePath;
            blog.HeaderImagePath = model.HeaderImagePath;
            //blog.BlogCategoryId = model.CategoryId;

            //var allTags = _unitOfWork.Blog.GetAllTags();
            //blog.BlogTags = allTags.Where(t => model.TagIds.Contains(t.Id)).ToList();
        }

        private string GenerateSeoFriendlyUrl(string blogTitle)
        {
            if (string.IsNullOrWhiteSpace(blogTitle))
                return string.Empty;

            string slug = blogTitle.ToLower();

            // Remove special characters except spaces and hyphens
            slug = Regex.Replace(slug, @"[^a-z0-9\s-]", "");

            // Replace multiple spaces or hyphens with a single hyphen
            slug = Regex.Replace(slug, @"[\s-]+", "-").Trim('-');

            // Add a hyphen after every word
            slug = Regex.Replace(slug, @"\s+", "-") + "-";

            return slug;
        }

        private bool IsValidImageFile(IFormFile file)
        {
            if (file == null) return false;

            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };
            var fileExtension = Path.GetExtension(file.FileName).ToLower();
            return file.Length > 0 && allowedExtensions.Contains(fileExtension);
        }

        #endregion
    }
}
