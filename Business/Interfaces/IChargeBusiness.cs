using Domain.Transactions.ValuesObject;

namespace Business.Interfaces; 
public interface IChargeBusiness<T> where T : class
{
    public void CreateTransaction(T customer, TransactionType transactionType);
}