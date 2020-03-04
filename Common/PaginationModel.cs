using System.Linq;

namespace AASTHA2.Common
{
    public class PaginationModel
    {
        public int StartPage { get; set; }
        public int EndPage { get; set; }
        public int TotalCount { get; set; }
        public IQueryable Data { get; set; }
    }
}
