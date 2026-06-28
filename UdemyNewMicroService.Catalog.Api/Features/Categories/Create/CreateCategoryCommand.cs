using MediatR;
using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories.Create
{
    public record CreateCategoryCommand(string Name): IRequest<ServiceResult<CreateCategoryResponse>>;

}
