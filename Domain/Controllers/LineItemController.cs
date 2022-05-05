using AutoMapper;
using Data.Models;
using Domain.DTOs;
using Infrastructure.Repos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Domain.Controllers;

/// <summary>
/// Controller containing the endpoints pertaining to line items
/// </summary>
[ApiController]
[Route("api/quotes/{quoteId}/lineitems")]
[Produces(contentType: "application/json", "application/xml")]
public class LineItemController : ControllerBase
{
    private readonly IQuoteRepository _quoteRepository;
    private readonly IMapper _mapper;
    
    /// <summary>
    /// Creates and instance of the Line item controller object
    /// </summary>
    /// <param name="quoteRepository">The repository used to persist quote information</param>
    /// <param name="mapper">The object that maps input/output data to model data</param>
    /// <exception cref="ArgumentNullException"></exception>
    public LineItemController(IQuoteRepository quoteRepository, IMapper mapper)
    {
        _quoteRepository = quoteRepository ?? throw new ArgumentNullException(nameof(quoteRepository));
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    /// <summary>
    /// Creates a line item 
    /// </summary>
    /// <param name="item">details of the line item to save input by the user</param>
    /// <param name="quoteId">The id of the quote the line item will be added to</param>
    /// <returns></returns>
    /// <response code="201">Returns the created line item</response>
    /// <response code="409">The new line item would conflict with an existing one</response>
    /// <response code="422">The line item data has validator errors</response>
    [ProducesResponseType(StatusCodes.Status201Created, Type = typeof(LineItemOutputDTO))]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [Consumes(contentType: "application/json")]
    [HttpPost]
    public ActionResult<LineItemOutputDTO> CreateLineItem(LineItemInputDTO item, Guid quoteId)
    {
        var itemEntity = _mapper.Map<LineItem>(item);
        _quoteRepository.AddLineItem(quoteId, itemEntity);
        _quoteRepository.Save();

        var returnLineItem = _mapper.Map<LineItemOutputDTO>(itemEntity);
        
        return CreatedAtRoute("GetLineItem", new {returnLineItem.LineItemId}, returnLineItem);
    }   
    
    /// <summary>
    /// Get a single line item
    /// </summary>
    /// <param name="quoteId">id of the quote the line items must be from</param>
    /// <param name="itemId">the id of the line item</param>
    /// <returns></returns>
    /// <response code="200">Returns the line item associated with the given line item id</response>
    /// <response code="404">A quote or line item with those Ids could not be found</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LineItemOutputDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet("{itemId}" , Name="GetLineItem")]
    public ActionResult<LineItemOutputDTO> GetLineItem(Guid quoteId, Guid itemId)
    {
        if (!_quoteRepository.QuoteExists(quoteId))
        {
            return NotFound();
        }

        if (!_quoteRepository.LineItemExists(itemId))
        {
            return NotFound();
        }
        var item = _quoteRepository.GetLineItem(quoteId, itemId);
        return Ok(_mapper.Map<LineItemOutputDTO>(item));
    }
     
    /// <summary>
    /// Get all the line items that belong to the quote based on the quote id 
    /// </summary>
    /// <param name="quoteId">id of the quote the line items must be from</param>
    /// <returns></returns>
    /// <response code="200">Returns all the line item associated with the given quote id</response>
    /// <response code="404">A quote with that Id could not be found or has no line items</response>
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(LineItemOutputDTO))]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [HttpGet]
    public ActionResult<IEnumerable<LineItemOutputDTO>> GetLineItems(Guid quoteId)
    {
        var items = _quoteRepository.GetLineItems(quoteId);
        
        if (items == null)
        {
            return NotFound();
        }
        return Ok(_mapper.Map<IEnumerable<LineItemOutputDTO>>(items));
    }

    /// <summary>
    /// Updates all fields of a line item if the line item id exists and creates an item with that id if it doesn't 
    /// </summary>
    /// <param name="itemInput">details of the line item to save input by the user</param>
    /// <param name="quoteId">The id of the quote the line item belongs to or will be added to</param>
    /// <param name="itemId">The id of the line item to be updated</param>
    /// <returns></returns>
    /// <response code="200">The update succeeded</response>
    /// <response code="201">A line item with that Id was not found for this quote so one was created</response>
    /// <response code="404">A quote with that Id could not be found</response>
    /// <response code="422">The supplied line item data has validator errors</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [Consumes(contentType: "application/json")]
    [HttpPut("{itemId}")]
    public ActionResult<LineItemOutputDTO> UpdateLineItem(LineItemInputDTO itemInput, Guid quoteId, Guid itemId)
    {
        if (!_quoteRepository.QuoteExists(quoteId))
        {
            return NotFound();
        }

        if (!_quoteRepository.LineItemExists(itemId))
        {
            var itemEntity = _mapper.Map<LineItem>(itemInput);
            _quoteRepository.AddLineItem(quoteId, itemEntity);
            _quoteRepository.Save();

            var returnLineItem = _mapper.Map<LineItemOutputDTO>(itemEntity);
        
            return CreatedAtRoute("GetLineItems", new {returnLineItem.LineItemId}, returnLineItem);
        }

        var item = _quoteRepository.GetLineItem(quoteId, itemId);

            _mapper.Map(itemInput, item);
        
            _quoteRepository.UpdateLineItem(item);
            _quoteRepository.Save();
            return Ok(_mapper.Map<LineItemOutputDTO>(item));
    }

    /// <summary>
    /// Delete a line item from a quote. This is only allowed if the quote has more than one line item
    /// </summary>
    /// <param name="itemId">the id of the line item to delete</param>
    /// <param name="quoteId">the id of the quote the line item is being deleted from</param>
    /// <returns></returns>
    /// <response code="200">The delete succeeded</response>
    /// <response code="404">A quote or line item with those Ids could not be found</response>
    /// <response code="409">The line item could not be deleted because it is the only one</response>
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    [HttpDelete("{itemId}" )]
    public IActionResult DeleteLineItem(Guid itemId, Guid quoteId)
    {
        if (!_quoteRepository.QuoteExists(quoteId))
        {
            return NotFound();
        }

        if (!_quoteRepository.LineItemExists(itemId))
        {
            return NotFound();
        }

        var quote = _quoteRepository.GetQuote(quoteId);
        if (quote.LineItems.Count !> 1)
        {
            return Conflict();
        }
        
        var item = _quoteRepository.GetLineItem(quoteId, itemId);
        _quoteRepository.DeleteLineItem(item);
        _quoteRepository.Save();

        return NoContent();
    }
}