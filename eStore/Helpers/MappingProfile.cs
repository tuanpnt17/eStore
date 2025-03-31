using AutoMapper;
using BusinessObject.Models;
using eStore.ViewModels;

namespace eStore.Helpers;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CartDTO, CartViewModel>().ReverseMap();
        CreateMap<CartItemDTO, CartItemViewModel>().ReverseMap();
    }
}
