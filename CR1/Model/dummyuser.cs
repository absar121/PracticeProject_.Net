using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.VisualBasic;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CR1.Model
{
    public class dummyuser
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int userid { get; set; }
        [StringLength(150)]
        public string username { get; set; }
        public int usermobileno { get; set; }
        public string userdob { get; set; }
      

    }
}
