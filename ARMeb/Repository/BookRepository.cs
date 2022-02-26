﻿using ARMeb.Contracts;
using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ARMeb.Repository
{
    public class BookRepository : RepositoryBase<tblBook>, IBookRepository
    {
        private readonly ARMebContext _context;

        public BookRepository(ARMebContext context)
            : base(context)
        {
            _context = context;
        }

        public void CreateBook(tblBook book) => Create(book);

        public void UpdateBook(tblBook book) => Update(book);
        public void DeleteBook(tblBook book) => Delete(book);

        public IEnumerable<tblBook> GetAllBooks(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Id)
            .ToList();

        public tblBook GetBook(int id, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(id), trackChanges)
            .FirstOrDefault();

        public int GetId(string name,bool trackChanges) =>
             FindByCondition(c => c.Bookname.Equals(name), trackChanges)
            .FirstOrDefault()
            .Id;
    }
}