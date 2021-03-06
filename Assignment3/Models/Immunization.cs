using Microsoft.EntityFrameworkCore;
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
    public class Immunization
    {
        /// <summary>
        /// Autogenerated, ID of the immunization
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Autogenerated, time at which the record was created
        /// </summary>
        [Required]
        public DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// Official name of the immunization
        /// </summary>
        [Required]
        [StringLength(128)]
        public string OfficialName { get; set; }

        /// <summary>
        /// Trade name of the immunization
        /// </summary>
        [StringLength(128)]
        public string TradeName { get; set; }

        /// <summary>
        /// Lot number of the immunization
        /// </summary>
        [Required]
        [StringLength(255)]
        public string LotNumber { get; set; }

        /// <summary>
        /// Expiration date of the immunization
        /// </summary>
        [Required]
        public DateTimeOffset ExpirationDate { get; set; }

        /// <summary>
        /// Autogenerated, time at which the record was updated
        /// </summary>
        public DateTimeOffset? UpdatedTime { get; set; }
    }
}
