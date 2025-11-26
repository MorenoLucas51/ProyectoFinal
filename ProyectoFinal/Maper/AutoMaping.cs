using AutoMapper;
using ProyectoFinal.Dtos;
using ProyectoFinal.Models;

namespace ProyectoFinal.Maper
{
    public class AutoMaping:Profile
    {
        public AutoMaping()
        {
            CreateMap<LaboratorioCreateDto, Laboratorio>();
            CreateMap<LaboratorioDto, Laboratorio>();
            CreateMap<Laboratorio, LaboratorioDto>();
            CreateMap<ActivosCreateDto, Activo>();
            CreateMap<ActivoDto, Activo>();
            CreateMap<Activo, ActivoDto>();
            CreateMap<Medicamento, MedicamentoDto>()
                .ForMember(dest => dest.Activos, opt => opt.MapFrom(src => src.Activos));
            CreateMap<MedicamentoCreateDto, Medicamento>(); 


        }
    }
}
