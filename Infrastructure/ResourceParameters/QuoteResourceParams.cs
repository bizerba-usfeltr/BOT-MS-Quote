namespace Infrastructure.ResourceParameters;

public class QuoteResourceParams
{
    public String QuoteNumber { get; set; }
    public String QuotedBy { get; set; }
    public String Type { get; set; }
    public String SecondaryType { get; set; }
    public String CustomerName { get; set; }
    public String MOName { get; set; }
    //TODO: exact creation date is unlikely to be searched by but should be able to search/filter by year and month
}