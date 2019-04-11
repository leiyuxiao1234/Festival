using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace APIYuxiao.Models
{
    public class Place
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(30)]
        public string Nom { get; set; }

        public int Capacite { get; set; }

        [StringLength(100)]
        public string Description { get; set; }

        public int IDFestival { get; set; }
    }
}