using System;

namespace AASTHA2.Services.DTO
{
    public class UserDTO
    {
        public long Id { get; set; }
        public string Firstname { get; set; }
        public string Middlename { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Lastname { get; set; }
        public string Fullname => $"{Firstname} {Middlename} {Lastname}";
        public string Mobile { get; set; }
        public int Age { get; set; }
        public bool? IsDeleted { get; set; }
        public DateTime ModifiedDate { get; set; }
        public bool IsSuperAdmin { get; set; }
        public int CalculatedAge
        {
            get { return Age + (ModifiedDate != DateTime.MinValue ? DateTime.UtcNow.Year - ModifiedDate.Year : 0); }
            set { Age = value; }
        }
    }
}
