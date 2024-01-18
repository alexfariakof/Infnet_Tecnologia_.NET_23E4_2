namespace Integration_PIX_Adapter.Adapters.Models;

[Serializable]
public class PixCharge
{
    public string CorrelationID { get; set; }
    public string Value { get; set; }
    public string BrCode { get; set; }
    public Charge Charge { get; set; }   

}
