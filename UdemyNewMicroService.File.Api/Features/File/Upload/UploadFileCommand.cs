using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.File.Api.Features.File.Upload;

public record UploadFileCommand(IFormFile file) : IRequestByServiceResult<UploadFileCommandResponse>;

