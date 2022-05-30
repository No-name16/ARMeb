using ARMeb.Contracts;
using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace ARMeb.Repository
{
    public class OperationRepository : RepositoryBase<Operations>
    {
        private readonly ARMebContext _context;

        public OperationRepository(ARMebContext context)
            : base(context)
        {
            _context = context;
        }

        public void CreateOperation(Operations operation) => Create(operation);

        public void UpdateOperation(Operations operation) => Update(operation);
        public void DeleteOperation(Operations operation) => Delete(operation);

        public IEnumerable<Operations> GetAllOperations(bool trackChanges) =>
            FindAll(trackChanges)
            .OrderBy(x => x.Id)
            .ToList();

        public Operations GetOperation(int id, bool trackChanges) =>
            FindByCondition(c => c.Id.Equals(id), trackChanges)
            .FirstOrDefault();
    }

}
