using AutoMapper;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Domain.Profiles;

public class QuoteProfile : Profile
{
    public QuoteProfile()
    {
        CreateMap<Data.Models.Quote, DTOs.OutputQuoteDTO>();
        CreateMap<DTOs.QuoteInputDTO, Data.Models.Quote>();
        CreateMap<DTOs.QuoteCreationDTO, Data.Models.Quote>();
        CreateMap<DTOs.QuoteUpdateDTO, Data.Models.Quote>();
        CreateMap<Data.Models.LineItem, DTOs.LineItemOutputDTO>();
        CreateMap<DTOs.LineItemInputDTO, Data.Models.LineItem>();
    }
}