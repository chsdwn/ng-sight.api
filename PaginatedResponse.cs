using System.Collections.Generic;
using System.Linq;

namespace Advantage.API
{
    public class PaginatedResponse<T>
    {
        public int Total { get; set; }
        public IEnumerable<T> Data { get; set; }

        public PaginatedResponse(IEnumerable<T> data, int pageIndex, int length)
        {
            Data = data.Skip((pageIndex - 1) * length).Take(length).ToList();
            Total = data.Count();
        }

        
    }
}