using System;
using System.Collections.Generic;

namespace Migration.Models
{
    public partial class TblUser
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Type { get; set; }
        public string Layout { get; set; }
        public string BiometricId { get; set; }
    }
}
