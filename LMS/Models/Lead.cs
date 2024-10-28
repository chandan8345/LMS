using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Lead
    {
        [Key]
        public int LeadId { get; set; }

        [Required]
        [MaxLength(100)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        [MaxLength(255)]
        public string Email { get; set; }

        [MaxLength(15)]
        public string PhoneNumber { get; set; }

        [MaxLength(50)]
        public string Source { get; set; }

        [MaxLength(50)]
        public string Status { get; set; }

        public int? ProductId { get; set; }

        public int? AssignedAgentId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
