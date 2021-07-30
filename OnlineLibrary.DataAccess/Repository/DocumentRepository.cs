using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineLibrary.DataAccess.Data;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository
{
    class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        private readonly ApplicationDbContext _db;

        public DocumentRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Document document)
        {
            var objFromDb = _db.Documents.FirstOrDefault(s => s.Id == document.Id);
            if(objFromDb != null)
            {
                if (document.ImageUrl != null)
                {
                    objFromDb.ImageUrl = document.ImageUrl;
                }
                objFromDb.ISBN = document.ISBN;
                objFromDb.Price = document.Price;
                objFromDb.Title = document.Title;
                objFromDb.Desription = document.Desription;
                objFromDb.CategoryId = document.CategoryId;
                objFromDb.Author = document.Author;
                objFromDb.AuthorId = document.AuthorId;
                //objFromDb.RequestAccess = document.RequestAccess;
                objFromDb.Chapter = document.Chapter;
            }    
        }
    } 
}
