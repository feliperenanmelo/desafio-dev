using Bycoders.DesafioDev.API.Domain.Entities;
using FluentValidation;
using System;

namespace Bycoders.DesafioDev.API.Domain.Validators
{
    public class TransacaoFinanceiraValidator : AbstractValidator<TransacaoFinanceira>
    {
        public TransacaoFinanceiraValidator()
        {
            RuleFor(tran => tran.TipoTransacaoId)
                .NotEmpty()
                .WithMessage("Tipo de transação inválida");

            RuleFor(tran => tran.Data)
                .NotEmpty()
                .GreaterThan(DateTime.MinValue);

            RuleFor(tran => tran.Valor)
                .NotNull()
                .GreaterThan(decimal.MinValue)
                .WithMessage("Valor inválido");

            // CPF no arquivo contém apenas 10 caracteres
            RuleFor(tran => tran.CPF)
                .NotEmpty()
                .Length(11)
                .WithMessage("CPF deve conter 10 caracteres");

            RuleFor(tran => tran.Cartao)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(30)
                .WithMessage("Cartão inválido");

            RuleFor(tran => tran.Hora)
                .NotEmpty()
                .WithMessage("Hora inválido");

            RuleFor(tran => tran.Dono)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(200)
                .WithMessage("Nome do dono da loja deve conter entre 1 e 200 caractres");

            RuleFor(tran => tran.NomeLoja)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(200)
                .WithMessage("Nome da loja deve conter entre 1 e 200 caractres");
        }
    }
}
