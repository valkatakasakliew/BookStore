using BookStore.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace BookStore.Data.Factories
{
    public interface IRepositoryFactory
    {
        IStore StoreRepository { get; }
    }
}
