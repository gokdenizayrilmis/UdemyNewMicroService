using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UdemyNewMicroService.Shared.Filters
{
    public class ValidationFilter<T> : IEndpointFilter
    {
        public async ValueTask<object?> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {

            var validator = context.HttpContext.RequestServices.GetService<IValidator<T>>();

            if(validator is null)
            {
                return await next(context);
            }

            var requestmodel = context.Arguments.OfType<T>().FirstOrDefault();

            if (requestmodel is null)
            {
                return await next(context);
            }

            var validateresult = await validator.ValidateAsync(requestmodel);

            if(!validateresult.IsValid)
            {
               return Results.ValidationProblem(validateresult.ToDictionary());
            }

            return await next(context);

        }
    }
}