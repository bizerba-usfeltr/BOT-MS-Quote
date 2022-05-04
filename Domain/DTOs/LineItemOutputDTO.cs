using Data.Models;

namespace Domain.DTOs;

/// <summary>
/// The Line Item object used for display
/// </summary>
public class LineItemOutputDTO
{
    public LineItemOutputDTO()
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
    /// A descriptor of the type of job this line item is.
    /// M inventory, C software / t&m, K no sales order 
    /// </summary>
    public Class ClassDesignation { get; set; }
}