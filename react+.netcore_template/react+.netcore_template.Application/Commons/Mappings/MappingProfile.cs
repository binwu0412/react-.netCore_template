using AutoMapper;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;

namespace react_.netcore_template.Application.Commons.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Dependend, DependendDto>();
            CreateMap<Employee, EmployeeDto>();
        }
    }
}
