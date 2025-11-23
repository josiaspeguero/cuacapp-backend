using AutoMapper;
using CuacApp.Domain.DTOs;
using CuacApp.Domain.Modelos;
using CuacApp.Domain.Models;

namespace CuacApp.Data
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<UsuarioDTO, Usuario>().ReverseMap();
            CreateMap<CodigoUsuarioDTO, CodigoUsuario>().ReverseMap();
        }
    }
}
