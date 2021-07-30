using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OnlineLibrary.Models
{
    public class Author
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Author")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}
