using System.ComponentModel.DataAnnotations.Schema;

namespace Data.Models;

/// <summary>
/// The location of a customer
/// </summary>
[NotMapped]
public class Location
{
    public Location()
    {
    }

    /// <summary>
    /// Id of the location record in the db, used as unique identifier (pk,fk)
    /// </summary>
    public Guid LocationId { get; set; }
    
    /// <summary>
    /// City the customer site is located in
    /// </summary>
    public String City { get; set; }
    
    /// <summary>
    /// State or Provence the customer site is located in
    /// </summary>
    public String State { get; set; }

    /// <summary>
    /// The Id of the customer that owns this location
    /// </summary>
    public Guid CustomerId { get; set; }

    /// <summary>
    /// the customer that owns this site
    /// </summary>
    public Customer Customer { get; set; }

}