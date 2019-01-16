using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projet_Audit.Models
{
    public class Risque
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_risque { get; set; }
        [Required]
        public String Type { get; set; }
        public int score { get; set; }

        public virtual ICollection<Question> Questions { get; set; }

    }
}