using A4.DAL.Entites.Blog;
using DAL.Repositories;
using Microsoft.EntityFrameworkCore;
using DAL;
using System.Linq;
using System.Collections.Generic;

namespace A4.DAL.Repositories
{
    public class BlogRepository : Repository<Blog>, IBlogRepository
    {
        public BlogRepository(DbContext context) : base(context)
        {
        }
        private ApplicationDbContext _appContext => (ApplicationDbContext)_context;

        public PagedList<Blog> GetAllBlogs(int pageIndex = 0, int pageSize = int.MaxValue, string name = "")
        {
            var blogList = from b in _appContext.Blog
                           select new Blog
                           {
                               Id=b.Id,
                               Title = b.Title,
                               Content = b.Content,
                               ShortDescription=b.ShortDescription,
                               IsPublished = b.IsPublished,
                               PublishedDate = b.PublishedDate,
                               ThumbnailImage=b.ThumbnailImage,
                               ThumbnailImagePath=b.ThumbnailImagePath,
                               HeaderImage=b.HeaderImage,
                               HeaderImagePath=b.HeaderImagePath
                           };
            if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
                blogList = blogList.Where(b=>b.Title.Contains(name));

            blogList = blogList.OrderByDescending(c => c.PublishedDate);

            return new PagedList<Blog>(blogList, pageIndex, pageSize);
        }

        public PagedList<Blog> GetAllPublishedBlogs(int pageIndex = 0, int pageSize = int.MaxValue)
        {
            var blogList = _appContext.Blog.Where(b => b.IsPublished);
            //if (!string.IsNullOrWhiteSpace(name) && name != "undefined")
            //    blogList = blogList.Where(b => b.Title.Contains(name));
            
            return new PagedList<Blog>(blogList, pageIndex, pageSize);
        }

        public Blog GetByUrlSlug(string urlSlug)
        {
            var blog=  _appContext.Blog.Where(b=>b.UrlSlug==urlSlug).FirstOrDefault();
            return blog;                      
        }
    }
}
