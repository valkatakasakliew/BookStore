using System;
using System.Collections.Generic;
using System.Text;
using BookStore.Data.Interfaces;
using BookStore.Data.Repositories;

namespace BookStore.Data.Factories
{
    public class RepositoryFactory : IRepositoryFactory
    {
        public IStore StoreRepository => new Store();
    }
}
