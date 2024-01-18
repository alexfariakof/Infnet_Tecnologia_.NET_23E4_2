using Integration_PIX_Adapter.Adapters.Models;

namespace Integration_PIX_Adapter;
public interface IChargePix
{
    public PixCharge CreateCharge(PixCharge Pixcharge);    
    public Boolean IsChargeApporve(Guid correlationID);
}