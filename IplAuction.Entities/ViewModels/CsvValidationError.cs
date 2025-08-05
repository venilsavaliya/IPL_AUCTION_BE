public class CsvValidationError
{
    public int RowNumber { get; set; }
    public string FieldName { get; set; } = null!;
    public string ErrorMessage { get; set; } = null!;
}
