using System;
using System.Collections.Generic;
using System.Text;

namespace ARMeb.Contracts
{
    public interface IRepositoryManager
    {
        //https://github.com/DmitriVT/FridgeAPI
        public IReaderRepository Readers { get; }

        void Save();
    }
}
