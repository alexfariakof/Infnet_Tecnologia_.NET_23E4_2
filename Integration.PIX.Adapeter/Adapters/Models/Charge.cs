namespace Integration_PIX_Adapter.Adapters.Models;

[Serializable]
public sealed class Charge
{
    public CustomerPix Customer { get; set; }
    public long  Value { get; set; }
    public string  Identifier { get; set; }
    public string  CorrelationID { get; set; }
    public string  PaymentLinkID { get; set; }
    public string  TransactionID { get; set; }
    public string  Status { get; set; }
    public AdditionalInfo[] AdditionalInfo { get; set; }
    public int Discount { get; set; }
    public int  ValueWithDiscount { get; set; }
    public DateTime  ExpiresDate { get; set; }
    public string  Type { get; set; }
    public DateTime  CreatedAt { get; set; }    
    public DateTime UpdatedAt { get; set; }
    public string BrCode { get; set; }    
    public int  ExpiresIn { get; set; }
    public string  PixKey { get; set; }    
    public string  PaymentLinkUrl { get; set; }
    public string  QrCodeImage { get; set; }
    public string  GlobalID { get; set; }
}
