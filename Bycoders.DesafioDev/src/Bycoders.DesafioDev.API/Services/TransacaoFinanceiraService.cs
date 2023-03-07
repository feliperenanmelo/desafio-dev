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

        private readonly CnabField _tipo;
        private readonly CnabField _data;
        private readonly CnabField _valor;
        private readonly CnabField _cpf;
        private readonly CnabField _cartao;
        private readonly CnabField _hora;
        private readonly CnabField _donoLoja;
        private readonly CnabField _nomeLoja;

        private readonly CnabConfigurations _configurations;        
        private readonly ITransacaoFinanceiraRepository _transacaoFinanceiraRepository;
        private readonly ITipoTransacaoRepository _tipoTransacaoRepository;
        private readonly IUnitOfWork _unitOfWork;

        public TransacaoFinanceiraService(
            ILogger<TransacaoFinanceiraService> logger,
            CnabConfigurations configurations,            
            ITransacaoFinanceiraRepository transacaoFinanceiraRepository,
            ITipoTransacaoRepository tipoTransacaoRepository,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _configurations = configurations;

            _tipo = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Tipo);
            _data = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Data);
            _valor = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Valor);
            _cpf = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.CPF);
            _cartao = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Cartao);
            _hora = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.Hora);
            _donoLoja = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.DonoLoja);
            _nomeLoja = _configurations.CnabFields.FirstOrDefault(cnab => cnab.Descricao == DescricaoCampo.NomeLoja);
                        
            _transacaoFinanceiraRepository = transacaoFinanceiraRepository;
            _tipoTransacaoRepository = tipoTransacaoRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<Paginacao<TipoTransacaoResponse>> GetAllTipoTransacao(int pageSize, int page)
        {
           var tiposTransacao = await _tipoTransacaoRepository.GetAll(pageSize, page);

            var totalTiposTransacao = await _tipoTransacaoRepository.GetCount();

            return new Paginacao<TipoTransacaoResponse>
            {
                Data = tiposTransacao.ToTipoTransacaoResponse(),
                PageSize = pageSize,
                PageIndex = page,
                TotalResults = totalTiposTransacao
            };
        }

        public async Task<Paginacao<TransacaoFinanceiraResponse>> GetAll(int pageSize, int page)
        {
            var transacoesFinanceiras = await _transacaoFinanceiraRepository.GetAll(pageSize, page);

            var totalTransaceosFinanceiras = await _transacaoFinanceiraRepository.GetCount();

            return new Paginacao<TransacaoFinanceiraResponse>
            {
                Data = transacoesFinanceiras.ToTransacaoFinanceiraResponse(),
                PageSize = pageSize,
                PageIndex = page,
                TotalResults = totalTransaceosFinanceiras
            };
        }

        public async Task<TransacoesFinanceirasResponse> CreateByPathFile(IFormFile file)
        {
            List<TransacaoFinanceira> transacoesSucesso;
            List<TransacaoFinanceiraErroResponse> transacoesErro;

            using (var sr = new StreamReader(file.OpenReadStream()))
            {
                (transacoesSucesso, transacoesErro) = GetTransacoesFinanceirasBy(sr);
            }

            if (transacoesSucesso.Any())
            {
                await _transacaoFinanceiraRepository.AddRange(transacoesSucesso);
                await _unitOfWork.CommitAsync();                
            }            

            var tiposTransacao = await _tipoTransacaoRepository.GetAllTiposTransacao();
            transacoesSucesso.ForEach(transacaoSucesso =>
            {
                transacaoSucesso.IncluirTipoTransacao(tiposTransacao.FirstOrDefault(tipo => tipo.Id.Equals(transacaoSucesso.TipoTransacaoId)));
            });

            var transacoesResponse = new TransacoesFinanceirasResponse
            {
                TransacaoFinanceirasSucesso = transacoesSucesso.ToTransacaoFinanceiraResponse(),
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

                _ = int.TryParse(line.GetValue(_tipo), out var tipo);
                _ = DateTime.TryParse(line.GetValue(_data).Insert(4, "-").Insert(7, "-"), new CultureInfo("pt-BR"), DateTimeStyles.None, out var data);
                _ = decimal.TryParse(line.GetValue(_valor), out var valor);
                var cpf = line.GetValue(_cpf);
                var cartao = line.GetValue(_cartao);
                var hora = line.GetValue(_hora);
                var donoLoja = line.GetValue(_donoLoja);
                var nomeLoja = line.GetValue(_nomeLoja);

                var transacaoFinanceira = TransacaoFinanceira.Create(tipo, data, cpf, cartao, hora, donoLoja, nomeLoja, valor);

                var validator = new TransacaoFinanceiraValidator().Validate(transacaoFinanceira);

                if (validator.IsValid)
                    transacoesSucesso.Add(transacaoFinanceira);
                else
                    transacoesErro.Add(TransacaoFinanceiraErroResponse.Create(posicao, transacaoFinanceira.ToTransacaoFinanceiraResponse(), validator.Errors.Select(err => err.ErrorMessage)));
            }

            return (transacoesSucesso, transacoesErro);
        }
    }
}
