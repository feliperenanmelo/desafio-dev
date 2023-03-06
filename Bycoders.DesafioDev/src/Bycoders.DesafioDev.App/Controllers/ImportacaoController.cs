using Bycoders.DesafioDev.App.Models;
using Bycoders.DesafioDev.App.Services;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.App.Controllers
{
    public class ImportacaoController : Controller
    {
        private readonly IImportaHttpRepository _repository;

        public ImportacaoController(IImportaHttpRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        [Route("")]
        [Route("importacao-transacoes-financeiras")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]        
        [Route("importar-transacoes-financeiras")]
        public async Task<IActionResult> Create(ImportacaoViewModel importacaoViewModel)
        {
            var transacoes = await _repository.PostAsync(importacaoViewModel.File);

            var operacoesPorLoja = transacoes.Data.TransacaoFinanceirasSucesso
                .GroupBy(tran => tran.NomeLoja)
                .Select(tran => new OperacaoPorLoja
                {
                    NomeLoja = tran.Key,
                    Transacoes = tran.Select(t => t).ToList()
                }).ToList(); 

            return View(new OperacoesImportadasViewModel() { Operacoes = operacoesPorLoja });
        }
    }
}
