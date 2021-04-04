using MediatR;
using react_.netcore_template.Application.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Cache.Commands
{
    public class RemoveEmployeeWithIdCacheCommand : IRequest
    {
        public string CacheKey { get; protected set; }

        public RemoveEmployeeWithIdCacheCommand(string cacheKey)
        {
            CacheKey = cacheKey;
        }
    }

    public class RemoveEmployeeWithIdCacheCommandHandler : IRequestHandler<RemoveEmployeeWithIdCacheCommand>
    {
        private ICacheService _cacheService;

        public RemoveEmployeeWithIdCacheCommandHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(RemoveEmployeeWithIdCacheCommand request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByKeyAsync(request.CacheKey);
            return Unit.Value;
        }
    }
}
