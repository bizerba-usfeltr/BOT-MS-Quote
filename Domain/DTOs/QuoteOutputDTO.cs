using Data.Models;
namespace Domain.DTOs;

public class OutputQuoteDTO
{
    /// <summary>
    /// Id of the quote record in the db, used as unique identifier (pk,fk)
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
    public String QuotedByName{ get; set; }
    
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
    /// The type of job it is: Checkweigher, Industrial System, Labeling, Logistics, Marketing
    /// Metal Detector, Retail, Software, Vision, X-Ray
    /// </summary>
    public QuoteType QuotePrimaryType{ get; set; }
    
    /// <summary>
    /// Optional secondary type of job it is
    /// </summary>
    public QuoteType? QuoteSecondaryType{ get; set; }
    
    /// <summary>
    /// Grand Total price of the Quote
    /// </summary>
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
    public String MOCustomerName{ get; set; }
    
    /// <summary>
    /// Material organizations country and location
    /// </summary>
    public String MOCustomerLocation{ get; set; }
    
    /// <summary>
    /// The date the quote is valid till, typically 90 days from the creation date
    /// If a quote is passed this data it can be re-certified as valid for another period by the quoter or pm
    /// </summary>
    public DateOnly QuoteExpiration{ get; set; }
    
    /// <summary>
    /// A summary of work and materials the quote covers.
    /// typically only one line item per quote but there can be more
    /// </summary>
    public List<LineItem> LineItems{ get; set; }
}