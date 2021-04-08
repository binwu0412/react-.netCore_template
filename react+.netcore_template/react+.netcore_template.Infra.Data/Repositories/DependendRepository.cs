using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Domain.Entities;
using react_.netcore_template.Infra.Data.Context;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace react_.netcore_template.Infra.Data.Repositories
{
    public class DependendRepository : IDependendRepository
    {
        private readonly EmployeeDbContext _dbContext;
        private readonly IMapper _mapper;

        public DependendRepository(EmployeeDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public Task<List<DependendDto>> GetAllDtoWithEmployeeIdAsync(int employeeId)
        {
            return _dbContext.Dependends.Where(d => d.EmployeeId == employeeId)
                .ProjectTo<DependendDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
        }

        public async Task UpdateAndDeleteRangeAsync(IList<Dependend> dependends, int employeeId)
        {
            var deleteDependends = await _dbContext.Dependends.Where(d => d.EmployeeId == employeeId && !dependends.Contains(d)).ToListAsync();
            _dbContext.Dependends.RemoveRange(deleteDependends);

            var editDependends = new List<Dependend>();
            for (var i = 0; i < dependends.Count; i++)
            {
                if (editDependends[i].Id == 0) editDependends.Add(dependends[i]);
            }
            _dbContext.Dependends.AddRange(editDependends);

            await _dbContext.SaveChangesAsync();
        }
    }
}
