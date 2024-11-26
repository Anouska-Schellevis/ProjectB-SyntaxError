public class SeatsModel
{
    public Int64 Id { get; set; }
    public int RowNumber { get; set; }
    public int ColumnNumber { get; set; }
    public double Price { get; set; }
    public SeatsModel() { }

    public SeatsModel(Int64 id, int rowNumber, int columnNumber, double price)
    {
        Id = id;
        RowNumber = rowNumber;
        ColumnNumber = columnNumber;
        Price = price;
    }


}



