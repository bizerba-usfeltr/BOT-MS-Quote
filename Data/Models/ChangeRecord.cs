namespace Data.Models;

/// <summary>
/// A single change made to the quote
/// </summary>
public class ChangeRecord
{
    public ChangeRecord()
    {
    }

    /// <summary>
    /// Id of the change record in the db, used as unique identifier (pk,fk)
    /// </summary>
    public Guid ChangeId { get; set; }
    
    /// <summary>
    /// The name of the field that was changed
    /// </summary>
    public String FieldName{ get; set; }
    
    /// <summary>
    /// The old value before it was changed
    /// </summary>
    public String OldValue{ get; set; }
    
    /// <summary>
    /// The new value after the change was saved
    /// </summary>
    public String NewValue{ get; set; }
    
    /// <summary>
    /// the id of the audit record the change belongs to
    /// </summary>
    public Guid AuditId { get; set; }
    
    /// <summary>
    /// the Audit the change is a part of 
    /// </summary>
    public AuditData Audit { get; set; }
}