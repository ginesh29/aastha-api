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
        private IOpdRepository _opd;
        private IIpdRepository _ipd;
        private ILookupRepository _lookup;
        public UnitOfWork(AASTHAContext AASTHAContext)
        {
            _AASTHAContext = AASTHAContext;
        }

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
        public IOpdRepository Opds
        {
            get
            {
                if (_opd == null)
                {
                    _opd = new OpdRepository(_AASTHAContext);
                }
                return _opd;
            }
        }
        public IIpdRepository Ipds
        {
            get
            {
                if (_ipd == null)
                {
                    _ipd = new IpdRepository(_AASTHAContext);
                }
                return _ipd;
            }
        }
        public ILookupRepository Lookups
        {
            get
            {
                if (_lookup == null)
                {
                    _lookup = new LookupRepository(_AASTHAContext);
                }
                return _lookup;
            }
        }
        public void SaveChanges()
        {
            _AASTHAContext.SaveChanges();
        }
    }
}
