using System.Reflection;
using Data;
using Data.Models;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Utilities;

public class QuoteExtentions
{
    public static AuditData AddAuditData(Quote inputQuote, Quote originalQuote, HttpContext context)
    {
        AuditData auditRecord = new AuditData
        {
            EditDate = DateTime.UtcNow,
            EditorID = context?.User?.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? String.Empty,
            EditorName = context?.User?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? String.Empty,
        };

        if (String.Compare(inputQuote.QuoteNumber, originalQuote.QuoteNumber, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.QuoteNumber),
                NewValue = inputQuote.QuoteNumber,
                OldValue = originalQuote.QuoteNumber
            };
            auditRecord.Changes.Add(change);
        }
        if (String.Compare(context.User.Identity.Name, originalQuote.QuoteCreatedBy, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(originalQuote.QuoteCreatedBy),
                NewValue = context.User.Identity.Name,
                OldValue = originalQuote.QuoteNumber
            };
            auditRecord.Changes.Add(change);
        }
        if (String.Compare(inputQuote.SalePersonName, originalQuote.SalePersonName, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.SalePersonName),
                NewValue = inputQuote.SalePersonName,
                OldValue = originalQuote.SalePersonName
            };
            auditRecord.Changes.Add(change);
        } 
        if (String.Compare(inputQuote.ContactName, originalQuote.ContactName, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.ContactName),
                NewValue = inputQuote.ContactName,
                OldValue = originalQuote.ContactName
            };
            auditRecord.Changes.Add(change);
        }         
        if (String.Compare(inputQuote.ContactEmail, originalQuote.ContactEmail, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.ContactEmail),
                NewValue = inputQuote.ContactEmail,
                OldValue = originalQuote.ContactEmail
            };
            auditRecord.Changes.Add(change);
        } 
//TODO: decide how to deal with quote date and expiration
        if (inputQuote.QuotePrimaryType != originalQuote.QuotePrimaryType)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.QuotePrimaryType),
                NewValue = inputQuote.QuotePrimaryType.ToString(),
                OldValue = originalQuote.QuotePrimaryType.ToString()
            };
            auditRecord.Changes.Add(change);
        }        
        if (inputQuote.QuoteSecondaryType != originalQuote.QuoteSecondaryType)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.QuoteSecondaryType),
                NewValue = inputQuote.QuoteSecondaryType.ToString(),
                OldValue = originalQuote.QuoteSecondaryType.ToString()
            };
            auditRecord.Changes.Add(change);
        }
        if (String.Compare(inputQuote.Description, originalQuote.Description, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.Description),
                NewValue = inputQuote.Description,
                OldValue = originalQuote.Description
            };
            auditRecord.Changes.Add(change);
        }         
        if (String.Compare(inputQuote.Conditions, originalQuote.Conditions, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.Conditions),
                NewValue = inputQuote.Conditions,
                OldValue = originalQuote.Conditions
            };
            auditRecord.Changes.Add(change);
        }         
        if (String.Compare(inputQuote.LeadTime, originalQuote.LeadTime, StringComparison.CurrentCulture) !=0)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.LeadTime),
                NewValue = inputQuote.LeadTime,
                OldValue = originalQuote.LeadTime
            };
            auditRecord.Changes.Add(change);
        }         
        if (inputQuote.QuoteTotal!= originalQuote.QuoteTotal)
        {
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.QuoteTotal),
                NewValue = inputQuote.QuoteTotal.ToString(),
                OldValue = originalQuote.QuoteTotal.ToString()
            };
            auditRecord.Changes.Add(change);
        } 
        if (String.Compare(inputQuote.QuoteCustomerExt.CustomerName, originalQuote.QuoteCustomerExt.CustomerName, StringComparison.CurrentCulture) !=0)
        {
            //TODO: if names don't match, check with customer service to see if a customer with that name exists
            //add change record for customer id
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.QuoteCustomerExt.CustomerName),
                NewValue = inputQuote.QuoteCustomerExt.CustomerName,
                OldValue = originalQuote.QuoteCustomerExt.CustomerName
            };
            auditRecord.Changes.Add(change);
        }            
        if (String.Compare(inputQuote.QuoteCustomerMo.CustomerName, originalQuote.QuoteCustomerMo.CustomerName, StringComparison.CurrentCulture) !=0)
        {
            //TODO: if names don't match, check with customer service to see if a customer with that name exists
            //add change record for customer id
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.QuoteCustomerMo.CustomerName),
                NewValue = inputQuote.QuoteCustomerMo.CustomerName,
                OldValue = originalQuote.QuoteCustomerMo.CustomerName
            };
            auditRecord.Changes.Add(change);
        }         
        if (String.Compare(inputQuote.QuoteCustomerExt.CustomerLocation.City, originalQuote.QuoteCustomerExt.CustomerLocation.City, StringComparison.CurrentCulture) !=0)
        {
            //TODO: how to deal with location matching, duplicate for mo location
            ChangeRecord change = new ChangeRecord
            {
                FieldName = nameof(inputQuote.LeadTime),
                NewValue = inputQuote.LeadTime,
                OldValue = originalQuote.LeadTime
            };
            auditRecord.Changes.Add(change);
        } 
        return auditRecord;
    }

    public static AuditData InitialRecord(Quote quote, HttpContext context)
    {
        AuditData auditRecord = new AuditData
        {
            EditDate = DateTime.UtcNow,
            EditorID = context?.User?.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? String.Empty,
            EditorName = context?.User?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? String.Empty,
            Changes = new List<ChangeRecord>{ }
        };

        foreach (PropertyInfo prop in quote.GetType().GetProperties())
        {
            string val = "";
            if(prop.GetValue(quote) != null)
            {
               val = prop.GetValue(quote).ToString();
            }
            ChangeRecord change = new ChangeRecord
            {
                FieldName = prop.Name,
                NewValue = val,
                OldValue = ""
            };
            auditRecord.Changes.Add(change);
        }
        return auditRecord;
    }
}