using Bycoders.DesafioDev.API.Domain.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Controllers.V1
{
    [ApiController]
    [Route("api/desafio-dev")]
    public class DesafioDevController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ITransacaoFinanceiraService _transacaoFinanceiraService;

        public DesafioDevController(
            IFileService fileService,
            ITransacaoFinanceiraService transacaoFinanceiraService)
        {
            _fileService = fileService;
            _transacaoFinanceiraService = transacaoFinanceiraService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll([FromQuery] int pageSize = 5, [FromQuery] int page = 1)
        {
            var response = await _transacaoFinanceiraService.GetAll(pageSize, page);

            if (!response.Data.Any()) return BadRequest(new { sucess = false, data = response });

            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> CreateByFile(IFormFile file)
        {
            var sourcePath = await _fileService.Create(file);

            var response = await _transacaoFinanceiraService.CreateByPathFile(sourcePath, file.FileName);

            if (!response.TransacaoFinanceirasSucesso.Any()) return BadRequest(new { sucess = false, data = response });

            return Ok(new { success = true, data = response });
        }

        [HttpGet("tipos-transacao")]
        public async Task<IActionResult> GetAllTipoTransacao([FromQuery] int pageSize = 5, [FromQuery] int page = 1)
        {
            var response = await _transacaoFinanceiraService.GetAllTipoTransacao(pageSize, page);

            return Ok(response);
        }
    }
}
