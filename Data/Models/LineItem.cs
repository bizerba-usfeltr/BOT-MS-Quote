namespace Data.Models;

/// <summary>
/// An entry in a quote detailing the work and materials of the quote
/// </summary>
public class LineItem
{
    public LineItem()
    {
    }

    /// <summary>
    /// Id of the line item record in the db, used as unique identifier (pk,fk)
    /// </summary>
    public Guid LineItemId { get; set; }
    
    /// <summary>
    /// The order the line item was added to the quote ie 1 for first, 2 for second ect
    /// </summary>
    public int LineNumber { get; set; }
    
    /// <summary>
    /// Identification number of the product 
    /// </summary>
    public String PartNumber { get; set; }
    
    /// <summary>
    /// How many of the specific parts will be covered in a quote
    /// </summary>
    public int Quantity { get; set; }
    
    /// <summary>
    /// Total price of the line item. May be the same as the quote total if there it only one line item 
    /// </summary>
    public decimal ItemPrice { get; set; }

    /// <summary>
    /// A descriptor of the type of job this line item is: M inventory, C software / t&m, K no sales order 
    /// </summary>
    public Class ClassDesignation { get; set; }

    /// <summary>
    /// The Id of Quote the line item belongs to
    /// </summary>
    public Guid QuoteId{ get; set; }
    
    /// <summary>
    /// The Quote the line item belongs to
    /// </summary>
    public Quote Quote { get; set; }
    
    /// <summary>
    /// The list of ids of the breakdown items that make up this line item
    /// </summary>
    public ICollection<BreakdownItem> BreakdownItems { get; set; }
}

/// <summary>
/// Class designation of a line item: M inventory, C software / t&m, K no sales order 
/// </summary>
public enum Class{
    M,
    C,
    K
}