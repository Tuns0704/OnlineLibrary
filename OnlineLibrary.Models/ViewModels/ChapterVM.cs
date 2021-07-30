using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace OnlineLibrary.Models.ViewModels
{
    public class ChapterVM
    {
        public Chapter Chapter { get; set; }
        public IEnumerable<SelectListItem> DocumentList { get; set; }
    }
}
