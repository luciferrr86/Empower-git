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
    [Route("api/[controller]")]
    [ApiController]
    public class BlogsClientController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public BlogsClientController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpGet("publishedList/{page?}/{pageSize?}")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        [Produces(typeof(BlogVM))]
        public ActionResult GetPublishedBlogs(int? page = null, int? pageSize = null)
        {
            try
            {
                var result = new BlogListVM();
                var model = new List<BlogVM>();

                var blogList = _unitOfWork.Blog.GetAllPublishedBlogs(Convert.ToInt32(page), Convert.ToInt32(pageSize));
                if (blogList.Any())
                {
                    foreach (var blog in blogList)
                    {
                        model.Add(new BlogVM
                        {
                            Id = blog.Id.ToString(),
                            Title = blog.Title,
                            Content = blog.Content,
                            IsPublished = blog.IsPublished,
                            UrlSlug = blog.UrlSlug,
                            PublishedDate = blog.PublishedDate,
                            ThumbnailImagePath = GetImageFullPath(blog.ThumbnailImagePath),
                            HeaderImagePath = GetImageFullPath(blog.HeaderImagePath)
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

        [HttpGet("{urlSlug}")]
        [ResponseCache(Duration = 120, Location = ResponseCacheLocation.Client)]
        [Produces(typeof(BlogVM))]
        public ActionResult GetByUrlSlug(string urlSlug)
        {
            try
            {
                var blog = _unitOfWork.Blog.GetByUrlSlug(urlSlug);

                if (blog == null)
                {
                    return NotFound();
                }

                var viewModel = new BlogVM
                {
                    Id = blog.Id.ToString(),
                    Title = blog.Title,
                    Content = blog.Content,
                    IsPublished = blog.IsPublished,
                    PublishedDate = blog.PublishedDate,
                    UrlSlug = blog.UrlSlug,
                    ThumbnailImagePath = GetImageFullPath(blog.ThumbnailImagePath),
                    HeaderImagePath = GetImageFullPath(blog.HeaderImagePath)
                };
                return Ok(viewModel);
            }
            catch (Exception ex)
            {
                return BadRequest($"An error occurred:{ex.GetBaseException().Message}");
            }
        }
        #region Private Methods
        private string GetImageFullPath(string fileName)
        {
            if (string.IsNullOrEmpty(fileName))
                return null;
            Console.WriteLine($"FileName-api: {fileName}");
            Console.WriteLine($"Path: {Request.Scheme}://{Request.Host}/{fileName.Replace("\\", "/")}");

            return $"{Request.Scheme}://{Request.Host}/{fileName.Replace("\\", "/")}";
        }
        #endregion
    }
}
