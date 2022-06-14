using Data.Models;
namespace Domain.DTOs;

/// <summary>
/// Records of changes that have happen of the lifetime of a quote
/// </summary>
public class AuditOutputDTO
{
    public AuditOutputDTO()
    {
    }

    /// <summary>
    /// The name of the person that made the changes
    /// </summary>
    private String EditorName{ get; set; }
    
    /// <summary>
    /// When the changes were made on
    /// </summary>
    private DateTime EditDate{ get; set; }
    
    /// <summary>
    /// What changes were made: field, old value and new value
    /// </summary>
    private List<ChangesOutputDTO> Changes{ get; set; }
}