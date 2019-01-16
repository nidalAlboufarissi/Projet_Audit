using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Projet_Audit.Models
{
    public class UserRisque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public double score { get; set; }
        public String Status { get; set; }
        public int NombreQst { get; set; }
        public int QstTraite { get; set; }
        public int NombreOui { get; set; }
        public int NombreNon { get; set; }
        public int NombreNull { get; set; }

        [Required]
        public int Id_risque { get; set; }
        public virtual Risque Risque { get; set; }

        public virtual UserEnregistrement Enregistrement { get; set; }

    }
}
