﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace Assignment3.Models
{
    public class Provider
    {
        /// <summary>
        /// Autogenerated, ID of the provider
        /// </summary>
        [Key]
        public Guid Id { get; set; }

        /// <summary>
        /// Autogenerated, time at which the record was created
        /// </summary>
        [Required]
        public DateTimeOffset CreationTime { get; set; }

        /// <summary>
        /// First name of the provider
        /// </summary>
        [Required]
        [StringLength(128)]
        public string FirstName { get; set; }

        /// <summary>
        /// Last name of the provider
        /// </summary>
        [Required]
        [StringLength(128)]
        public string LastName { get; set; }

        /// <summary>
        /// License number of the provider
        /// </summary>
        [Required]
        public uint LicenseNumber { get; set; }

        /// <summary>
        /// Address of the provider
        /// </summary>
        [Required]
        public string Address { get; set; }
    }
}