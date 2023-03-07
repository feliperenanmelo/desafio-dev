using Bycoders.DesafioDev.App.Models;
using Bycoders.DesafioDev.App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.App.Controllers
{
    public class ImportacaoController : Controller
    {
        private readonly IImportaHttpRepository _repository;
        private readonly ILogger<ImportacaoController> _logger;

        public ImportacaoController(
            IImportaHttpRepository repository,
            ILogger<ImportacaoController> logger)
        {
            _repository = repository;
            _logger = logger;
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
            _logger.LogInformation("Processando arquivo");

            var transacoes = await _repository.PostAsync(importacaoViewModel.File);

            var operacoesPorLoja = transacoes.Data.TransacaoFinanceirasSucesso
                .GroupBy(tran => tran.NomeLoja)
                .Select(tran => new OperacaoPorLoja
                {
                    NomeLoja = tran.Key,
                    Transacoes = tran.Select(t => t).ToList()
                }).ToList();

            _logger.LogInformation("Arquivos processados");

            return View(new OperacoesImportadasViewModel() { Operacoes = operacoesPorLoja });
        }
    }
}
