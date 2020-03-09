using AutoMapper;
using TheSneakersMob.Models;
using TheSneakersMob.Services.Sells;

namespace TheSneakersMob.Extensions.AutoMapper
{
    public class SellProfile : Profile
    {
        public SellProfile()
        {
            CreateMap<PhotoForSellDto,Photo>();
            
        }
        
    }
}