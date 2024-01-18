using Domain.Account.Agreggates;
using Domain.Core.Aggreggates;
using Domain.Core.ValueObject;
using Domain.Notifications;
using Domain.Transactions.ValuesObject;
using Integration_PIX_Adapter;
using Integration_PIX_Adapter.Adapters.Models;
using Integration_PIX_Adapter.Adapters.OpenPix;

namespace Domain.Transactions.Agreggates;
public class PIX: BaseModel
{
    private const int INTERVAL_TRANSACTON_PIX = -4;
    public DateTime Date { get; set; }
    public Status Status { get; set; }
    public Guid CorrelationId { get; set; }
    public Customer Customer { get; set; }
    public Monetary Monetary { get; set; }
    public string Description { get; set; }
    public QRCode QrCode { get; set; } = new QRCode();
    public Notification Notification { get; set; }
    public List<Transaction> Transactions { get; set; } = new List<Transaction>();
    public PIX(Customer custumer)
    {
        Id = Guid.NewGuid();
        CorrelationId = custumer.Id;
        Customer = custumer;
    }

    public void CreateTransaction(Monetary value, string description="")
    {
        IChargePix chargePix = new OpenPix();
        Transaction transaction = new Transaction
        {
            CorrelationId = this.CorrelationId,
            Customer = this.Customer,
            Value = value,
            Description = description,
            DtTransaction = DateTime.Now,
        };

        // Verificãção de Existência de uma Transação PIX já Criada com o mesmo Valor dentro do Intervalo Minimo 
        transaction = this.ValidateTransaction(transaction) ?? transaction;

        var pix = chargePix.CreateCharge(new PixCharge
        {
            CorrelationID = transaction.CorrelationId.ToString(),
            Value = value.GetCents().ToString(),
        });

        this.QrCode.Url = pix.Charge.QrCodeImage;
        this.QrCode.BrCode = pix.Charge.BrCode;

        if (transaction.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
        {
            transaction.Id = Guid.NewGuid();
            this.Transactions.Add(transaction);
        }
    }

    private Transaction ValidateTransaction(Transaction transaction)
    {
        var lastTransactions = this.Transactions.Where(t => t.DtTransaction > DateTime.Now.AddMinutes(INTERVAL_TRANSACTON_PIX) && t.Value.Equals(transaction.Value));
        if (lastTransactions?.Count() >= 1)
            return lastTransactions.Last();

        return null;        
    }
}
