public class SeatsModel
{
    public Int64 Id { get; set; }
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public decimal Price { get; set; }
    public SeatsModel() { }

    public SeatsModel(Int64 id, int rowNumber, int columnNumber, decimal price)
    {
        Id = id;
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        Price = price;
    }


}



