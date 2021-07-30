using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OnlineLibrary.DataAccess.Data;
using OnlineLibrary.DataAccess.Repository.IRepository;
using OnlineLibrary.Models;

namespace OnlineLibrary.DataAccess.Repository
{
    class ChapterRepository : Repository<Chapter>, IChapterRepository
    {
        private readonly ApplicationDbContext _db;

        public ChapterRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }

        public void Update(Chapter chapter)
        {
            var objFromDb = _db.Chapters.FirstOrDefault(s => s.Id == chapter.Id);
            if(objFromDb != null)
            {
                objFromDb.Content = chapter.Content;
                objFromDb.Document.Title = chapter.Document.Title;
                objFromDb.Document = chapter.Document;
            }    
        }
    } 
}
