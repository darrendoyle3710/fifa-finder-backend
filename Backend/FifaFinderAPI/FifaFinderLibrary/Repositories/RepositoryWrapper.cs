using FifaFinderAPI.Library.Data;
using FifaFinderAPI.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FifaFinderAPI.Library.Repositories
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        ApplicationDbContext _repoContext;
        public RepositoryWrapper(ApplicationDbContext repoContext)
        {
            _repoContext = repoContext;
        }
        IUserRepository _users;

        IPostRepository _posts;

        public IUserRepository Users
        {
            get
            {
                if (_users == null)
                {
                    _users = new UserRepository(_repoContext);
                }
                return _users;
            }
        }
        public IPostRepository Posts
        {
            get
            {
                if (_posts == null)
                {
                    _posts = new PostRepository(_repoContext);
                }
                return _posts;
            }
        }

        void IRepositoryWrapper.Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
