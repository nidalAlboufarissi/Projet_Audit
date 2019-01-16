using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Projet_Audit.Models
{
    public class UserEnregistrement
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id_Record { get; set; }
        [Required]
        public DateTime Date { get; set; }

        public virtual ICollection<UserResponse> UserResponses { get; set; }
        [Required]
        public virtual string UserName { get; set; }
        public virtual ApplicationUser ApplicationUser { get; set; }

        public virtual ICollection<UserRisque>   UserRisques { get; set; }

    }
}