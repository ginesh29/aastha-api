using AASTHA2.Entities;
using AASTHA2.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AASTHAContext _AASTHAContext;
        private IUserRepository _user;

        public IUserRepository Users
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_AASTHAContext);
                }

                return _user;
            }
        }
        public UnitOfWork(AASTHAContext AASTHAContext)
        {
            _AASTHAContext = AASTHAContext;
        }
        public void SaveChanges()
        {
            _AASTHAContext.SaveChanges();
        }
    }
}
