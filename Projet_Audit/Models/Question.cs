using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projet_Audit.Models
{
    public class Question
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_question { get; set; }
        public String NumQuestion { get; set; }
        [Required]
        public String MainQuestion { get; set; }
        [Required]
        public Boolean? Reponse { get; set; }
        [Required]
        public String Commentaire { get; set; }
        [Required]
        public int Coefficient { get; set; }
        [Required]
        public String Recommandation { get; set; }
        [Required]
        public String MesurePropose { get; set; }
        [Required]
        public virtual int Id_risque { get; set; }
        public virtual Risque Risque { get; set; }

    }
}