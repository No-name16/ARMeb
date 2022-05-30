using ARMeb.Contracts;
using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARMeb.Repository
{
    public class RepositoryManager 
    {
        private readonly ARMebContext _repositoryContext;

        private BookRepository _bookRepository;
        private ReaderRepository _readerRepository;
        private OperationRepository _operationRepository;
        public RepositoryManager(ARMebContext repository)
        {
            _repositoryContext = repository;
        }

        public BookRepository Books
        {
            get
            {
                if (_bookRepository == null)
                    _bookRepository = new BookRepository(_repositoryContext);

                return _bookRepository;
            }
        }
        public OperationRepository Operations
        {
            get
            {
                if (_operationRepository == null)
                    _operationRepository = new OperationRepository(_repositoryContext);

                return _operationRepository;
            }
        }
        public ReaderRepository Readers
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
