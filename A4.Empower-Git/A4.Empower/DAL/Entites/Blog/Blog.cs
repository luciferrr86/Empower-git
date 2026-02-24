using Microsoft.AspNetCore.Http;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace A4.DAL.Entites.Blog
{
    public class Blog : AuditableEntity
    {
        public Blog()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ShortDescription { get; set; }
        public DateTime PublishedDate { get; set; }
        public bool IsPublished { get; set; }
        public string UrlSlug { get; set; }
        [NotMapped]
        public IFormFile ThumbnailImage { get; set; }
        [NotMapped]
        public IFormFile HeaderImage { get; set; }
        public string ThumbnailImagePath { get; set; }
        public string HeaderImagePath { get; set; }
    }
}
