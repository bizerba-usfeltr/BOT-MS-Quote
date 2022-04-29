namespace Data.Models;

/// <summary>
/// The location of a customer
/// </summary>
public class Location
{
    /// <summary>
    /// Id of the location record in the db, used as unique idendifier (pk,fk)
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