using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineLibrary.Models.ViewModels
{
    public class DocumentVM
    {
        public Document Document { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; } 
        public IEnumerable<SelectListItem> AuthorList { get; set; }
        public IEnumerable<SelectListItem> ChapterList { get; set; }
    }
}
