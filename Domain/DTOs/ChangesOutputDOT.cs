namespace Domain.DTOs;

/// <summary>
/// A record of changes to a field
/// </summary>
public class ChangesOutputDOT
{
    /// <summary>
    /// The name of the field that was changed
    /// </summary>
    private String FieldName{ get; set; }
    
    /// <summary>
    /// The old value before it was changed
    /// </summary>
    private String OldValue{ get; set; }
    
    /// <summary>
    /// The new value after the change was saved
    /// </summary>
    private String NewValue{ get; set; }
}