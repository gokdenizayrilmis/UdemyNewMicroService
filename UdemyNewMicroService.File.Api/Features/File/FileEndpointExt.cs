using Asp.Versioning.Builder;
using UdemyNewMicroService.File.Api.Features.File.Delete;
using UdemyNewMicroService.File.Api.Features.File.Upload;

namespace UdemyNewMicroService.File.Api.Features.File
{
    public static class FileEndpointExt
    {
        public static void AddFileGroupEndpointExt(this WebApplication app, ApiVersionSet apiVersionSet) 
        {
            app.MapGroup("api/v{version:apiVersion}/files")
                .WithTags("Files")
                .WithApiVersionSet(apiVersionSet)
                .UploadFileGroupItemEndpoint()
                .DeleteFileGroupItemEndpoint();
        }
    }
}