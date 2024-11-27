public class VoucherModel
{
    public Int64 Id { get; set; }
    public string Code { get; set; }
    public string Description { get; set; }
    public int Amount { get; set; }
    public string Type { get; set; }
    public Int64 UserId { get; set; }

    public VoucherModel(Int64 id, string code, string description, int amount, string type, Int64 userId)
    {
        Id = id;
        Code = code;
        Description = description;
        Amount = amount;
        Type = type;
        UserId = userId;
    }
}