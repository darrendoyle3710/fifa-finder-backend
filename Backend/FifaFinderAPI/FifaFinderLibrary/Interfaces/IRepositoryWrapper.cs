
using FifaFinderAPI.Library.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace FifaFinderAPI.Library.Interfaces
{
    public interface IRepositoryWrapper
    {
        IUserRepository Users { get; }
        IPostRepository Posts { get; }
        void Save();
    }
}
