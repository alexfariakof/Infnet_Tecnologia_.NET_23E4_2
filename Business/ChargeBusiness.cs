using Business.Interfaces;
using Domain.Transactions.ValuesObject;
using Integration_PIX_Adapter;
using Integration_PIX_Adapter.Adapters.OpenPix;

namespace Business;
public class ChargeBusiness : IChargeBusiness
{
    public void CreateTransaction(TransactionType transactionType)
    {
        IChargePix chargePix = new OpenPix();

        chargePix.CreateCharge(charge);
    }

}