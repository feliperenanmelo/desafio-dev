using Bycoders.DesafioDev.App.Extensions;
using Bycoders.DesafioDev.App.Models;
using Bycoders.DesafioDev.App.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
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
            var operacoesImportadasViewModel = new ImportacaoViewModel();

            var megaByte = 1000000;

            if (importacaoViewModel.File == null || importacaoViewModel.File?.Length > megaByte)
            {
                operacoesImportadasViewModel.Erros.Add("Arquivo inexistente ou maior que 1Mb");
                return View("Index",operacoesImportadasViewModel);
            }

            var indiceExtensao = importacaoViewModel.File.FileName.LastIndexOf('.');

            var extensao = importacaoViewModel.File.FileName.Substring(indiceExtensao, importacaoViewModel.File.FileName.Length - indiceExtensao);

            if (!extensao.Contains(".txt"))
            {
                operacoesImportadasViewModel.Erros.Add("Arquivo com extensão inválida");
                return View("Index",operacoesImportadasViewModel);
            }

            var transacoes = await _repository.PostAsync(importacaoViewModel.File);

            var operacoesSucessoPorLoja = transacoes.Dados.TransacaoFinanceirasSucesso.ParaListaOperacoesSucessoPorLoja();
            var operacoesErroPorLoja = transacoes.Dados.TransacaoFinanceirasComErro.ParaListaOperacoesErroPorLoja();

            return View(new OperacoesImportadasViewModel
            {
                OperacoesSucesso = operacoesSucessoPorLoja,
                OperacoesNaoProcessadas = operacoesErroPorLoja
            });
        }
    }
}
