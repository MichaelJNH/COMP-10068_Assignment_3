using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment3.Models
{
    /// <summary>
    /// An immunization received by a patient
    /// </summary>
    public class ErrorResponse
    {
        /// <summary>
        /// ID of the error
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Status code of the error
        /// </summary>
        [Required]
        public int StatusCode { get; set; }

        /// <summary>
        /// Error message content
        /// </summary>
        [Required]
        public string Message { get; set; }
    }
}
