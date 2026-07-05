using Amazon.Runtime.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;
using UdemyNewMicroService.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroService.Catalog.Api.Features.Categories.DTOs;
using UdemyNewMicroService.Catalog.Api.Features.Categories.GetAll;
using UdemyNewMicroService.Catalog.Api.Repositories;
using UdemyNewMicroService.Shared;
using UdemyNewMicroService.Shared.Extensions;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories.GetAll
{
    public class GetAllCategoryQuery : IRequest<ServiceResult<List<CategoryDto>>>;

    public class GetAllCategoryQueryHandler(AppDbContext context) : IRequestHandler<GetAllCategoryQuery, ServiceResult<List<CategoryDto>>>
    {
        
        public async Task<ServiceResult<List<CategoryDto>>> Handle(GetAllCategoryQuery request, CancellationToken cancellationToken)
        {
            var categories = await context.Categories.ToListAsync();
            var categoriesAsDto = categories.Select(x => new CategoryDto(x.Id, x.Name)).ToList();
            return ServiceResult<List<CategoryDto>>.SuccessAsOk(categoriesAsDto);
        }

    }

    public static class GetAllCategoryEndpoint
    {
        public static RouteGroupBuilder GetAllCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/", async (IMediator mediator) =>
                (await mediator.Send(new GetAllCategoryQuery())).ToGenericResult());

            return group;
        }
    }
     
}