using Data.Models;

namespace Domain.DTOs;

/// <summary>
/// Line Item object with only the fields a your would input
/// </summary>
public class LineItemInputDTO
{
    public LineItemInputDTO()
    {
    }

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