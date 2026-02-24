using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace A4.BAL.Blog
{
    public class BlogListVM
    {
        public BlogListVM()
        {
            BlogVM = new List<BlogVM>();
        }
        public List<BlogVM> BlogVM { get; set; }
        public int TotalCount { get; set; }
    }
    public class BlogVM
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        public string UrlSlug { get; set; }
        public IFormFile ThumbnailImage { get; set; }
        public IFormFile HeaderImage { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string HeaderImagePath { get; set; }
    }
}
