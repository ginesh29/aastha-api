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
        private IPatientRepository _patient;
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
        public IPatientRepository Patients
        {
            get
            {
                if (_patient == null)
                {
                    _patient = new PatientRepository(_AASTHAContext);
                }

                return _patient;
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
