using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.File.Api.Features.File.Delete
{
    public record DeleteFileCommand(string FileName) : IRequestByServiceResult;
}
