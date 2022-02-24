using ARMeb.Contracts;
using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARMeb.Repository
{
    public class RepositoryManager : IRepositoryManager
    {
        private readonly ARMebContext _repositoryContext;

        private IReaderRepository _readerRepository;

        public RepositoryManager(ARMebContext repository)
        {
            _repositoryContext = repository;
        }

        public IReaderRepository Readers
        {
            get
            {
                if (_readerRepository == null)
                    _readerRepository = new ReaderRepository(_repositoryContext);

                return _readerRepository;
            }
        }

        public void Save() => _repositoryContext.SaveChanges();
    }
}
