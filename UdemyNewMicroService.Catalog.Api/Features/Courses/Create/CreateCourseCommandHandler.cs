namespace UdemyNewMicroService.Catalog.Api.Features.Courses.Create
{
    public class CreateCourseCommandHandler(AppDbContext context, IMapper mapper) : IRequestHandler<CreateCourseCommand, ServiceResult<Guid>>
    {
        public async Task<ServiceResult<Guid>> Handle(CreateCourseCommand request, CancellationToken cancellationToken)
        {

            var hasCategory = await context.Categories.AnyAsync(c => c.Id == request.CategoryId, cancellationToken);

            if (!hasCategory)
            {
                return ServiceResult<Guid>.Error("Category not found.", $"The Caregory with id({request.CategoryId}) was not found.", HttpStatusCode.NotFound);
            }

            var hasCourse = await context.Courses.AnyAsync(c => c.Name == request.Name, cancellationToken);
            
            if (hasCourse)
            {
                return ServiceResult<Guid>.Error("Course already exists.", $"The Course with name({request.Name}) already exists.", HttpStatusCode.BadRequest);
            }

            var newCourse = mapper.Map<Course>(request);
            newCourse.Created = DateTime.UtcNow;
            newCourse.Id = NewId.NextSequentialGuid();
            newCourse.Feature = new Feature()
            {
                Duration = 10, //kurs videolarını toplayacak
                EducatorFullName = "Gökdeniz Ayrılmış", //tokendan alacağız
                Rating = 0
            };

            context.Courses.Add(newCourse);
            await context.SaveChangesAsync(cancellationToken);

            return ServiceResult<Guid>.SuccessAsCreated(newCourse.Id, $"/api/courses/{newCourse.Id}");

        }
    }
}

