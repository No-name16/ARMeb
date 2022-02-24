using ARMeb.Contracts;
using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ARMeb.Repository
{
    public class ReaderRepository : RepositoryBase<Readers>, IReaderRepository
    {
        private readonly ARMebContext _context;

        public ReaderRepository(ARMebContext context)
            : base(context)
        {
            _context = context;
        }

        public void CreateReader(Readers reader) => Create(reader);

        public void UpdateReader(Readers reader) => Update(reader);
        public void DeleteReader(Readers reader) => Delete(reader);

        public IEnumerable<Readers> GetAllReaders(bool trackChanges) =>
            FindAll(trackChanges)
            .Include(x => x.TblBooks)
            .OrderBy(x => x.Id)
            .ToList();

        public Readers GetReader(int id, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(id), trackChanges)
            .FirstOrDefault();
    }
}
