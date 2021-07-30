using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Models
{
    public class Document
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }

        public string Desription { get; set; }
        [Required]
        public string ISBN { get; set; }

        [Required]
        [Range(1, 10000)]
        public double Price { get; set; }

        public string ImageUrl { get; set; }

        [Required]
        public int CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }

        public ICollection<Chapter> Chapter { get; set; }
        public Document()
        {
            this.Chapter = new List<Chapter>();
        }

        [Required]
        public int AuthorId { get; set; }
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
