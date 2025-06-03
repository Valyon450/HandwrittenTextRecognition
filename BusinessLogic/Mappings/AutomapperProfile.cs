using AutoMapper;
using BusinessLogic.DTOs;
using DataAccess.Entities;

namespace BusinessLogic.Mappings
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<InFile, InFileDTO>()
                .ReverseMap();

            CreateMap<RecognitionModel, RecognitionModelDTO>()
                .ReverseMap();

            CreateMap<RecognitionResult, RecognitionResultDTO>()
                .ReverseMap();

            CreateMap<TextBlock, TextBlockDTO>()
                .ReverseMap();

            CreateMap<Tuning, TuningDTO>()
                .ReverseMap();

            CreateMap<User, UserDTO>()
                .ReverseMap();

            CreateMap<USRS, USRSDTO>()
                .ReverseMap();
        }
    }
}
