﻿using ProvaPub.Interfaces;

namespace ProvaPub.Services
{
    public class PixPaymentStrategy : IPaymentStrategy
    {
        public async Task Pay(decimal paymentValue, int customerId)
        {
            // Lógica de pagamento via Pix
            /*
             // Validar o valor do pagamento e o ID do cliente
             if (paymentValue <= 0)
             {
                 throw new ArgumentException("O valor do pagamento deve ser maior que zero.");
             }

             if (customerId <= 0)
             {
                 throw new ArgumentException("O ID do cliente deve ser maior que zero.");
             }

             // Realizar o pagamento via Pix
             bool pagamentoAprovado = ProcessarPagamentoViaPix(paymentValue);

             // Verificar se o pagamento foi aprovado
             if (!pagamentoAprovado)
             {
                 throw new Exception("O pagamento via Pix não foi concluído. Por favor, tente novamente.");
             }

             // Registrar o pagamento no sistema
             RegistrarPagamento(paymentValue, customerId);

             // Enviar um e-mail de confirmação para o cliente
             EnviarEmailConfirmacao(customerId, paymentValue);
             */
        }
    }
}
