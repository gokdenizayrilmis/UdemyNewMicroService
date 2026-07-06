namespace UdemyNewMicroService.Catalog.Api.Features.Courses.Update
{
    public class UpdateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<UpdateCourseCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdateCourseCommand request, CancellationToken cancellationToken)
        {
            var hascourse = await context.Courses.FindAsync([request.Id], cancellationToken);
            if (hascourse is null)
            {
                return ServiceResult.ErrorAsNotFound();
            }


            hascourse.Name = request.Name;
            hascourse.Description = request.Description;
            hascourse.Price = request.Price;
            hascourse.ImageUrl = request.ImageUrl;
            hascourse.CategoryId = request.CategoryId;

            context.Courses.Update(hascourse);

            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult.SuccessAsNoContent();

        }
    }
}
