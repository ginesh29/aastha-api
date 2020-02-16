using AASTHA2.Common;
using System;
using System.Collections.Generic;

namespace AASTHA2.DTO
{
    public class LookupDTO
    {
        public long id { get; set; }
        public string name { get; set; }
        public LookupType type { get; set; }
        public string typeName => this.type.ToString();
        public long? parentId { get; set; }
        public bool? isDeleted { get; set; }

        public LookupDTO parent { get; set; }
        public ICollection<LookupDTO> children { get; set; }
    }
}
