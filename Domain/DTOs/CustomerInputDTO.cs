﻿using Data.Models;
namespace Domain.DTOs;

public abstract class InputCustomerDTO
{
    /// <summary>
    /// Name of the customer organization
    /// </summary>
    private String CustomerName{ get; set; }
    
    /// <summary>
    /// Country the customer site is in
    /// </summary>
    public String CustomerCountry{ get; set; }    
    
    /// <summary>
    /// Location of the customer site
    /// </summary>
    private Location CustomerLocation{ get; set; }
}