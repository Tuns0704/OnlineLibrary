using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineLibrary.DataAccess.Data;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository
{
    class AuthorRepository : Repository<Author>, IAuthorRepository
    {
        private readonly ApplicationDbContext _db;

        public AuthorRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Author author)
        {
            var objFromDb = _db.Authors.FirstOrDefault(s => s.Id == author.Id);
            if(objFromDb != null)
            {
                objFromDb.Name = author.Name;
            }    
        }
    } 
}
