public class VoucherModel
{
    public Int64 Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; }
    public int? UserId { get; set; }

    public VoucherModel() { }

    public VoucherModel(Int64 id, string code, string description, int amount, string type, int? userId = null)
    {
        Id = id;
        Code = code;
        Description = description;
        Amount = amount;
        Type = type;
        UserId = userId;
    }
}