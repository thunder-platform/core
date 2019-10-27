using System;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using Thunder.Platform.AspNetCore.ExceptionHandling;
using Thunder.Platform.AspNetCore.ModelBinding;
using Thunder.Platform.AspNetCore.UnitOfWork;
using Thunder.Platform.AspNetCore.Validation;
using Thunder.Platform.Core.Json;

namespace Thunder.Platform.AspNetCore
{
    public static class ThunderMvcOptionsExtensions
    {
        public static void AddThunderMvcOptions(this MvcOptions options)
        {
            AddActionFilters(options);
            AddModelBinders(options);
        }

        public static IMvcBuilder AddThunderJsonOptions(this IMvcBuilder builder)
        {
            builder.AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                options.JsonSerializerOptions.IgnoreNullValues = true;

                options.JsonSerializerOptions.Converters.Add(new ThunderDateTimeConverter());
                options.JsonSerializerOptions.Converters.Add(new ThunderNullableDateTimeConverter());
            });

            return builder;
        }

        /// <summary>
        /// Use filter or middleware. <see cref="Thunder.Infrastructure.AspNetCore.UnitOfWorkMiddleware"/>.
        /// </summary>
        /// <param name="options">The MvcOptions.</param>
        public static void AddUowActionFilter(this MvcOptions options)
        {
            options.Filters.AddService(typeof(ThunderUowActionFilter));
        }

        private static void AddActionFilters(MvcOptions options)
        {
            options.Filters.AddService(typeof(ThunderExceptionFilter));
            options.Filters.AddService(typeof(ThunderModelValidationFilter));
        }

        private static void AddModelBinders(MvcOptions options)
        {
            options.ModelBinderProviders.Insert(0, new ThunderDateTimeModelBinderProvider());
        }
    }
}
