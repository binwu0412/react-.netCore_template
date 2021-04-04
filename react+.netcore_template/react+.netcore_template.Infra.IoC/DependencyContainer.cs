﻿using MediatR;
using Microsoft.Extensions.DependencyInjection;
using react_.netcore_template.Application.Commons.BenefitCost.Queries;
using react_.netcore_template.Application.Commons.Cache.Commands;
using react_.netcore_template.Application.Commons.Dependends.Queries;
using react_.netcore_template.Application.Commons.Employees.Commands;
using react_.netcore_template.Application.Commons.Employees.Queries;
using react_.netcore_template.Application.Commons.Interfaces;
using react_.netcore_template.Application.Commons.Models;
using react_.netcore_template.Infra.Data.Context;
using react_.netcore_template.Infra.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace react_.netcore_template.Infra.IoC
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            // query handers
            services.AddScoped<IRequestHandler<GetBenefitCostByEmployeeIdQuery, BenefitCostPreviewDto>, GetBenefitCostByEmployeeIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllDependendsWithEmployeeIdQuery, List<DependendDto>>, GetAllDependendsWithEmployeeIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetAllEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>, GetAllEmployeesWithPaginationQueryHandler>();
            services.AddScoped<IRequestHandler<GetEmployeeWithIdQuery, EmployeeDto>, GetEmployeeWithIdQueryHandler>();
            services.AddScoped<IRequestHandler<GetSearchEmployeesWithPaginationQuery, PaginatedList<EmployeeDto>>, GetSearchEmployeesWithPaginationQueryHandler>();

            // commands
            services.AddScoped<IRequestHandler<RemoveEmployeesWithPaginationCacheCommand>, RemoveEmployeesWithPaginationCacheCommandHandler>();
            services.AddScoped<IRequestHandler<RemoveEmployeeWithIdCacheCommand>, RemoveEmployeeWithIdCacheCommandHandler>();
            services.AddScoped<IRequestHandler<AddEmployeeCommand>, AddEmployeeCommandHandler>();
            services.AddScoped<IRequestHandler<UpdateEmployeeCommand>, UpdateEmployeeCommandHandler>();

            // data
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            services.AddScoped<IDependendRepository, DependendRepository>();
            services.AddScoped<EmployeeDbContext>();
        }
    }
}
