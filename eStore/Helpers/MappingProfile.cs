using AutoMapper;
using BusinessObject.Models.Member;
using BusinessObject.Models.Product;
namespace eStore.Helpers;

public class MappingProfile : Profile
{
	public MappingProfile()
	{
		CreateMap<CreateProductDTO, Product>();

		CreateMap<Product, ProductViewDTO>()
			.ForMember(dest => dest.Category, opt => opt.MapFrom(src => src.Category));

		CreateMap<UpdateProductDTO, Product>()
			.ForAllMembers(opts => opts.Condition((src, dest, srcMember) =>
					srcMember != null || !srcMember?.Equals(default) == true));

		CreateMap<Product, UpdateProductDTO>();
		CreateMap<ProductViewDTO, UpdateProductDTO>();
		CreateMap<UpdateProductDTO, ProductViewDTO>();

		CreateMap<Member, MemberViewDTO>();
		CreateMap<CreateMemberDTO, Member>();
		CreateMap<UpdateMemberDTO, Member>()
			.ForAllMembers(opt => opt.Condition((src, dest, srcMember) => srcMember != null));

		CreateMap<Member, UpdateMemberDTO>();
	}
}
