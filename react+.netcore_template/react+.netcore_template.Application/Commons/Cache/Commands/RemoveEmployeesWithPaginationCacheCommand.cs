using MediatR;
using react_.netcore_template.Application.Commons.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace react_.netcore_template.Application.Commons.Cache.Commands
{
    public class RemoveEmployeesWithPaginationCacheCommand : IRequest
    {
        public string CachePattern { get; protected set; }

        public RemoveEmployeesWithPaginationCacheCommand(string cachePattern)
        {
            CachePattern = cachePattern;
        }
    }

    public class RemoveEmployeesWithPaginationCacheCommandHandler : IRequestHandler<RemoveEmployeesWithPaginationCacheCommand>
    {

        private ICacheService _cacheService;

        public RemoveEmployeesWithPaginationCacheCommandHandler(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task<Unit> Handle(RemoveEmployeesWithPaginationCacheCommand request, CancellationToken cancellationToken)
        {
            await _cacheService.RemoveByPatternAsync(request.CachePattern);
            return Unit.Value;
        }
    }
}
