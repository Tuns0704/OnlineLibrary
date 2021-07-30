using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineLibrary.Models
{
    public enum StatusName
    {
        Requested,
        Approved,
        Rejected
    }
    public class RequestAccess
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Request Access")]

        public DateTime TimeAccess { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }
        [ForeignKey("ApplicationUserId")]
        public ApplicationUser ApplicationUser { get; set; }

        [Required]
        public int DocumentId { get; set; }
        public Document Document{ get; set;}

        public StatusName Status { get; set; } 

    }
}
