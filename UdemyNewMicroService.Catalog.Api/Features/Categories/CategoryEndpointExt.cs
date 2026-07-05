using UdemyNewMicroService.Catalog.Api.Features.Categories.Create;
using UdemyNewMicroService.Catalog.Api.Features.Categories.GetAll;
using UdemyNewMicroService.Shared.Filters;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories
{
    public static class CategoryEndpointExt
    {
        public static void AddCategoryGroupEndpointExt(this WebApplication app) 
        {
            app.MapGroup("api/categories")
                .CreateCategoryGroupItemEndpoint()
                .GetAllCategoryGroupItemEndpoint();
        }
    }
}
