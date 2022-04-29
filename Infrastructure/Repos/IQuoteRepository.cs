using Data.Models;
using Infrastructure.ResourceParameters;

namespace Infrastructure.Repos;

public interface IQuoteRepository
{
    void AddQuote(Quote quote);
    Quote GetQuote(Guid quoteId);
    IEnumerable<Quote> GetQuotes();
    IEnumerable<Quote> GetQuotes(QuoteResourceParams quoteResourceParams);
    void UpdateQuote(Quote quote);
    bool QuoteExists(Guid quoteId);

    void AddLineItem(Guid quoteId, LineItem item);
    LineItem GetLineItem(Guid itemId);
    IEnumerable<LineItem> GetLineItems(Guid quoteId);
    void DeleteLineItem(LineItem item);
    bool LineItemExists(Guid itemId);
    bool Save();
}