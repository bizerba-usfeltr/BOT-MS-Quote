using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Data.Models;
namespace Domain.DTOs;

/// <summary>
/// Fields of the quote that may be manually input by user
/// </summary>
public abstract class QuoteInputDTO : IValidatableObject
{
    /// <summary>
    /// The number used for identifying the quote by users.
    /// This number will be the same in the budget and the project
    /// </summary>
    [Required]
    public String QuoteNumber { get; set; }
    
    /// <summary>
    ///  Person who created the quote.
    /// </summary>
    public String SalePersonName{ get; set; }
    
    /// <summary>
    /// Person qho created the quote and is the point of contact regarding the quote.
    /// May or may not be the pm on the project.
    /// </summary>
    [Required]
    public String ContactName{ get; set; }
    
    /// <summary>
    /// The contact persons email address 
    /// </summary>
    [Required]
    public String ContactEmail{ get; set; }

    /// <summary>
    /// The type of job it is: Checkweigher, Industrial System, Labeling, Logistics, Marketing
    /// Metal Detector, Retail, Software, Vision, X-Ray
    /// </summary>
    [Required]
    public QuoteType QuotePrimaryType{ get; set; }
    
    /// <summary>
    /// Optional secondary type of job it is
    /// </summary>
    public QuoteType? QuoteSecondaryType{ get; set; }
    
    /// <summary>
    /// Description of the products and and work to be done covered by the quote
    /// </summary>
    [Required]
    public String Description { get; set; }
    
    /// <summary>
    /// Any disclaimers or conditions that stipulate the scope of the product and/or work covered in this quote 
    /// </summary>
    [Required]
    public String Conditions { get; set; }
    
    /// <summary>
    /// An estimation of how long fulfilling this quote will take
    /// </summary>
    [Required]
    public String LeadTime { get; set; }
    
    /// <summary>
    /// Grand Total price of the Quote
    /// </summary>
    [Required]
    public decimal QuoteTotal{ get; set; }
    
    /// <summary>
    /// The external end customer ie Cargill Springdale
    /// </summary>
    public String ExtCustomerName{ get; set; }
    
    /// <summary>
    /// The external end customers country and location
    /// </summary>
    public String ExtCustomerLocation{ get; set; }    
    
    /// <summary>
    /// Material organization. the main customer for the quote ie Bizerba Germany
    /// </summary>
    [Required]
    public String MOCustomerName{ get; set; }
    
    /// <summary>
    /// Material organizations country and location
    /// </summary>
    [Required]
    public String MOCustomerLocation{ get; set; }

    /// <summary>
    /// The date the quote is valid till, typically 90 days
    /// If a quote is pasted this data it can be recertified by the quoter or pm
    /// </summary>
    [Required]
    //default DateOnly.FromDateTime(DateTime.Now).AddDays(90)
    public DateTime QuoteExpiration{ get; set; }
    
    /// <summary>
    /// A summary of work and materials the quote covers.
    /// typically only one line item per quote but there can be more
    /// </summary>
    public List<LineItemInputDTO> LineItems{ get; set; }

    public QuoteInputDTO()
    {
        
    }

    /// <summary>
    /// Determines if the object is valid
    /// </summary>
    /// <param name="validationContext"></param>
    /// <returns></returns>
    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (this.QuoteExpiration !> DateTime.Today)
        {
            yield return
                new ValidationResult("The expiration date must be later then the creation or last update date",
                    new[]
                    {
                        nameof(this.QuoteExpiration)
                    });
        }

        decimal expectedTotal = 0;
        foreach (var i in LineItems)
        {
            expectedTotal += i.ItemPrice;
        }
        
        if (this.QuoteTotal != expectedTotal)
        {
            yield return
                new ValidationResult("The quote total cannot be different then the sum of the line items prices",
                    new[]
                    {
                        nameof(this.QuoteTotal)
                    });
        }
    }
}