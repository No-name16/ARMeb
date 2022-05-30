using ARMeb.Repository;
using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ARMeb.Repository
{
    public class BookRepository : RepositoryBase<tblBook>
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
    }
}
   