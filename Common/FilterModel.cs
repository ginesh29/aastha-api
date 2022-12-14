using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AASTHA2.Common
{
    public class FilterModel
    {
        public string filter { get; set; }
        public string sort { get; set; }
        public int skip { get; set; }
        public int take { get; set; }
        public string includeProperties { get; set; }
        public string fields { get; set; }
        public bool isPage { get; set; }
    }
}
