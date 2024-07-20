using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace CR1.Model
{
    public class login
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int loginid { get; set; }
        public string email { get; set; }
        public string password { get; set; }
        public string role { get; set; }
    }
}
