using Data;
using Data.Models;
using Domain.DTOs;
using Microsoft.AspNetCore.Http;

namespace Domain.Utilities;

public class QuoteExtentions
{
    public AuditData AddAuditData(QuoteInputDTO inputQuote, Quote originalQuote, HttpContext context)
    {
        AuditData auditRecord = new AuditData
        {
            EditDate = DateTime.UtcNow,
            EditorID = context?.User?.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? String.Empty,
            EditorName = context?.User?.Claims.FirstOrDefault(x => x.Type == "name")?.Value ?? String.Empty,
        };



        return auditRecord;
    }
}