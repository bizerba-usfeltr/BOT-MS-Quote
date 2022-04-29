namespace Data.Models;

/// <summary>
/// An entry in a quote detailing the work and materials of the quote
/// </summary>
public class LineItem
{
    /// <summary>
    /// Id of the line item record in the db, used as unique identifier (pk,fk)
    /// </summary>
    public Guid LineItemId { get; set; }
    
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
    /// Description of the item and work to be done
    /// </summary>
    public String Description { get; set; }
    
    /// <summary>
    /// Any disclaimers or conditions that stipulate the scope of the product and/or work covered in this line item 
    /// </summary>
    public String Conditions { get; set; }
    
    /// <summary>
    /// A descriptor of the type of job this line item is: M inventory, C software / t&m, K no sales order 
    /// </summary>
    public Class ClassDesignation { get; set; }
    
    /// <summary>
    /// An estimation of how long fulfilling this line item will take
    /// </summary>
    public String LeadTime { get; set; }
    
    /// <summary>
    /// The Id of Quote the line item belongs to
    /// </summary>
    public Guid QuoteId{ get; set; }
    
    /// <summary>
    /// The Quote the line item belongs to
    /// </summary>
    public Quote Quote { get; set; }
}

/// <summary>
/// Class designation of a line item: M inventory, C software / t&m, K no sales order 
/// </summary>
public enum Class{
    M,
    C,
    K
}