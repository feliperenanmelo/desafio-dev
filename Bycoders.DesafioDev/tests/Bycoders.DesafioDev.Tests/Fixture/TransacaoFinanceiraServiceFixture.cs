using Bycoders.DesafioDev.API.Configurations;
using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Bycoders.DesafioDev.API.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using Xunit;

namespace Bycoders.DesafioDev.Tests.Fixture
{
    [CollectionDefinition(nameof(TransacaoFinanceiraServiceCollection))]
    public class TransacaoFinanceiraServiceCollection : ICollectionFixture<TransacaoFinanceiraServiceFixture>
    { }

    public class TransacaoFinanceiraServiceFixture
    {
        private Mock<ILogger<TransacaoFinanceiraService>> _transacaoFinanceiraServiceLogger;
        
        public Mock<CnabConfiguracoes> CnabConfiguracoesMock;
        public Mock<ITransacaoFinanceiraRepository> TransacaoFinanceiraRepositoryMock;
        public Mock<ITipoTransacaoRepository> TipoTransacaoRepositoryMock;
        public Mock<IUnitOfWork> UnitOfWorkMock;

        public TransacaoFinanceiraService ConfigurarSucesso()
        {
            CarregarMock();

            CnabConfiguracoesMock
                .SetupGet(config => config.CnabCampos)
                .Returns(ObterCnabCampos().Select(o => o.Object).ToList());

            TipoTransacaoRepositoryMock
                .Setup(tipoRepo => tipoRepo.ObterTodos())
                .ReturnsAsync(ObterTiposTransacao());

            return new TransacaoFinanceiraService(
                _transacaoFinanceiraServiceLogger.Object,
                CnabConfiguracoesMock.Object,
                TransacaoFinanceiraRepositoryMock.Object,
                TipoTransacaoRepositoryMock.Object,
                UnitOfWorkMock.Object);
        }

        private void CarregarMock()
        {
            _transacaoFinanceiraServiceLogger = new Mock<ILogger<TransacaoFinanceiraService>>();
            CnabConfiguracoesMock = new Mock<CnabConfiguracoes>();
            CnabConfiguracoesMock = new Mock<CnabConfiguracoes>();
            TransacaoFinanceiraRepositoryMock = new Mock<ITransacaoFinanceiraRepository>();
            TipoTransacaoRepositoryMock = new Mock<ITipoTransacaoRepository>();
            UnitOfWorkMock = new Mock<IUnitOfWork>();
        }

        public IFormFile CriarArquivo(string nomeArquivo)
        {
            var caminhoArquivo = Path.Combine(AppContext.BaseDirectory, $"File\\{nomeArquivo}");
            var stream = new MemoryStream(File.ReadAllBytes(caminhoArquivo).ToArray());
            var arquivo = new FormFile(stream, 0, stream.Length, "CNAB", caminhoArquivo.Split(@"\").Last());

            return arquivo;
        }       

        public Collection<Mock<CnabCampo>> ObterCnabCampos()
        {
            var _configuracao = new ConfigurationBuilder()
                .AddJsonFile("appsettings.Development.json")
                .Build();

            var cnabConfiguracoes = _configuracao.Get<CnabConfiguracoes>();
            var camposMock = new Collection<Mock<CnabCampo>>();
            foreach (var campo in cnabConfiguracoes.CnabCampos)
            {
                var campoMock = new Mock<CnabCampo>();
                campoMock.Object.Tamanho = campo.Tamanho;
                campoMock.Object.Descricao = campo.Descricao;
                campoMock.Object.TipoCampo = campo.TipoCampo;
                campoMock.Object.Inicio = campo.Inicio;
                camposMock.Add(campoMock);
            }

            return camposMock;
        }

        public List<TipoTransacao> ObterTiposTransacao()
        {
            var tiposTransacao = new List<TipoTransacao>();
            tiposTransacao.Add(TipoTransacao.Create("Débito", "Entrada", '+'));
            tiposTransacao.Add(TipoTransacao.Create("Boleto", "Saída", '-'));
            tiposTransacao.Add(TipoTransacao.Create("Financimento", "Saída", '-'));
            tiposTransacao.Add(TipoTransacao.Create("Crédito", "Entrada", '+'));
            tiposTransacao.Add(TipoTransacao.Create("Recebimento Empréstimo", "Entrada", '+'));
            tiposTransacao.Add(TipoTransacao.Create("Vendas", "Entrada", '+'));
            tiposTransacao.Add(TipoTransacao.Create("Recebimento TED", "Entrada", '+'));
            tiposTransacao.Add(TipoTransacao.Create("Recebimento DOC", "Entrada", '+'));
            tiposTransacao.Add(TipoTransacao.Create("Aluguel", "Saída", '-'));

            return tiposTransacao;
        }

    }
}
