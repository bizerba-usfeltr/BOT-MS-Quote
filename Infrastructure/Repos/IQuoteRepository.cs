using Data.Models;
using Infrastructure.ResourceParameters;

namespace Infrastructure.Repos;

public interface IQuoteRepository
{
    void AddQuote(Quote quote);
    Quote GetQuote(Guid quoteId);
    Quote GetQuoteByNumber(String quoteNumber);
    IEnumerable<Quote> GetQuotes();
    IEnumerable<Quote> GetQuotes(QuoteResourceParams quoteResourceParams);
    void UpdateQuote(Quote quote);
    bool QuoteExists(Guid quoteId);

    void AddLineItem(Guid quoteId, LineItem item);
    LineItem GetLineItem(Guid quoteId, Guid itemId);
    IEnumerable<LineItem> GetLineItems(Guid quoteId);
    void UpdateLineItem(LineItem item);
    void DeleteLineItem(LineItem item);
    bool LineItemExists(Guid itemId);
    bool Save();
}