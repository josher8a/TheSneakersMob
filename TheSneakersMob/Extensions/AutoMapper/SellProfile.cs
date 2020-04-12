using AutoMapper;
using TheSneakersMob.Models;
using TheSneakersMob.Services.Common;

namespace TheSneakersMob.Extensions.AutoMapper
{
    public class SellProfile : Profile
    {
        public SellProfile()
        {
            CreateMap<PhotoDto,Photo>();
            
        }
        
    }
}