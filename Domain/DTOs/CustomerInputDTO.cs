using Data.Models;
namespace Domain.DTOs;

/// <summary>
/// Customer fields that can be manually input by the user
/// </summary>
public class InputCustomerDTO
{
    public InputCustomerDTO()
    {
    }

    /// <summary>
    /// Name of the customer organization
    /// </summary>
    public String CustomerName{ get; set; }
}