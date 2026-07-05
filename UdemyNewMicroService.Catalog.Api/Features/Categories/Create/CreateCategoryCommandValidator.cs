using FluentValidation;
using MassTransit;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;
using UdemyNewMicroService.Catalog.Api.Repositories;
using UdemyNewMicroService.Shared;

namespace UdemyNewMicroService.Catalog.Api.Features.Categories.Create
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommand>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Length(4,25).WithMessage("{PropertyName} must be between 4 and 25 characters.");
        }
    }
}
