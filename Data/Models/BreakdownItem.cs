namespace Data.Models;

public class BreakdownItem
{
    public BreakdownItem()
    {
    }

    public Guid Id { get; set; }
    
    public String Description { get; set; }
    public decimal Total { get; set; }
    public Guid LineItemId { get; set; }
    public LineItem LineItem { get; set; }
}