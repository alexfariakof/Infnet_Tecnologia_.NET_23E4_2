using __mock__;
using Domain.Transactions.Agreggates;

namespace Domain.Transactions;
public class PIXTest
{
    [Fact]
    public void Should_Create_Transaction_With_Success()
    {
        var customer = MockCustomer.GetFaker();
        PIX pix = new PIX(customer);

        pix.CreateTransaction(125.24m, "Teste Unitário Create PIX");

        Assert.NotNull(pix.QrCode.Url);
        Assert.NotNull(pix.QrCode.BrCode);
    }
}