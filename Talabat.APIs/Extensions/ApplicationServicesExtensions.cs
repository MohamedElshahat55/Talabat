using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.Errors;
using Talabat.APIs.Helpers;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Repository;

namespace Talabat.APIs.Extensions
{
    public static class ApplicationServicesExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services) {
            //Allow DI For Controller
            Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            //Allow AutoMapper
            Services.AddAutoMapper(typeof(MappingProfiles));
            // Change the Configuration For Errors
            Services.Configure<ApiBehaviorOptions>(Options =>
            {
                Options.InvalidModelStateResponseFactory = (actionContext) =>
                {
                    var errors = actionContext.ModelState.Where(P => P.Value.Errors.Count() > 0)
                                                        .SelectMany(P => P.Value.Errors)
                                                        .Select(E => E.ErrorMessage)
                                                        .ToList();
                    var ValidationErrorResponse = new ApiValidationError()
                    {
                        Errors = errors
                    };
                    return new BadRequestObjectResult(ValidationErrorResponse);
                };

            });
            // Allow DI For IBasketReository
            Services.AddScoped(typeof(IBasketRepository), typeof(BasketRepository));


			return Services;
        }
    }
}
