using System;
using System.Collections.Generic;
using System.Text;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository.IRepository
{
    public interface IAuthorRepository : IRepository<Author>
    {
        void Update(Author author);
    }
}
