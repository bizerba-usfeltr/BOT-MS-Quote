using Data.Models;
namespace Domain.DTOs;

/// <summary>
/// Quote fields to be displayed in the ui 
/// </summary>
public class OutputQuoteDTO
{
    public OutputQuoteDTO()
    {
    }

    /// <summary>
    /// Id of the quote record in the db, used as unique identifier (pk,fk), not for display
    /// </summary>
    public Guid QuoteId { get; set; }   
    
    /// <summary>
    /// The number used for identifying the quote by users.
    /// This number will be the same in the budget and the project
    /// </summary>
    public String QuoteNumber { get; set; }
   
    /// <summary>
    /// Person who created the quote.
    /// </summary>
    public String QuoteCreatedBy{ get; set; }    
    
    /// <summary>
    /// Person in sales involved in the quote.
    /// </summary>
#nullable enable
    public String? SalePersonName{ get; set; } 
#nullable disable  
    
    /// <summary>
    /// Person who requested the quote and is the point of contact regarding the quote.
    /// </summary>
    public String ContactName{ get; set; }  
    
    /// <summary>
    /// The contact persons email address 
    /// </summary>
    public String ContactEmail{ get; set; }
    
    /// <summary>
    /// Date the quote was created
    /// </summary>
    public DateOnly QuoteDate{ get; set; }
    
    /// <summary>
    /// Date the quote is valid till, typically 90 days from creation or last update 
    /// </summary>
    public DateOnly QuoteExpiration{ get; set; }
    
    /// <summary>
    /// The type of job it is: Checkweigher, Industrial System, Labeling, Logistics, Marketing
    /// Metal Detector, Retail, Software, Vision, X-Ray
    /// </summary>
    public QuoteType QuotePrimaryType{ get; set; }
    
    /// <summary>
    /// Optional secondary type of job it is
    /// </summary>
#nullable enable
    public QuoteType? QuoteSecondaryType{ get; set; }
#nullable disable 
 
    /// <summary>
    /// Description of the products and and work to be done covered by the quote
    /// </summary>
    public String Description { get; set; }
    
    /// <summary>
    /// Any disclaimers or conditions that stipulate the scope of the product and/or work covered in this quote 
    /// </summary>
    public String Conditions { get; set; }
    
    /// <summary>
    /// An estimation of how long fulfilling this quote will take
    /// </summary>
    public String LeadTime { get; set; }
    
    /// <summary>
    /// Grand Total price of the Quote
    /// </summary>
    public decimal QuoteTotal{ get; set; }

    /// <summary>
    /// The external end customer ie Cargill
    /// </summary>
#nullable enable
    public String? ExtCustomerName{ get; set; }  
    
    /// <summary>
    /// Material organizations country and location
    /// </summary>
    public String? ExtCustomerLocation{ get; set; }
#nullable disable    
    
    /// <summary>
    /// Material organization. the main customer for the quote ie Bizerba Germany
    /// </summary>
    public String MOCustomerName{ get; set; }
    
    /// <summary>
    /// Material organizations country and location
    /// </summary>
    public String MOCustomerLocation{ get; set; }

    /// <summary>
    /// A summary of work and materials the quote covers.
    /// typically only one line item per quote but there can be more
    /// </summary>
    public List<LineItem> LineItems{ get; set; }

}