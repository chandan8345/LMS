using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Required]
        [MaxLength(100)]
        public string ProductName { get; set; }

        [MaxLength(50)]
        public string InsuranceType { get; set; }

        public string PlanDetails { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
