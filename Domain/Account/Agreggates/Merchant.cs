using Domain.Core.Interfaces;
using Domain.Streaming.Agreggates;
using Domain.Transactions.Agreggates;

namespace Domain.Account.Agreggates
{
    public class Merchant : Account, IMerchant
    {
        public string CNPJ { get; set; }

        public void CreateAccount(string nome, string email, string senha, string cnpj, Flat flat, Card card)
        {
            this.Name = nome;
            this.Email = email;
            this.CNPJ = cnpj;

            //Criptografar a senha
            this.Password = this.CryptoPasswrod(senha);

            //Assinar um plano
            this.AddFlat(flat, card);

            //Adicionar cartão na conta do usuário
            this.AddCard(card);
        }
    }
}
