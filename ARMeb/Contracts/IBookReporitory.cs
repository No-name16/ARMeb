using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARMeb.Contracts
{
    public interface IBookRepository
    {
        IEnumerable<tblBook> GetAllBooks(bool trackChanges);
        tblBook GetBook(int id, bool trackChanges);
        void CreateBook(tblBook book);
        void DeleteBook(tblBook book);
        void UpdateBook(tblBook book);
        public int GetId(string name, bool trackChanges);
    }
}
