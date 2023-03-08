using Bycoders.DesafioDev.API.Domain.Interfaces;
using Bycoders.DesafioDev.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Controllers.V1
{
    [ApiController]
    [Route("api/desafio-dev")]
    public class DesafioDevController : ControllerBase
    {
        private List<string> _erros = new List<string>();
        private readonly ILogger<DesafioDevController> _logger;
        private readonly ITransacaoFinanceiraService _transacaoFinanceiraService;

        public DesafioDevController(
            ILogger<DesafioDevController> logger,
            ITransacaoFinanceiraService transacaoFinanceiraService)
        {
            _logger = logger;
            _transacaoFinanceiraService = transacaoFinanceiraService;
        }       

        [HttpPost]
        public async Task<IActionResult> CriarTransacaoFinanceiraPorArquivo(IFormFile file)
        {
            var megaByte = 1000000;

            if (file == null || file?.Length > megaByte)
            {
                _erros.Add("Arquivo inexistente ou maior que 1Mb");
                return ResponseRequest();
            }
            
            var indiceExtensao = file.FileName.LastIndexOf('.');

            var extensao = file.FileName.Substring(indiceExtensao, file.FileName.Length - indiceExtensao);

            if (!extensao.Contains(".txt"))
            {
                _erros.Add("Arquivo com extensão inválida");
                return ResponseRequest();
            }

            var resposta = await _transacaoFinanceiraService.CriarPorArquivo(file);

            return ResponseRequest(resposta);
        }        

        private IActionResult ResponseRequest(object dados = null)
        {
            var resposta = new Response
            {
                Sucesso = _erros.Count == 0,
                Dados = dados,
                Erros = _erros
            };

            if (resposta.Sucesso)
                return Ok(resposta);
            else
                return BadRequest(resposta);
        }
    }
}
