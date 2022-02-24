using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMeb.Models
{
    public class tblUser
    {
        [Column("UserId")]
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    
    }
}
