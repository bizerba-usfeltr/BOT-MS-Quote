namespace Data.Models;

/// <summary>
/// A record of a change to the quote
/// </summary>
public class AuditData
{
    public AuditData()
    {
    }

    /// <summary>
    /// Id of the audit record in the db, used as unique identifier (pk,fk)
    /// </summary>
    public Guid AuditId { get; set; }
    
    /// <summary>
    /// The name of the person that made the changes
    /// </summary>
    public String EditorName{ get; set; }
    
    /// <summary>
    /// When the changes were made on
    /// </summary>
    public DateTime EditDate{ get; set; }
    
    /// <summary>
    /// The Quote the changes where made to
    /// </summary>
    public Quote Quote{ get; set; }   
    
    /// <summary>
    /// The id of the quote the changes where made to
    /// </summary>
    public Guid QuoteId{ get; set; }

    /// <summary>
    /// What changes were made: field, old value and new value
    /// </summary>
    public ICollection<ChangeRecord> Changes{ get; set; }
}