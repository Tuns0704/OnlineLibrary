using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineLibrary.DataAccess.Data;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository
{
    class RequestAccessRepository : Repository<RequestAccess>, IRequestAccessRepository
    {
        private readonly ApplicationDbContext _db;

        public RequestAccessRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }
    } 
}
