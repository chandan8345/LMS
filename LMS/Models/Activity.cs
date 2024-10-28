using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using LMS.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Models
{
    public class Activity
    {
        [Key]
        public int ActivityId { get; set; }

        [Required]
        public int LeadId { get; set; }
        [ForeignKey("LeadId")]
        public Lead Lead { get; set; }

        [Required]
        public int AgentId { get; set; }
        [ForeignKey("AgentId")]
        public Agent Agent { get; set; }

        [MaxLength(50)]
        public string ActivityType { get; set; }

        public string ActivityNotes { get; set; }

        public DateTime ActivityDate { get; set; } = DateTime.UtcNow;
    }
}
