using FifaFinderAPI.Library.Data;
using FifaFinderAPI.Library.Interfaces;
using FifaFinderAPI.Library.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FifaFinderAPI.Library.Repositories
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext dbContext) : base(dbContext)
        {

        }
    }
}
