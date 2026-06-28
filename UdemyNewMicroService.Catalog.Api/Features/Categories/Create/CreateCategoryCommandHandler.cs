using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UdemyNewMicroService.Catalog.Api.Repositories;
using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandHandler(AppDbContext context) : IRequestHandler<CreateCategoryCommand, ServiceResult<CreateCategoryResponse>>
    {
        public async Task<ServiceResult<CreateCategoryResponse>> Handle(CreateCategoryCommand request, CancellationToken cancellationToken)
        {
            var existingCategory = await context.Categories.AnyAsync(x => x.Name == request.Name);

            if (existingCategory)
            {
                return ServiceResult<CreateCategoryResponse>.Error("Category Name Already Exists", $"A category with the same name {request.Name} already exists.", HttpStatusCode.BadRequest);
            }

            var category = new Category
            {
                Name = request.Name,
                Id = NewId.NextSequentialGuid()
            };

            await context.Categories.AddAsync(category, cancellationToken);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<CreateCategoryResponse>.SuccessAsCreated(new CreateCategoryResponse(category.Id), url: "<empty>");
        }
    }
}
