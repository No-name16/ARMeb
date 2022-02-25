using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ARMeb.Models
{
    public class Operations
    {
        [Column("OperationId")]
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime Time { get; set; }
    }
}
