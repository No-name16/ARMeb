using ARMeb.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace ARMeb.Contracts
{
    public interface IReaderRepository
    {
        IEnumerable<Readers> GetAllReaders(bool trackChanges);
        Readers GetReader(int id, bool trackChanges);
        void CreateReader(Readers reader);
        void DeleteReader(Readers reader);
        void UpdateReader(Readers reader);
    }
}
