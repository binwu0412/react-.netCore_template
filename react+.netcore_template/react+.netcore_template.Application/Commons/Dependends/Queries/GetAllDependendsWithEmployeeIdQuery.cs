using MediatR;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Dependends.Queries
{
    public class GetAllDependendsWithEmployeeIdQuery : IRequest<List<DependendDto>>, ICacheKey
    {
        public int EmployeeId { get; set; }

        public string GetCacheKey() => "getAllDependendsWithEmployeeIdQuery";
    }

    public class GetAllDependendsWithEmployeeIdQueryHandler : IRequestHandler<GetAllDependendsWithEmployeeIdQuery, List<DependendDto>>
    {
        private readonly IDependendRepository _dependendRepository;
        private readonly ICacheService _cacheService;

        public GetAllDependendsWithEmployeeIdQueryHandler(IDependendRepository dependendRepository, ICacheService cacheService)
        {
            _dependendRepository = dependendRepository;
            _cacheService = cacheService;
        }

        public async Task<List<DependendDto>> Handle(GetAllDependendsWithEmployeeIdQuery request, CancellationToken cancellationToken)
        {
            var result = await _cacheService.GetCachedAsync<List<DependendDto>>(request.GetCacheKey());
            if (result != null) return result;

            var dependendDtos = await _dependendRepository.GetAllDtoWithEmployeeIdAsync(request.EmployeeId);
            await _cacheService.CacheAsync(request.GetCacheKey(), dependendDtos);
            return dependendDtos;
        }
    }
}
