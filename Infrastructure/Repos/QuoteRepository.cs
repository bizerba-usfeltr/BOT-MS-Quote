using System.Linq;
using Data.Models;

using Infrastructure.Contexts;
using Infrastructure.ResourceParameters;

namespace Infrastructure.Repos;

public class QuoteRepository : IQuoteRepository
{
    private readonly QuoteContext _context;

    public QuoteRepository(QuoteContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public void AddQuote(Quote quote)
    {
        if (quote == null)
        {
            throw new ArgumentNullException(nameof(quote));
        }

        _context.Quotes.Add(quote);
        
    }
    public Quote GetQuote(Guid quoteId)
    {
        if (quoteId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(quoteId));
        }

        return _context.Quotes.FirstOrDefault(q => q.QuoteId == quoteId);
    }
    public IEnumerable<Quote> GetQuotes()
    {
        return _context.Quotes.ToList<Quote>();
    }
    public IEnumerable<Quote> GetQuotes(QuoteResourceParams quoteResourceParams)
    {
        if(string.IsNullOrWhiteSpace(quoteResourceParams.Type) 
           && string.IsNullOrWhiteSpace(quoteResourceParams.SecondaryType)
           && string.IsNullOrWhiteSpace(quoteResourceParams.CustomerName) 
           && string.IsNullOrWhiteSpace(quoteResourceParams.QuotedBy) 
           && string.IsNullOrWhiteSpace(quoteResourceParams.QuoteNumber)
           && string.IsNullOrWhiteSpace(quoteResourceParams.MOName))
        {
            return _context.Quotes.ToList<Quote>();
        }

        var collection = _context.Quotes as IQueryable<Quote>;
        if (!string.IsNullOrWhiteSpace(quoteResourceParams.Type))
        {
            var type = quoteResourceParams.Type.Trim();
            collection = collection.Where(q => q.QuotePrimaryType.Equals(type));
        }          
        
        if (!string.IsNullOrWhiteSpace(quoteResourceParams.SecondaryType))
        {
            var type = quoteResourceParams.SecondaryType.Trim();
            collection = collection.Where(q => q.QuoteSecondaryType.Equals(type));
        }        
        
        if (!string.IsNullOrWhiteSpace(quoteResourceParams.MOName))
        {
            var name = quoteResourceParams.MOName.Trim();
            collection = collection.Where(q => q.QuoteCustomerMo.CustomerName.Equals(name));
        }
        
        if (!string.IsNullOrWhiteSpace(quoteResourceParams.CustomerName))
        {
            var name = quoteResourceParams.CustomerName.Trim();
            collection = collection.Where(q => q.QuoteCustomerExt.CustomerName.Equals(name));
        }         
        
        if (!string.IsNullOrWhiteSpace(quoteResourceParams.QuotedBy))
        {
            var name = quoteResourceParams.QuotedBy.Trim();
            collection = collection.Where(q => q.QuotedName.Equals(name));
        }          
        
        if (!string.IsNullOrWhiteSpace(quoteResourceParams.QuoteNumber))
        {
            var number = quoteResourceParams.QuoteNumber.Trim();
            collection = collection.Where(q => q.QuoteNumber.Equals(number));
        }        
   
        return collection.ToList();
    }
    public void UpdateQuote(Quote quote)
    {
        
    }
    public bool QuoteExists(Guid quoteId)
    {
        if (quoteId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(quoteId));
        }

        return _context.Quotes.Any(q => q.QuoteId == quoteId);
    }    
    
    public void AddLineItem(Guid quoteId, LineItem item)
    {
        if (quoteId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(quoteId));
        }
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        item.QuoteId = quoteId;
        _context.LineItems.Add(item);
    }
    public LineItem GetLineItem(Guid itemId)
    {
        if (itemId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(itemId));
        }

        return _context.LineItems.FirstOrDefault(i => i.LineItemId == itemId);
    }
    public IEnumerable<LineItem> GetLineItems(Guid quoteId)
    {
        if (quoteId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(quoteId));
        }
        
        var collection = _context.LineItems as IQueryable<LineItem>;
        collection = collection.Where(q => q.QuoteId.Equals(quoteId));
        
        return collection.ToList();
    }
    public void DeleteLineItem(LineItem item)
    {
        if (item == null)
        {
            throw new ArgumentNullException(nameof(item));
        }
        
        _context.LineItems.Remove(item);
    }
    public bool LineItemExists(Guid itemId)
    {
        if (itemId == Guid.Empty)
        {
            throw new ArgumentNullException(nameof(itemId));
        }

        return _context.LineItems.Any(i => i.QuoteId == itemId);
    }

    private void AddAuit()
    {
        
    }

    private void AddChange()
    {
        
    }
    public bool Save()
    {
        return (_context.SaveChanges() >= 0);
    }
}