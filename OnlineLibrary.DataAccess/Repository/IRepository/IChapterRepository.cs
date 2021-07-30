using System;
using System.Collections.Generic;
using System.Text;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository.IRepository
{
    public interface IChapterRepository : IRepository<Chapter>
    {
        void Update(Chapter chapter);
    }
}
