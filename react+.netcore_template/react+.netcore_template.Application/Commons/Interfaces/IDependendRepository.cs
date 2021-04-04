using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Interfaces
{
    public interface IDependendRepository
    {

        Task<List<DependendDto>> GetAllDtoWithEmployeeIdAsync(int employeeId);

        Task UpdateAndDeleteRangeAsync(IList<Dependend> dependends, int employeeId);
    }
}
