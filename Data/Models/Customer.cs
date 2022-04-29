﻿namespace Data.Models;

public class Customer
{
    /// <summary>
    /// Id of the customer record in the db, used as unique identifier (pk,fk)
    /// </summary>
    public Guid CustomerId { get; set; }
    
    /// <summary>
    /// Country the customer site is in
    /// </summary>
    public String CustomerCountry{ get; set; }    
    
    /// <summary>
    /// Name of the customer organization
    /// </summary>
    public String CustomerName{ get; set; }

    /// <summary>
    /// Location of the customer site
    /// </summary>
    public Location CustomerLocation{ get; set; }
    
    /// <summary>
    /// Id of the location of the customer site
    /// </summary>
    public Guid CustomerLocationId{ get; set; }
    
    /// <summary>
    /// the list of quotes a customer has
    /// </summary>
    public ICollection<Quote> Quotes { get; set; }
}