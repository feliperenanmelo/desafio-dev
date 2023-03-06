using System.Collections.Generic;

namespace Bycoders.DesafioDev.API.ViewModel
{
    public class Paginacao<T> where T : class, IResponse
    {
        public IEnumerable<T> Data { get; set; }
        public int TotalResults { get; set; }
        public int PageIndex { get; set; }
        public int PageSize { get; set; }
    }

    public interface IResponse { }
}
