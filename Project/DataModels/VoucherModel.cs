public class VoucherModel
{
    public Int64 Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public decimal Amount { get; set; }
    public string Type { get; set; }
    public string ExpirationDate { get; set; }
    public int? UserId { get; set; }

    public VoucherModel(Int64 id, string code, string description, decimal amount, string type, string expirationDate, int? userId = null)
    {
        Id = id;
        Code = code;
        Description = description;
        Amount = amount;
        Type = type;
        ExpirationDate = expirationDate;
        UserId = userId;
    }
}