using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LMS.Models
{
    public class RefreshToken
    {
        public string Token { get; set; }
        public string UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
    }
}
