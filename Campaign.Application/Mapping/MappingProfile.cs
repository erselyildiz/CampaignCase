using AutoMapper;
using Campaign.Application.ViewModels;

namespace Campaign.Application.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Domain.Entities.Campaign, CampaignViewModel>().ReverseMap();
        }
    }
}