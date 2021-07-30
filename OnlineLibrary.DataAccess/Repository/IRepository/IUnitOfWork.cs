using System;
using System.Collections.Generic;
using System.Text;

namespace OnlineLibrary.DataAccess.Repository.IRepository
{
    public interface IUnitOfWork : IDisposable
    {
        ICategoryRepository Category { get; }
        IAuthorRepository  Author{ get; }
        IDocumentRepository Document { get; }
        //IRequestAccessRepository RequestAccess { get; }
        IApplicationUserRepository ApplicationUser { get; }
        IChapterRepository Chapter { get; }

        ISP_Call SP_Call { get; }

        void Save();
    }
}
