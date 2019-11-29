using System;

namespace AASTHA2.DTO
{
    public class PatientDTO
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        private int _age;
        private DateTime _createdDate;
        public int Age
        {
            get { return _age; }
            set { _age = value; }
        }
    }
}
