using AASTHA2.Entities.Models;
using AASTHA2.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace AASTHA2.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private AASTHA2Context _AASTHA2Context;
        private IUserRepository _user;
        private IPatientRepository _patient;
        private IOpdRepository _opd;
        private IIpdRepository _ipd;
        private ILookupRepository _lookup;
        private IIpdLookupRepository _ipdLookup;
        private IDeliveryRepository _delivery;
        private IOperationRepository _operation;
        private IChargeRepository _charge;
        private IAppointmentRepository _appointment;
        public UnitOfWork(AASTHA2Context AASTHA2Context)
        {
            _AASTHA2Context = AASTHA2Context;
        }

        public IUserRepository Users
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_AASTHA2Context);
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
                    _patient = new PatientRepository(_AASTHA2Context);
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
                    _opd = new OpdRepository(_AASTHA2Context);
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
                    _ipd = new IpdRepository(_AASTHA2Context);
                }
                return _ipd;
            }
        }
        public IIpdLookupRepository IpdLookups
        {
            get
            {
                if (_ipdLookup == null)
                {
                    _ipdLookup = new IpdLookupRepository(_AASTHA2Context);
                }
                return _ipdLookup;
            }
        }
        public ILookupRepository Lookups
        {
            get
            {
                if (_lookup == null)
                {
                    _lookup = new LookupRepository(_AASTHA2Context);
                }
                return _lookup;
            }
        }
        public IDeliveryRepository Deliveries
        {
            get
            {
                if (_delivery == null)
                {
                    _delivery = new DeliveryRepository(_AASTHA2Context);
                }
                return _delivery;
            }
        }
        public IOperationRepository Operations
        {
            get
            {
                if (_operation == null)
                {
                    _operation = new OperationRepository(_AASTHA2Context);
                }
                return _operation;
            }
        }
        public IChargeRepository Charges
        {
            get
            {
                if (_charge == null)
                {
                    _charge = new ChargeRepository(_AASTHA2Context);
                }
                return _charge;
            }
        }
        public IAppointmentRepository Appointments
        {
            get
            {
                if (_appointment == null)
                {
                    _appointment = new AppointmentRepository(_AASTHA2Context);
                }
                return _appointment;
            }
        }

        public void SaveChanges()
        {
            _AASTHA2Context.SaveChanges();
        }
    }
}
