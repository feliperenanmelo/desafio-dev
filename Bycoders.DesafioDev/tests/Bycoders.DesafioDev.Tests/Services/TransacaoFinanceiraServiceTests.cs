using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.Tests.Fixture;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Bycoders.DesafioDev.Tests.Services
{
    [Collection(nameof(TransacaoFinanceiraServiceCollection))]
    public class TransacaoFinanceiraServiceTests
    {
        private readonly TransacaoFinanceiraServiceFixture _transacaoFinanceiraServiceFixture;

        public TransacaoFinanceiraServiceTests(TransacaoFinanceiraServiceFixture transacaoFinanceiraServiceFixture)
        {
            _transacaoFinanceiraServiceFixture = transacaoFinanceiraServiceFixture;
        }

        [Fact(DisplayName = "TransacaoFinanceiraService - Processar arquivo sucesso")]
        [Trait("Services", "TransacaoFinanceiraService")]
        public async Task DadoQueTransacaoFinanceiraService_CriarPorArquivoComArquivoValido_CriarArquivoSucesso()
        {
            // Arrange
            var arquivo = _transacaoFinanceiraServiceFixture.CriarArquivo("CNAB.txt");

            var transacaoFinanceiraService = _transacaoFinanceiraServiceFixture.ConfigurarSucesso();

            // Act
            var resultado = await transacaoFinanceiraService.CriarPorArquivo(arquivo);

            // Assert
            Assert.NotNull(resultado);

            _transacaoFinanceiraServiceFixture.CnabConfiguracoesMock
                .VerifyGet(configuracao => configuracao.CnabCampos, Times.Exactly(8));

            _transacaoFinanceiraServiceFixture.TransacaoFinanceiraRepositoryMock
                .Verify(transacaoRepo => transacaoRepo.AdicionarPorLista(It.IsAny<List<TransacaoFinanceira>>()), Times.Once);

            _transacaoFinanceiraServiceFixture.UnitOfWorkMock
                .Verify(wow => wow.CommitAsync(), Times.Once);
        }

        [Fact(DisplayName = "TransacaoFinanceiraService - Processar arquivo sucesso mas não encontrou nenhuma transação")]
        [Trait("Services", "TransacaoFinanceiraService")]
        public async Task DadoQueTransacaoFinanceiraService_CriarPorArquivoComArquivoTxtEmBranco_CriarArquivoSucessoMasNaoInsereDadosNoDb()
        {
            // Arrange
            var arquivo = _transacaoFinanceiraServiceFixture.CriarArquivo("CNAB-Vazio.txt");

            var transacaoFinanceiraService = _transacaoFinanceiraServiceFixture.ConfigurarSucesso();

            // Act
            var resultado = await transacaoFinanceiraService.CriarPorArquivo(arquivo);

            // Assert
            Assert.NotNull(resultado);

            _transacaoFinanceiraServiceFixture.CnabConfiguracoesMock
                .VerifyGet(configuracao => configuracao.CnabCampos, Times.Exactly(8));

            _transacaoFinanceiraServiceFixture.TransacaoFinanceiraRepositoryMock
                .Verify(transacaoRepo => transacaoRepo.AdicionarPorLista(It.IsAny<List<TransacaoFinanceira>>()), Times.Never);

            _transacaoFinanceiraServiceFixture.UnitOfWorkMock
                .Verify(wow => wow.CommitAsync(), Times.Never);
        }       
    }
}
