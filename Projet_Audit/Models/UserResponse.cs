using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projet_Audit.Models
{
    public class UserResponse
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public Boolean? Reponse { get; set; }
        public int score { get; set; }
        [Required]
        public virtual Question Question { get; set; }

        public virtual int id_Record { get; set; }
        public virtual UserEnregistrement UserEnregistrement { get; set; }
    }
}