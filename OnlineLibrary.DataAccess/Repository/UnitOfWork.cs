using System;
using System.Collections.Generic;
using System.Text;
using OnlineLibrary.DataAccess.Data;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _db;

        public UnitOfWork(ApplicationDbContext db)
        {
            _db = db;
            Category = new CategoryRepository(_db);
            Author = new AuthorRepository(_db);
            ApplicationUser = new ApplicationUserRepository(_db);
            Document = new DocumentRepository(_db);
            //RequestAccess = new RequestAccessRepository(_db);
            Chapter = new ChapterRepository(_db);
            SP_Call = new SP_Call(_db);
        }
        public IApplicationUserRepository ApplicationUser { get; set; }
        public ICategoryRepository Category { get; set; }
        public IChapterRepository Chapter { get; set; }
        //public IRequestAccessRepository RequestAccess { get; set; }
        public IAuthorRepository Author { get; set; }
        public IDocumentRepository Document { get; set; }
        public ISP_Call SP_Call { get; set; }
        public void Dispose()
        {
            _db.Dispose();
        }
        public void Save()
        {
            _db.SaveChanges();
        }
    }
}
