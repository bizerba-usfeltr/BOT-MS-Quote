using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

// The Quote in it's entirety as it is in the DB
public class Quote
{
    // Id of the quote record in the db, used as unique identifier (pk,fk)
    public Quote()
    {
    }

    [Key]
    [Required]
    public Guid QuoteId { get; set; }

    // The number used for identifying the quote by users. will be the same in the budget and the project
    [Required]
    public String QuoteNumber { get; set; }
    
    // Person who created the quote.
    public String QuoteCreatedBy{ get; set; }
        
    // Person in sales involved in the quote.
#nullable enable
    public String? SalePersonName{ get; set; } 
#nullable disable  
    
    // Person who requested the quote and is the point of contact regarding the quote.
    public String ContactName{ get; set; }

    // The contact persons email address 
    public String ContactEmail{ get; set; }

    // Date the quote was created
    public DateOnly QuoteDate{ get; set; }
    
    // The date the quote is valid till, typically 90 days
    // If a quote is pasted this data it can be recertified as valid for another period by the quoter or pm
    public DateOnly QuoteExpiration{ get; set; }
    
    // The type of job it is: Checkweigher, Industrial System, Labeling, Logistics, Marketing
    //Metal Detector, Retail, Software, Vision, X-Ray
    public QuoteType QuotePrimaryType{ get; set; }
    
    // Optional secondary type of job it is
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
    
    // Grand Total price of the Quote
    public decimal QuoteTotal{ get; set; }

    // The id of the external end customer
#nullable enable
    public Guid? QuoteCustomerExtId{ get; set; }    
    
    // The external end customer ie Cargill
    public CustomerExt? QuoteCustomerExt{ get; set; }    
#nullable disable   
    
    // The id of the mo
    public Guid QuoteCustomerMOId{ get; set; }

    // Material organization. the main customer for the quote ie Bizerba Germany
    public CustomerMO QuoteCustomerMo{ get; set; }

            
    // Id of the quote breakdown record
    // used like a foriegn key but must be nullable to allow for backwards compatability
    public String? BreakdownId { get; set; }

    // A summary of work and materials the quote covers.
    // typically only one line item per quote but there can be more
    public ICollection<LineItem> LineItems{ get; set; }

    //A record of when and how the quote has changed 
    public ICollection<AuditData> Revisions{ get; set; }
}