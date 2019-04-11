using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIYuxiao.Models
{
    public class Artist
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Required]
        [StringLength(20)]
        public string Nom { get; set; }

        [Required]
        [StringLength(20)]
        public string Prenom { get; set; }

        [StringLength(15)]
        public string Style { get; set; }

        [StringLength(50)]
        public string Commentaire { get; set; }

        [StringLength(20)]
        public string Pays { get; set; }

        [StringLength(100)]
        public string ExtraitM { get; set; }
    }


}