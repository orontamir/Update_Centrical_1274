using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _1274_RECUITE.Models
{
    public class Portal
    {

        public int Id { get; set; }

        [Required]
        [MaxLength(50, ErrorMessage = "Name can not be exceed 50 charecters")]
        public string Name { get; set; }

        [Required]
        public string PhotoPath { get; set; }

        public string Link { get; set; }

    }
}
