using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace StudentInformationJS.Entities
{
    public class StudentMaster
    {
        [Key]
        public string ID { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string FatherName { get; set; }
        [Required]
        public string Gender { get; set; }
        [Required]
        public string ClassName { get; set; }
        [Required]
        public DateTime DateOfAdmission { get; set; }

    }
}