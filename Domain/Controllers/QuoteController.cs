using AutoMapper;
using Data.Models;
using Domain.DTOs;
using Infrastructure.Repos;
using Infrastructure.ResourceParameters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controllers;

/// <summary>
/// Controller containing endpoints pertaining to quotes
/// </summary>
[ApiController]
[Route("api/quotes")]
[Produces(contentType: "application/json", "application/xml")]
public class QuoteController : ControllerBase
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Creates an instance of the Quote controller object
    /// </summary>
    /// <param name="quoteRepository">The repository used to persist quote information</param>
    /// <param name="mapper">The object that maps input/output data to model data</param>
    /// <exception cref="ArgumentNullException"></exception>
    public QuoteController(IQuoteRepository quoteRepository, IMapper mapper)
    {
        _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

        
    /// <summary>
    /// Creates a new quote
    /// </summary>
    /// <param name="quote">the user input quote fields creating a new quote</param>
    /// <returns>An output quote DTO </returns>
    /// <response code="201">Returns the created quote</response>
    /// <response code="409">The new quote would conflict with existing an existing</response>
    /// <response code="422">The supplied labor data has validator errors</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(OutputQuoteDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [Consumes(contentType: "application/json")]
    [HttpPost]
    public ActionResult<OutputQuoteDTO> CreateQuote(QuoteInputDTO quote)
    {
        var quoteEntity = _mapper.Map<Quote>(quote);
        try
        {
            if (HttpContext.User.Identity != null)
            {
                quoteEntity.QuoteCreatedBy = HttpContext.User.Identity.Name;
            }
        }catch(Exception e)
        {
            
        }
        _quoteRepository.AddQuote(quoteEntity);
        _quoteRepository.Save();

        var returnQuote = _mapper.Map<OutputQuoteDTO>(quoteEntity);
        return CreatedAtRoute("GetQuote", new {QuoteId = returnQuote.QuoteId}, returnQuote);
    }
    
    /// <summary>
    /// Get a single quote based on the id of the record in the db
    /// </summary>
    /// <param name="quoteId">id used as primary key of the quote to get</param>
    /// <returns>An action result with the output quote DTO in the response body</returns>
    /// <response code="200">Returns the quote associated with the given quote id</response>
    /// <response code="404">A quote with that Id could not be found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(OutputQuoteDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{quoteId}", Name = "GetQuote")]
    public ActionResult<OutputQuoteDTO> GetQuote(Guid quoteId)
    {
        var quote = _quoteRepository.GetQuote(quoteId);
        
        if (quote == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<OutputQuoteDTO>(quote));
    }
    
    /// <summary>
    /// Get a list of all quotes that match the search and filter parameters sent on the query string
    /// </summary>
    /// <param name="quoteResourceParams">string parameters to search and filter results by:
    /// QuoteNumber, QuotedBy, Type, SecondaryType, CustomerName, MOName </param>
    /// <returns>An action result with an IEnumerable of quotes that match the search and filter parameters in the response body</returns>
    /// <response code="200">Returns the quote associated with the given quote id</response>
    /// <response code="404">A quote with that met the query parameters could not be found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<OutputQuoteDTO>))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public ActionResult<IEnumerable<OutputQuoteDTO>> GetQuotes([FromQuery] QuoteResourceParams quoteResourceParams)
    {
        if(quoteResourceParams != null)
        {
            var quotes = _quoteRepository.GetQuotes(quoteResourceParams);
            if (quotes == null || !quotes.Any())
            {
                return NotFound();
            }
            return Ok(_mapper.Map<IEnumerable<OutputQuoteDTO>>(quotes));
        }

        return NotFound();
    }


    /// <summary>
    /// Updates all fields of a quote. Any fields that have not been given a value will be assigned its default value.
    /// If the id of the quote doesn't exist, a quote with that id and the input values will be created 
    /// </summary>
    /// <param name="quoteInput">the user input quote fields updating an existing quote</param>
    /// <param name="quoteId">the id of the quote to be updated</param>
    /// <returns></returns>
    /// <response code="200">The update succeeded</response>
    /// <response code="201">A quote with that Id was not found so one was created</response>
    /// <response code="422">The supplied quote data has validator errors</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [Consumes(contentType: "application/json")]
    [HttpPut("{quoteId}")]
    public IActionResult UpdateQuote(QuoteUpdateDTO quoteInput, Guid quoteId)
    {
        if (!_quoteRepository.QuoteExists(quoteId))
        {
            var quoteToAdd = _mapper.Map<Quote>(quoteInput);
            quoteToAdd.QuoteId = quoteId;
            try
            {
                if (HttpContext.User.Identity != null)
                {
                    quoteToAdd.QuoteCreatedBy = HttpContext.User.Identity.Name;
                }
            }catch(Exception e)
            {
            
            }
            _quoteRepository.AddQuote(quoteToAdd);
            _quoteRepository.Save();
            var returnQuote = _mapper.Map<OutputQuoteDTO>(quoteToAdd);
            return CreatedAtRoute("GetQuote", new {returnQuote.QuoteId}, returnQuote);
        }

        var quote = _quoteRepository.GetQuote(quoteId);
        _mapper.Map(quoteInput, quote);

        _quoteRepository.UpdateQuote(quote);
        _quoteRepository.Save();
        return Ok(_mapper.Map<OutputQuoteDTO>(quote));
        
    }

    /// <summary>
    /// Updates the fields of the quote present in the patch document. Fields not in the document will not be changed
    /// </summary>
    /// <param name="quoteInput">Patch document with the user input quote fields to be updated on an existing quote</param>
    /// <param name="quoteId">The id of the quote to be updated</param>
    /// <returns></returns>
    /// <response code="204">The update succeeded</response>
    /// <response code="404">A quote with that quote id could not be found</response>
    /// <response code="422">The supplied quote data has validator errors</response>
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [Consumes(contentType: "application/json")]
    [HttpPatch("{quoteId}")]
    public ActionResult<OutputQuoteDTO> UpdateQuote(JsonPatchDocument<QuoteInputDTO> quoteInput, Guid quoteId)
    {
        if (!_quoteRepository.QuoteExists(quoteId))
        {
            return NotFound();
        }
        var quote = _quoteRepository.GetQuote(quoteId);
        
        try
        {
            if (HttpContext.User.Identity != null)
            {
                quote.QuoteCreatedBy = HttpContext.User.Identity.Name;
            }
        }catch(Exception e)
        {
            
        }
        _mapper.Map(quoteInput, quote);

        _quoteRepository.UpdateQuote(quote);
        _quoteRepository.Save();
        
        return Ok();
    }
}