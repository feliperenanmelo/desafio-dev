using System.Collections.Generic;

namespace Bycoders.DesafioDev.API.ViewModel
{
    public class Paginacao<T> where T : class, IResponse
    {
        public IEnumerable<T> Dados { get; set; }
        public int TotalPagina { get; set; }
        public int IndicePagina { get; set; }
        public int TamanhoPagina { get; set; }
    }

    public interface IResponse { }
}
