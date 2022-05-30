using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMeb.Models
{
    public class Readers
    {
        [Column("ReaderId")]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public bool HaveBooks { get; set; }
        public int? BookId { get; set; }      
        public virtual tblBook TblBooks { get; set; }  
    }
}
