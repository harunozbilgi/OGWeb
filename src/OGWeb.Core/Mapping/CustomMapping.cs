using AutoMapper;
using OGWeb.Core.Commands.Works;
using OGWeb.Core.Dtos;
using OGWeb.Core.Entities;

namespace OGWeb.Core.Mapping;

public class CustomMapping : Profile
{
    public CustomMapping()
    {
        #region  WorkMapping
        CreateMap<CreateWorkCommand, Work>();
        CreateMap<UpdateWorkCommand, Work>();
        CreateMap<Work, WorkDto>().ReverseMap();
        #endregion

    }
}