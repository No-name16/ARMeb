using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ARMeb.Models
{
    public class tblBook
    {
        [Column("BookId")]
        public int Id { get; set; }
        public string Bookname { get; set; }
        public string BookAuthor { get; set; }
        public int NumOfBooks { get; set; }
        public bool IsAny { get; set; }
    }
}
