﻿using System;

namespace AASTHA2.DTO
{
    public class PatientDTO
    {
        public long id { get; set; }
        public string firstname { get; set; }
        public string middlename { get; set; }
        public string fathername { get; set; }
        public string lastname { get; set; }
        public string fullname => $"{this.firstname } {this.middlename} {(!string.IsNullOrEmpty(this.fathername) ? $"({this.fathername})" : string.Empty) } {this.lastname}";
        public long addressId { get; set; }
        public string mobile { get; set; }
        public int age { get; set; }
        public bool? isDeleted { get; set; }
        public DateTime createdDate { get; set; }
        public LookupDTO Address { get; set; }
        public int updatedAge
        {
            get { return age + (createdDate != DateTime.MinValue ? (DateTime.UtcNow.Year - createdDate.Year) : 0); }
            set { age = value; }
        }
        //public ICollection<OpdDTO> opds { get; set; }
        //public ICollection<IpdDTO> ipds { get; set; }
    }
}
