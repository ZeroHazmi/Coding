using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace prasApi.Dtos.Feedback
{
    public class FeedbackUpdateDto
    {
        [Required]
        public string UserId { get; set; } = string.Empty;
        [Required]
        public int Rating { get; set; }
        [Required]
        public string Comment { get; set; } = string.Empty;
    }
}