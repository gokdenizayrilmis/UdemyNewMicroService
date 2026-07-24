namespace UdemyNewMicroService.Catalog.Api.Features.Categories.GetById
{
    public record GetCategoryByIdQuery(Guid Id) : IRequestByServiceResult<CategoryDto>;

    public class GetCategoryByIdQueryHandler(AppDbContext context, IMapper mapper) : IRequestHandler<GetCategoryByIdQuery, ServiceResult<CategoryDto>>
    {
        public async Task<ServiceResult<CategoryDto>> Handle(GetCategoryByIdQuery request, CancellationToken cancellationToken)
        {
            var hasCategory = await context.Categories.FindAsync(request.Id, cancellationToken);
            if (hasCategory is null)
            {
                return ServiceResult<CategoryDto>.Error("Category Not Found", "The category you are looking for does not exist.", System.Net.HttpStatusCode.NotFound);
            }
            else
            {
                var categoryDto = mapper.Map<CategoryDto>(hasCategory);
                return ServiceResult<CategoryDto>.SuccessAsOk(categoryDto);
            }
        }
    }

    public static class GetCategoryByIdEndpoint
    {
        public static RouteGroupBuilder GetByIdCategoryGroupItemEndpoint(this RouteGroupBuilder group)
        {
            group.MapGet("/{id:guid}", async (IMediator mediator, Guid id) =>
                (await mediator.Send(new GetCategoryByIdQuery(id))).ToGenericResult())
                .MapToApiVersion(1,0)
                .WithName("GetByIdCategory").RequireAuthorization("ClientCredential");


            return group;
        }
    }

}
