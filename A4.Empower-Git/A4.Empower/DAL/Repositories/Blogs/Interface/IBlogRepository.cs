using DAL.Repositories;
using System;
using System.Collections.Generic;
using System.Text;
using A4.DAL.Entites.Blog;
using DAL.Repositories.Interfaces;
using A4.DAL.Entites;

namespace A4.DAL.Repositories
{
    public interface IBlogRepository : IRepository<Blog>
    {
        PagedList<Blog> GetAllBlogs(int pageIndex = 0, int pageSize = int.MaxValue, string name = "");
        PagedList<Blog> GetAllPublishedBlogs(int pageIndex = 0, int pageSize = int.MaxValue);
        Blog GetByUrlSlug(string urlSlug);
    }
}
