using Domain.Account.ValueObject;

namespace Domain.Account.Agreggates.Interfaces
{
    public interface ICustomer
    {
        Guid Id { get; set; }
        string Name { get; set; }
        Login Login { get; set; }
        string CPF { get; set; }
        DateTime Birth { get; set; }
    }
}
