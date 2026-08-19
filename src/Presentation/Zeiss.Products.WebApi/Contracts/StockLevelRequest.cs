namespace Zeiss.Products.WebApi.Contracts;

public sealed class StockLevelRequest : PageRequest
{
    public int? Min { get; set; }
    public int? Max { get; set; }
}