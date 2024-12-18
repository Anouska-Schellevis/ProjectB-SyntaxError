public class LocationModel
{
    public Int64 Id { get; set; }
    public string City { get; set; }
    public string Address { get; set; }
    public string PostalCode { get; set; }

    public LocationModel() { }
    public LocationModel(Int64 id, string city, string address, string postalCode)
    {
        Id = id;
        City = city;
        Address = address;
        PostalCode = postalCode;
    }
}