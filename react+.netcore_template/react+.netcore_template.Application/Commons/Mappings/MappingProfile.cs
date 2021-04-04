using System;
using System.Collections.Generic;
using System.Text;

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
