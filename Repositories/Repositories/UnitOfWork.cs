using AASTHA2.Entities;
using AASTHA2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Repositories.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AASTHAContext _repoContext;
        private IUserRepository _user;

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }

                return _user;
            }
        }
        public UnitOfWork(AASTHAContext repositoryContext)
        {
            _repoContext = repositoryContext;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}
