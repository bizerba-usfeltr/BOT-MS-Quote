using System.ComponentModel.DataAnnotations;
using Data.Models;

namespace Domain.DTOs;

/// <summary>
/// Line Item object with only the fields a your would input
/// </summary>
public class LineItemInputDTO : IValidatableObject
{
    public LineItemInputDTO()
    {
    }

    /// <summary>
    /// Identification number of the product 
    /// </summary>
    [Required]
    public String PartNumber { get; set; }
    
    /// <summary>
    /// How many of the specific parts will be covered in a quote
    /// </summary>
    [Required]
    public int Quantity { get; set; }
    
    /// <summary>
    /// Total price of the line item. May be the same as the quote total if there it only one line item 
    /// </summary>
    [Required]
    public decimal ItemPrice { get; set; }

    /// <summary>
    /// A descriptor of the type of job this line item is.
    /// M inventory, C software / t&m, K no sales order 
    /// </summary>
    [Required]
    public Class ClassDesignation { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.Quantity <= 0)
        {
            yield return
                new ValidationResult("The quantity for a line item must be greater than zero",
                    new[]
                    {
                        nameof(this.Quantity)
                    });
        }        
        
        if (this.ItemPrice <= 0)
        {
            yield return
                new ValidationResult("The price for a line item must be greater than zero",
                    new[]
                    {
                        nameof(this.ItemPrice)
                    });
        }
    }
}