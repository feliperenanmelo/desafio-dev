using Bycoders.DesafioDev.API.Configurations;
using Bycoders.DesafioDev.API.Domain.Entities;
using Bycoders.DesafioDev.API.Domain.Interfaces;
using Bycoders.DesafioDev.API.Domain.Validators;
using Bycoders.DesafioDev.API.Extensions;
using Bycoders.DesafioDev.API.ViewModel;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Bycoders.DesafioDev.API.Services
{
    public class TransacaoFinanceiraService : ITransacaoFinanceiraService
    {
        private readonly ILogger<TransacaoFinanceiraService> _logger;

        private readonly CnabCampo _tipo;
        private readonly CnabCampo _data;
        private readonly CnabCampo _valor;
        private readonly CnabCampo _cpf;
        private readonly CnabCampo _cartao;
        private readonly CnabCampo _hora;
        private readonly CnabCampo _donoLoja;
        private readonly CnabCampo _nomeLoja;

        private readonly CnabConfiguracoes _configuracoes;        
        private readonly ITransacaoFinanceiraRepository _transacaoFinanceiraRepository;
        private readonly ITipoTransacaoRepository _tipoTransacaoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransacaoFinanceiraService(
            ILogger<TransacaoFinanceiraService> logger,
            CnabConfiguracoes configuracoes,            
            ITransacaoFinanceiraRepository transacaoFinanceiraRepository,
            ITipoTransacaoRepository tipoTransacaoRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _configuracoes = configuracoes;

            _tipo = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Tipo);
            _data = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Data);
            _valor = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Valor);
            _cpf = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.CPF);
            _cartao = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Cartao);
            _hora = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Hora);
            _donoLoja = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.DonoLoja);
            _nomeLoja = _configuracoes.CnabCampos.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.NomeLoja);
                        
            _transacaoFinanceiraRepository = transacaoFinanceiraRepository;
            _tipoTransacaoRepository = tipoTransacaoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<TransacoesFinanceirasResponse> CriarPorArquivo(IFormFile aruqivo)
        {
            List<TransacaoFinanceira> transacoesSucesso;
            List<TransacaoFinanceiraErroResponse> transacoesErro;

            using (var sr = new StreamReader(aruqivo.OpenReadStream()))
            {
                (transacoesSucesso, transacoesErro) = GetTransacoesFinanceirasBy(sr);
            }

            if (transacoesSucesso.Any())
            {
                await _transacaoFinanceiraRepository.AdicionarPorLista(transacoesSucesso);
                await _unitOfWork.CommitAsync();                
            }            

            var tiposTransacao = await _tipoTransacaoRepository.ObterTodos();
            transacoesSucesso.ForEach(transacaoSucesso =>
            {
                transacaoSucesso.IncluirTipoTransacao(
                    tiposTransacao.FirstOrDefault(tipo 
                        => tipo.Id.Equals(transacaoSucesso.TipoTransacaoId)));
            });

            var transacoesResponse = new TransacoesFinanceirasResponse
            {
                TransacaoFinanceirasSucesso = transacoesSucesso.ParaTransacaoFinanceiraResponse(),
                TransacaoFinanceirasComErro = transacoesErro
            };

            return transacoesResponse;
        }
        private (List<TransacaoFinanceira>, List<TransacaoFinanceiraErroResponse>) GetTransacoesFinanceirasBy(StreamReader sr)
        {
            var transacoesSucesso = new List<TransacaoFinanceira>();
            var transacoesErro = new List<TransacaoFinanceiraErroResponse>();

            int posicao = 0;
            while (!sr.EndOfStream)
            {
                posicao++;

                var line = sr.ReadLine();
                if (line == null) continue;

                _ = int.TryParse(line.ObterValor(_tipo), out var tipo);
                _ = DateTime.TryParse(line.ObterValor(_data).Insert(4, "-").Insert(7, "-"), new CultureInfo("pt-BR"), DateTimeStyles.None, out var data);
                _ = decimal.TryParse(line.ObterValor(_valor), out var valor);
                var cpf = line.ObterValor(_cpf);
                var cartao = line.ObterValor(_cartao);
                var hora = line.ObterValor(_hora);
                var donoLoja = line.ObterValor(_donoLoja);
                var nomeLoja = line.ObterValor(_nomeLoja);

                var transacaoFinanceira = TransacaoFinanceira.Create(tipo, data, cpf, cartao, hora, donoLoja, nomeLoja, valor);

                var validator = new TransacaoFinanceiraValidator().Validate(transacaoFinanceira);

                if (validator.IsValid)
                    transacoesSucesso.Add(transacaoFinanceira);
                else
                    transacoesErro.Add(TransacaoFinanceiraErroResponse
                        .Create(posicao, transacaoFinanceira.ParaTransacaoFinanceiraResponse(), validator.Errors.Select(err => err.ErrorMessage)));
            }

            return (transacoesSucesso, transacoesErro);
        }
    }
}
