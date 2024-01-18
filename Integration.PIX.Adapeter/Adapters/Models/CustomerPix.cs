namespace Integration_PIX_Adapter.Adapters.Models;

[Serializable]
public sealed class CustomerPix
{
    public string Name { get; set; }
    public string TaxID { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }
    public string CorrelationID { get; set; }
    public Address Address { get; set; }
}